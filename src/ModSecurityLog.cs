using ModSecurityLogService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.LayoutRenderers;

namespace ModSecurityLogService
{
    public partial class ModSecurityLog : ServiceBase
    {
        private bool _eventLog;
        private string _logPath;
        private NLog.Logger _logger;

        public ModSecurityLog()
        {
            InitializeComponent();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            _eventLog = ConfigurationManager.AppSettings["EventLog"] == "Yes";
            _logPath = ConfigurationManager.AppSettings["LogPath"];
            _logger = NLog.LogManager.GetCurrentClassLogger();
            GlobalDiagnosticsContext.Set("Application", "ModSecurityLog");
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            if (e.InnerException != null)
            {
                EventLog.WriteEntry("ModSecurityLog", e.Message + " " + e.InnerException.Message, EventLogEntryType.Error);
            }
            else
            {
                EventLog.WriteEntry("ModSecurityLog", e.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnStart(string[] args)
        {
            // Set path of fsLogWatcher.
            fsLogWatcher.Path = _logPath;
        }

        protected override void OnStop()
        {
        }

        private void fsLogWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            // Only Files (not directories)
            if (File.Exists(e.FullPath))
            {
                try
                {
                    System.Threading.Thread.Sleep(500); // wait 0.5 seconds. - because the file was just created ;)
                    string logData = File.ReadAllText(e.FullPath);

                    var secLog = JsonConvert.DeserializeObject<SecAuditLog>(logData);
                    if (secLog != null)
                    {
                        // Use NLog for greater customization.
                        var logEvent = NLogModSecurity.ConvertToEventInfo(secLog, LogLevel.Warn, _logger.Name, secLog.audit_data?.action?.message ?? "");
                        _logger.Log(logEvent);

                        if (_eventLog)
                        {
                            EventLog.WriteEntry("ModSecurityLog", "Logged " + (secLog.audit_data?.action?.message ?? ""), EventLogEntryType.Information);
                        }
                    }

                    // remove file. (we're done here).
                    File.Delete(e.FullPath);

                    // Every 5th time (or so) try a cleanup
                    var r = new Random();
                    if (r.Next(0,5) == 0)
                    {
                        DeleteEmptyDirs(_logPath);
                    }
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry("ModSecurityLog", "Error " + ex.Message, EventLogEntryType.Warning);
                }
            }
        }

        // This is to clean the Concurrent ModSecurity log directory
        private void DeleteEmptyDirs(string directory)
        {
            try
            {
                foreach (var d in Directory.EnumerateDirectories(directory))
                {
                    DeleteEmptyDirs(d);
                }


                if (!Directory.EnumerateFileSystemEntries(directory).Any())
                {
                    try
                    {
                        Directory.Delete(directory);
                    }
                    catch (UnauthorizedAccessException) { }
                    catch (DirectoryNotFoundException) { }
                }
            }
            catch (UnauthorizedAccessException) { }
        }

    }
}

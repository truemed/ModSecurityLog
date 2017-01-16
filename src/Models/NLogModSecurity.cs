using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModSecurityLogService.Models
{
    [LayoutRenderer("modsecurity")]
    public static class NLogModSecurity
    {
        
        /// <summary>
        /// Returns as LogEventInfo for NLog
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logLevel"></param>
        /// <param name="loggerName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static LogEventInfo ConvertToEventInfo(SecAuditLog log, LogLevel logLevel, string loggerName, string message)
        {
            var logInfo = new LogEventInfo(logLevel, loggerName, message);

            // Parse the time. Format: 16/Jan/2017:11:55:44 --0500
            DateTime logtime;
            string transTime = log.transaction?.time ?? "";
            // Format as 16 Jan 2017 11:55:44
            transTime = transTime.Replace("/", " ");
            int c = transTime.IndexOf(':');
            transTime = transTime.Remove(c, 1).Insert(c, " ");
            transTime = transTime.Substring(0, transTime.IndexOf('-'));
            if (!DateTime.TryParse(transTime, out logtime))
            {
                logtime = DateTime.Now;
            }

            string serverName = System.Environment.MachineName?.ToLower();

            logInfo.Properties.Add("logtime", logtime);
            logInfo.Properties.Add("server", serverName);
            logInfo.Properties.Add("remote_address", log.transaction?.remote_address ?? "");
            logInfo.Properties.Add("remote_port", log.transaction?.remote_port ?? 80);
            logInfo.Properties.Add("request_url", log.request?.request_line ?? "");
            logInfo.Properties.Add("request_headers", log.request?.headers != null ? JsonConvert.SerializeObject(log.request.headers) : "");
            logInfo.Properties.Add("response_protocol", log.response?.protocol ?? "");
            logInfo.Properties.Add("response_status", log.response?.status ?? 0);
            logInfo.Properties.Add("audit_messages", log.audit_data?.messages != null ? String.Join("\r\n", log.audit_data.messages) : "");
            logInfo.Properties.Add("audit_action_intercepted", log.audit_data?.action?.intercepted ?? false);
            logInfo.Properties.Add("audit_action_message", log.audit_data?.action?.message ?? "");
            logInfo.Properties.Add("engine_mode", log.audit_data?.engine_mode ?? "");
            logInfo.Properties.Add("data", ""); // for future data

            return logInfo;

        }

    }
}

# ModSecurity Log Capture Service

[ModSecurity](https://github.com/SpiderLabs/ModSecurity) is an open source, cross platform web application firewall for IIS, Apache and Nginx.

# ModSecurity Logging Windows Service 

ModSecurity Log Capture Service is a Windows Service that watches the log folder and automatically captures the logs and sends them to a configured NLog target, like a SQL Server Database.
It uses a Windows File Watcher, listening to the FileCreated event, at which time it reads the log file, sends it to NLog, which sends it to the Database target.

* [How to install](#how-to-install-the-windows-service)
* [Configure ModSecurity](#configure-modsecurity)
* [How to configure NLog/Database target settings](#how-to-configure-nlogdatabase-target-settings)

## How to install the windows service

1. Unzip the files in the release to a folder.
2. Run the `install.ps1` command. (This uses the `InstallUtil` from the .NET framework.)
3. Open up Local Services (Control Panel > Administrative Tools > Services) and select "ModSecurityLog" and click "Start".
If it does not start, update the permissions on the folder to allow the "Network Service" user read access.

## Configure ModSecurity

1. Open the `modsecurity_iis.conf` file and update it to the following:

    ````
    SecAuditEngine RelevantOnly
    SecAuditLogType Concurrent
    SecAuditLogFormat JSON
    SecAuditLogStorageDir "D:\Path\To\Log\Directory"
    SecAuditLog "D:\Path\To\Log\Directory\modsec_audit.log"
    
    ````
2. Back in this service's folder, open up the `ModSecurityLogService.exe.config` file and update the `LogPath` to be the same as the SecAuditLogStorageDir value.

Ensure that you restart this windows service (ModSecurityLog) any time you make changes to the `ModSecurityLogService.exe.config` file.

## How to configure NLog/Database target settings

1. Open `nlog.config`, file.
2. Update the `<target>` to match your Database settings and table format.
3. Alternatively, create your own custom NLog `<target>`

## How to uninstall
1. Stop the service (Control Panel > Administrative Tools > Services > ModSecurityLog)
2. Run the `install.ps1 -uninstall` command.

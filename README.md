# ModSecurity IIS Logging Windows Service 

ModSecurity Log Capture Service is a Windows Service that watches the ModSecurity log folder and automatically captures the logs and sends them to a configured NLog target, like a SQL Server Database.
It uses a Windows File Watcher, listening to the FileCreated event, at which time it reads the log file, and uses NLog to send the log to the Database. 
Since it uses [NLog](https://github.com/NLog/NLog), 
it could be [any NLog Target](https://github.com/nlog/nlog/wiki/Targets).

  [ModSecurity](https://github.com/SpiderLabs/ModSecurity) is an open source, cross platform web application firewall for IIS, Apache and Nginx.

* [How to install the Windows Service](#how-to-install-the-windows-service)
* [Configure ModSecurity](#configure-modsecurity)
* [How to configure NLog/Database target settings](#how-to-configure-nlogdatabase-target-settings)
* [How to uninstall](#how-to-uninstall)

## How to install the Windows Service

1. [Download the Release zip file](https://github.com/truemed/ModSecurityLog/releases/download/v1.0/ModSecurityLogService.zip)
2. Unzip the files in the release to a folder.
3. Run the `install.ps1` command (Open Powershell as Administrator). (This uses the `InstallUtil` from the .NET framework.)
4. Open up Local Services (Control Panel > Administrative Tools > Services) and select "ModSecurityLog" and click "Start".
You may need to update the permissions on the folder to allow the "Network Service" user read access for the service to start.

## Configure ModSecurity

1. Open the `modsecurity_iis.conf` file and update it to the following:

    ````
    SecAuditEngine RelevantOnly
    SecAuditLogType Concurrent
    SecAuditLogFormat JSON
    SecAuditLogStorageDir "D:\Path\To\Log\Directory"
    SecAuditLog "D:\Path\To\Log\Directory\modsec_audit.log"
    
    ````
2. Back in this service's folder, open up the `ModSecurityLogService.exe.config` file and update the `LogPath` to be the same as the `SecAuditLogStorageDir` value.

Ensure that you restart this windows service (ModSecurityLog) any time you make changes to the `ModSecurityLogService.exe.config` file.

## How to configure NLog/Database target settings

1. Open `nlog.config`, file.
2. Update the `<target>` to match your Database settings and table format.
3. Alternatively, create your own custom NLog `<target>`

## How to uninstall
1. Stop the service (Control Panel > Administrative Tools > Services > ModSecurityLog)
2. Run the `install.ps1 -uninstall` command.  (Open Powershell as Administrator)

## To Be Extended..
This service could be extended to trigger IP Restrictions on an external firewall or send an alert to an admin of attack velocity.
These are both ideas of modules that could use the ModSecurity data to trigger actions to do something about the alerts.
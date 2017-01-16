# ModSecurity Log Capture Service

[ModSecurity](https://github.com/SpiderLabs/ModSecurity) is an open source, cross platform web application firewall for IIS, Apache and Nginx
ModSecurity Log Capture Service is a Windows Service that watches the log folder and automatically captures the logs and sends them to a configured NLog target, like a SQL Server Database.

* [How to install](#how-to-install)
* [How to configure NLog/Database target settings](#how-to-configure)

## How to install

1. Unzip the files in the release to a folder.
2. Run the `install.ps1` command.
3. Open up Local Services (Control Panel > Administrative Tools > Services) and select "ModSecurityLog" and click "Start".

## How to configure NLog/Database target settings

1. Open `nlog.config`, file.
2. Update the `<target>` to match your Database settings and table format.
3. Alternatively, create your own custom NLog `<target>`


## How to uninstall
1. Stop the service (Control Panel > Administrative Tools > Services > ModSecurityLog)
2. Run the `install.ps1 -uninstall` command.

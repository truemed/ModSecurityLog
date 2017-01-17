# Run this in an elevated shell (Administrator)
param (
    [switch]$uninstall=$false
 )

$invocation = (Get-Variable MyInvocation).Value
$directorypath = Split-Path $invocation.MyCommand.Path
$servicePath = $directorypath + "\ModSecurityLogService.exe"

if (!$uninstall) {
    
    #To install service with the latest executable.
    & "$env:windir\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" "$servicePath"
    
    # Assign "Network Service" Read & Execute access to the folder.
    $Acl = Get-Acl $directorypath
    $Ar = New-Object  system.security.accesscontrol.filesystemaccessrule("Network Service","ReadAndExecute","Allow")
    $Acl.SetAccessRule($Ar)
    Set-Acl $directorypath $Acl

} else {
    
    #To un-install service:
    & "$env:windir\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" /U "$servicePath"

}
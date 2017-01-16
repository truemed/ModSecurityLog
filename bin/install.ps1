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

} else {
    
    #To un-install service:
    & "$env:windir\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" /U "$servicePath"

}
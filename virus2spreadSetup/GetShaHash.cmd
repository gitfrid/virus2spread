SET PowerShellDir=C:\Windows\System32\WindowsPowerShell\v1.0
CD /D "%PowerShellDir%"
Powershell -ExecutionPolicy Bypass -Command "& 'C:\Github\virus2spread\virus2spreadSetup\GetHash.ps1'"
pause
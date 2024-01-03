Get-FileHash "C:\Github\virus2spread\virus2spreadSetup\Release\setup.exe" | Format-List | Out-File -FilePath "C:\Github\virus2spread\virus2spreadSetup\Virus2spreadHash.txt"
Get-FileHash "C:\Github\virus2spread\virus2spreadSetup\Release\virus2spreadSetup.msi" | Format-List | Out-File -Append -FilePath "C:\Github\virus2spread\virus2spreadSetup\Virus2spreadHash.txt"
pause

param(
	[Parameter()] $ProjectName,
	[Parameter()] $ConfigurationName,
	[Parameter()] $TargetDir
)

#Copy 'PsExcelReader.dll' '.\PsExcelReader' -Force -Verbose
Get-ChildItem | where {$_.Mode -eq '-a----'} | copy -Destination '.\PsExcelReader' -Force -Verbose

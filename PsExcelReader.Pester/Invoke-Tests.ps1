﻿<#
This script will run on debug to execute the Pester tests.
#>

# Make sure we can load the local modules.
$env:PSModulePath = (Resolve-Path .).Path + ";" + $env:PSModulePath

# Optionally; load the module under test:
$env:PSModulePath = (Resolve-Path "..\..\..\PsExcelReader\bin\Debug").Path + ";" + $env:PSModulePath
Import-Module  'PsExcelReader'

# Load Pester. We assume it can be found in one of the module paths.
Import-Module Pester

# Run the tests. By default all tests matching *.Tests.ps1 will be executed.
# See https://github.com/pester/Pester for more information.
Invoke-Pester

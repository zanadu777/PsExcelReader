Describe "GetExcelList" {

	It "Reading BasicList" {

		$TestData = (Resolve-Path "..\..\TestData").Path

		$output = Get-ExcelList -File (Resolve-Path ($TestData + "\ReadExcel.xlsx")) -Name Name
		$output.Count | Should be 3
		$output -join "," | Should be "Ken,Carol,Bob"

		$output = Get-ExcelList -File (Resolve-Path ($TestData + "\ReadExcel.xlsx")) -Index 1
		$output.Count | Should be 3
		$output -join "," | Should be "1,2,3"

		$output = Get-ExcelList -File (Resolve-Path ($TestData + "\ReadExcel.xlsx")) -Index 2
		$output.Count | Should be 3
		$output -join "," | Should be "1,1,2"
	}

	It "Reading  Unique List" {
	
		$TestData = (Resolve-Path "..\..\TestData").Path

		$output = Get-ExcelList -File (Resolve-Path ($TestData + "\ReadExcel.xlsx")) -Name Name -Unique
		$output.Count | Should be 3
		$output -join "," | Should be "Ken,Carol,Bob"


		$output = Get-ExcelList -File (Resolve-Path ($TestData + "\ReadExcel.xlsx")) -Name Count -Unique
		$output.Count | Should be 2
		$output -join "," | Should be "1,2"
	
	}
}

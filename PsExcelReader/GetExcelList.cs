using System;
using System.Collections.Generic;
using System.Management.Automation;
using ClosedXML.Excel;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using Errata.Collections;

namespace PsExcelReader
{
    /// <summary>
    /// <para type= "synopsis">Extracts a list from an excel sheet</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "ExcelList")]
    public class GetExcelList : PSCmdlet
    {
        /// <summary>
        /// <para type="description">This is the path to the file(s) of the excel spreadsheets</para>
        /// </summary>
        [Parameter]
        public string[] File { get; set; }


        /// <summary>
        /// <para type="description">This is the Name of the column(s) or columns  in the excel spreadsheet.</para>
        /// <para type="description">This will only work properly if excel spreadsheet is </para>
        /// </summary>
        [Parameter]
        public string[] Name { get; set; } = { };


        /// <summary>
        /// <para type="description">This is the 0 based index of the column(s) to be extracted from the excel spreadsheet.</para>
        /// </summary>
        [Parameter]
        public int[] Index { get; set; } = { };


        /// <summary>
        ///  <para type="description">This flag determines if the values returned from the excel sheet will be unique.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Unique { get; set; }


        /// <summary>
        /// <para type="description">This indicate whether the excel spreadsheet has a header row. The default is that the spreadsheet does.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter HeaderRow { get; set; } = true;

        readonly HashSet<string> uniqueValues = new HashSet<string>();

        protected override void ProcessRecord()
        {

            foreach (var file in File)
            {
                var fInfo = new FileInfo(file);
                DataTable dt = ReadDataTable(fInfo, HeaderRow);
                foreach (var index in Index)
                {
                    var values = dt.ColumnValues<object>(index);
                    foreach (var value in values)
                        WriteValue(value);
                }

                foreach (var name in Name)
                {
                    var values = dt.ColumnValues<object>(name);
                    foreach (var value in values)
                        WriteValue(value);
                }
            }
        }

        private void WriteValue(object value)
        {
            if (Unique.IsPresent)
            {
                if (uniqueValues.Contains(value.ToString())) 
                    return;

                uniqueValues.Add(value.ToString());
                WriteObject(value);
            }
            else
            {
                WriteObject(value);
            }
        }

        private DataTable ReadDataTable(FileInfo fi, bool hasHeader = true)
        {
            using (XLWorkbook workBook = new XLWorkbook(fi.FullName))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                DataTable dt = new DataTable();
                bool firstRow = true;

                if (hasHeader)
                {
                    var row = workSheet.Rows().First();
                    foreach (IXLCell cell in row.Cells())
                        dt.Columns.Add(cell.Value.ToString());
                }
                else
                {
                    var cellcount = workSheet.Rows().First().CellCount();
                    for (int iCol = 0; iCol < cellcount; iCol++)
                        dt.Columns.Add();
                    firstRow = false;
                }

                foreach (IXLRow row in workSheet.Rows())
                {
                    if (firstRow)
                        firstRow = false;
                    else
                    {
                        var newRow = dt.NewRow();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            newRow[i] = cell.Value.ToString();
                            i++;
                        }
                        if (newRow.HasValues())
                            dt.Rows.Add(newRow);
                    }
                }
                return dt;
            }
        }
    }
}

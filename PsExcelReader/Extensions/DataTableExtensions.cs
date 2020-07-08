using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Errata.Collections
{
    public static class DataTableExtensions
    {
        public static Dictionary<TK, List<DataRow>> ColumIndex<TK>(this DataTable dataTable, int columnPosition)
        {
            var rows = from DataRow row in dataTable.Rows
                       select row;

            var columnIndex = rows.ToIndex(r => (TK)r[columnPosition]);
            return columnIndex;
        }

        public static Dictionary<TK, List<DataRow>> ColumIndex<TK>(this DataTable dataTable, string columnName)
        {
            var rows = from DataRow row in dataTable.Rows
                       select row;
            var columnIndex = rows.ToIndex(r => (TK)r[columnName]);
            return columnIndex;
        }

        //public static DataTable GroupBy(this DataTable dataTable, IEnumerable<int> columns)
        //{
        //    var indexes = new  Dictionary<string,>();


        //}

        public static List<T> ColumnValues<T>(this DataTable dataTable, int columnPosition)
        {
            var rows = from DataRow row in dataTable.Rows select row;
            return rows.ColumnValues<T>(columnPosition);
        }

        public static List<T> ColumnValues<T>(this DataTable dataTable, string columnName)
        {
            var rows = from DataRow row in dataTable.Rows select row;
            return rows.ColumnValues<T>(columnName);
        }

        public static TOut ColumnAggregate<TCol, TOut>(this DataTable dataTable, int columnPosition, Func<IEnumerable<TCol>, TOut> aggregator)
        {
            var rows = from DataRow row in dataTable.Rows select row;
            return rows.ColumnAggregate(columnPosition, aggregator);
        }

        public static TOut ColumnAggregate<TCol, TOut>(this DataTable dataTable, string columnName, Func<IEnumerable<TCol>, TOut> aggregator)
        {
            var rows = from DataRow row in dataTable.Rows select row;
            return rows.ColumnAggregate(columnName, aggregator);
        }


        public static DataTable AddColumn<T>(this DataTable dataTable, string columnName, IEnumerable<T> values)
        {
            dataTable.Columns.Add(columnName, typeof(T));
            if (dataTable.Columns.Count == 1)
            {
                foreach (var value in values)
                {
                    var datarow = dataTable.NewRow();
                    datarow[0] = value;
                    dataTable.Rows.Add(datarow);
                }
            }
            else
            {
                var rowcount = dataTable.Rows.Count;
                var valueList = values.ToList().CyclicResize(rowcount);
                for (int i = 0; i < rowcount; i++)
                    dataTable.Rows[i][columnName] = valueList[i];
            }

            return dataTable;
        }
    }
}

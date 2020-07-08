using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Errata.Collections
{
    public static class DataRowExtensions
    {

        public static List<T> ColumnValues<T>(this IEnumerable<DataRow> rows, string columnName)
        {
            var values = new List<T>();
            foreach (DataRow row in rows)
                values.Add((T)row[columnName]);
            return values;
        }

        public static List<T> ColumnValues<T>(this IEnumerable<DataRow> rows, int columnPosition)
        {
            var values = new List<T>();
            foreach (DataRow row in rows)
                values.Add((T)row[columnPosition]);
            return values;
        }

        public static TOut ColumnAggregate<TCol, TOut>(this IEnumerable<DataRow> rows, int columnPosition, Func<IEnumerable<TCol>, TOut> aggregator)
        {
            var columnValues = rows.ColumnValues<TCol>(columnPosition);
            var values = aggregator(columnValues);
            return values;
        }


        public static TOut ColumnAggregate<TCol, TOut>(this IEnumerable<DataRow> rows, string columnName, Func<IEnumerable<TCol>, TOut> aggregator)
        {
            var columnValues = rows.ColumnValues<TCol>(columnName);
            var values = aggregator(columnValues);
            return values;
        }

        public static bool IsEmpty(this DataRow row)
        {
            return row.ItemArray.All(item => item == DBNull.Value);
        }

        public static bool HasValues(this DataRow row)
        {
            return row.ItemArray.Any(item => item != DBNull.Value);
        }


    }
}
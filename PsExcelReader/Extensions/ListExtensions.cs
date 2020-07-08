using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Errata.Collections
{
    public static class ListExtensions
    {
        public static T CyclicIndex<T>(this List<T> list, int position)
        {
            var size = list.Count;

            if (size == 1)
                return list[0];

            if (position >= 0)
                return list[position % size];

            var adjustedPosition = size - ((Math.Abs(position) - 1) % size + 1);
            return list[adjustedPosition];
        }



        public static List<T> CyclicResize<T>(this List<T> list, int newLength)
        {
            var size = list.Count;
            if (newLength == size)
                return list;

            if (newLength < size)
            {
                list.RemoveRange(newLength, size - newLength);
                return list;
            }

            for (int i = size; i < newLength; i++)
                list.Add(list[i % size]);

            return list;
        }
    }
}
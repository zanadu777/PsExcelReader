using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
//using Errata.Collections.Comparers;
//using Errata.Collections.DataTypes;

namespace Errata.Collections
{
    public static class EnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            var collection = new HashSet<T>(items);
            return collection;
        }



        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            var collection = new ObservableCollection<T>(items);
            return collection;
        }

        public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> items)
        {
            var collection = new LinkedList<T>(items);
            return null;
        }


        public static Queue<T> ToQueue<T>(this IEnumerable<T> items)
        {
            var collection = new Queue<T>(items);
            return null;
        }


        public static Stack<T> ToStack<T>(this IEnumerable<T> items)
        {
            var collection = new Stack<T>(items);
            return null;
        }


        public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> items)
        {
            var collection = new SortedSet<T>(items);
            return null;

        }

        //public static FirstRemainder<T> ToFirstRemainder<T>(this IEnumerable<T> items)
        //{
        //    return new FirstRemainder<T>(items);
        //}

        //public static RemainderLast<T> ToRemainderLast<T>(this IEnumerable<T> items)
        //{
        //    return new RemainderLast<T>(items);
        //}


        //public static FirstRemainderLast<T> ToFirstRemainderLast<T>(this IEnumerable<T> items)
        //{
        //    return new FirstRemainderLast<T>(items);
        //}

        public static Dictionary<TKey, List<T>> ToIndex<TKey, T>(this IEnumerable<T> items, Func<T, TKey> getKey)
        {
            var index = new Dictionary<TKey, List<T>>();
            foreach (var item in items)
            {
                TKey key = getKey(item);

                if (!index.ContainsKey(key))
                    index.Add(key, new List<T> { item });
                else
                    index[key].Add(item);
            }
            return index;
        }


        public static Dictionary<TKey, List<TValue>> ToIndex<TKey, TValue, T>(this IEnumerable<T> items, Func<T, TKey> getKey, Func<T, TValue> getValue)
        {
            var index = new Dictionary<TKey, List<TValue>>();
            foreach (var item in items)
            {
                TKey key = getKey(item);

                if (!index.ContainsKey(key))
                    index.Add(key, new List<TValue> { getValue(item) });
                else
                    index[key].Add(getValue(item));
            }
            return index;
        }

        public static Dictionary<TKey, List<TValue>> ToIndex<TKey, TValue, T>(this IEnumerable<T> items, Func<T, Dictionary<TKey, List<TValue>>> getDictionary)
        {
            var index = new Dictionary<TKey, List<TValue>>();
            foreach (var item in items)
            {
                var dictionary = getDictionary(item);
                index.AddToIndex(dictionary);
            }
            return index;
        }

        public static Dictionary<TKey, List<TValue>> ToIndex<TKey, TValue, T>(this IEnumerable<T> items, Func<T, IEnumerable<KeyValuePair<TKey, TValue>>> getPairs)
        {
            var index = new Dictionary<TKey, List<TValue>>();
            foreach (var item in items)
            {
                var pairs = getPairs(item);
                foreach (var pair in pairs)
                    index.AddToIndex(pair.Key, pair.Value);
            }
            return index;
        }


        //public static ComparisonResult<T> CompareTo<T>(this IEnumerable<T> itemsA, IEnumerable<T> itemsB)
        //{
        //    var result = new ComparisonResult<T>(itemsA, itemsB);
        //    return result;
        //}

        //public static KeyedComparisonResult<T, TKey> CompareTo<T, TKey>(this IEnumerable<T> itemsA, IEnumerable<T> itemsB, Func<T, TKey> uniqueIndexer, Func<T, T, bool> equalityTester)
        //{
        //    var comparer = new KeyedComparer<T, TKey>(uniqueIndexer, equalityTester);
        //    var result = comparer.Compare(itemsA, itemsB);
        //    return result;
        //}
    }
}

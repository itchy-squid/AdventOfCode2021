using AdventOfCode.Solutions.DataStructures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Tools
{
    internal static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach(var item in source)
            {
                action(item);
            }
        }

        public static IDictionary<TValue, TKey> ToReverseMapping<TKey, TValue>(this IDictionary<TKey, TValue> dict)
            where TValue : notnull
        {
            return new Dictionary<TValue, TKey>(dict.Select(kvp => new KeyValuePair<TValue, TKey>(kvp.Value, kvp.Key)));
        }

        public static IDefaultDictionary<TKey, TValue> ToDefaultDictionary<T, TKey, TValue>(
            this IEnumerable<T> source, 
            Func<T, TKey> keySelector, 
            Func<T, TValue> valueSelector,
            Func<TValue>? defaultSelector = null) 
            where TKey : struct 
        {
            if(defaultSelector == null)
            {
                defaultSelector = new(() => default);
            }

            IDefaultDictionary<TKey, TValue> dict = new DefaultDictionary<TKey, TValue>(defaultSelector);
            foreach(var element in source)
            {
                dict[keySelector(element)] = valueSelector(element);
            }

            return dict;
        }
    }
}

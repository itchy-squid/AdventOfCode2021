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

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            while (source.Any())
            {

                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }

        public static IDefaultDictionary<TKey, TValue> ToDefaultDictionary<T, TKey, TValue>(
            this IEnumerable<T> source, 
            Func<T, TKey> keySelector, 
            Func<T, TValue> valueSelector) 
            where TKey : struct 
            where TValue : struct
        {
            IDefaultDictionary<TKey, TValue> dict = new DefaultDictionary<TKey, TValue>(() => default);
            foreach(var element in source)
            {
                dict[keySelector(element)] = valueSelector(element);
            }

            return dict;
        }
    }
}

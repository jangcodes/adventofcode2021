using System;
using System.Collections.Generic;

namespace AdventOfCode.Common
{
    public static class EnumerableExtension
    {
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TKey, TValue, TElement>(this IEnumerable<TValue> seq, Func<TValue, TKey> keySelector, Func<TValue, TElement> elementSelector)
            where TKey : notnull
            where TValue : notnull
            where TElement : notnull
        {
            var dict = new SortedDictionary<TKey, TElement>();
            foreach (TValue item in seq)
            {
                dict.Add(keySelector(item), elementSelector(item));
            }

            return dict;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace MapperExtensions.Models
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> LeftJoin<T>(this IEnumerable<T> enumerable1, IEnumerable<T> enumerable2,
            IEqualityComparer<T> equalityComparer)
        {
            var result = enumerable1.Union(enumerable2, equalityComparer).ToList();
            return result;
        }

        public static R Join<T, R>(this IEnumerable<T> enumerable, Func<R, T, R> accFunc, T Separator)
            where R : new()
        {
            var list = enumerable.ToList();
            var seed = accFunc(new R(), list[0]);
            for (int i = 1; i < list.Count; i++)
            {
                seed = accFunc(accFunc(seed, Separator), list[i]);
            }

            return seed;
        }

        public static R Join<T, S, R>(this IEnumerable<T> enumerable, Func<R, T, R> accFunc, Func<R, S, R> sepFunc,
            S Separator)
            where R : new()
        {
            var list = enumerable.ToList();
            var seed = accFunc(new R(), list[0]);
            for (int i = 1; i < list.Count; i++)
            {
                seed = accFunc(sepFunc(seed, Separator), list[i]);
            }

            return seed;
        }
    }
}
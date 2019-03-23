using System;
using System.Collections.Generic;
using System.Linq;

namespace MapperExtensions.Models
{
    public static class IEnumerableExtensions
    {
        public static R JOIN<T, S, R>(this IEnumerable<T> enumerable, Func<R, T, R> accFunc, Func<R, S, R> sepFunc,
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
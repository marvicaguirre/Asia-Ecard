using System;
using System.Collections.Generic;
using System.Linq;

namespace Wizardsgroup.Utilities.Extensions
{
    public static class BetweenExtension
    {
        public static IEnumerable<TSource> Between<TSource, TResult>(this IEnumerable<TSource> source, 
            Func<TSource, TResult> selector,
            TResult lowest, TResult highest) where TResult : IComparable<TResult>
        {
            return source.OrderBy(selector).
                SkipWhile(s => selector.Invoke(s).CompareTo(lowest) < 0).
                TakeWhile(s => selector.Invoke(s).CompareTo(highest) <= 0);
        }
    }
}

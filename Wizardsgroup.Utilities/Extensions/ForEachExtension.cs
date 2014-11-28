using System;
using System.Collections.Generic;
using System.Linq;

namespace Wizardsgroup.Utilities.Extensions
{
    public static class ForEachExtension
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action, Action<ILoopCondition<T>> condition = null)
        {
            ForEach(items.ToArray(), action, condition);
        }

        public static void ForEach<T>(this T[] items, Action<T> action,Action<ILoopCondition<T>> condition = null)
        {
            ICustomIterator<T> iterator = new CustomIterator<T>(items);
            iterator.ForEach(action,condition);
        }
    }
}

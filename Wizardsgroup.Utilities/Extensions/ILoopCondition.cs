using System;

namespace Wizardsgroup.Utilities.Extensions
{
    public interface ILoopCondition<T>
    {
        ILoopCondition<T> ExitWhen(Func<T, bool> exit);
        ILoopCondition<T> SkipWhen(Func<T, bool> skip);
    }
}
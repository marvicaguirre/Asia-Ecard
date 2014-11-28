using System;

namespace Wizardsgroup.Utilities.Extensions
{
    public interface ICustomIterator<T>
    {
        void ForEach(Action<T> action,Action<ILoopCondition<T>> condition);        
    }
}
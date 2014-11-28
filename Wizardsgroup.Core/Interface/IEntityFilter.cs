using System.Collections.Generic;

namespace Wizardsgroup.Core.Interface
{
    public interface IEntityFilter<T> : IEntityBitwiseFilter<T>, IEntityBitwiseConditionalFilter<T>
    {
        IEnumerable<T> ExecuteFilter();
    }
}
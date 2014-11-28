using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Interface
{
    public interface IFluentEntityFilter<T>
    {
        IEntityFilter<T> Filter(Expression<Func<T, bool>> filter);
    }
}

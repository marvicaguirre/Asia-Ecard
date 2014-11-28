using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Interface
{
    public interface IEntityFilterSet<T>
    {
        IEntityFilter<T> FilterSetFor(Expression<Func<T, bool>> anchorFilter, Action<IFluentEntityBitwiseFilter<T>> filterSet);
    }
}
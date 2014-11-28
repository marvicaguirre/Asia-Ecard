using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Interface
{
    public interface IFluentEntityBitwiseFilter<T>
    {
        IFluentEntityBitwiseFilter<T> And(Expression<Func<T, bool>> filter);
        IFluentEntityBitwiseFilter<T> Or(Expression<Func<T, bool>> filter);
    }
}
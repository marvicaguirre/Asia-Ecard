using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Interface
{
    public interface IEntityBitwiseFilter<T>
    {
        IEntityFilter<T> And(Expression<Func<T, bool>> filter);
        IEntityFilter<T> Or(Expression<Func<T, bool>> filter);        
    }
}
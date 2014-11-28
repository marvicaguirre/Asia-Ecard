using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Interface
{
    public interface IExpressionFilter<T>
    {
        Expression<Func<T, bool>> Create(IFilterExpressionNode<T> node);
    }
}
using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Interface
{
    public interface IFilterExpressionNode<T>
    {
        Expression<Func<T, bool>> LeftNode { get; set; }
        Expression<Func<T, bool>> RightNode { get; set; }
        ParameterExpression ParameterExpression { get; set; }
    }
}
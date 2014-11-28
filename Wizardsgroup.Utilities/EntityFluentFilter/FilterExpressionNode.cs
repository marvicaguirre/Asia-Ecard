using System;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Utilities.EntityFluentFilter
{
    public class FilterExpressionNode<T> : IFilterExpressionNode<T>
    {
        public Expression<Func<T, bool>> LeftNode { get; set; }
        public Expression<Func<T, bool>> RightNode { get; set; }
        public ParameterExpression ParameterExpression { get; set; }
    }
}

using System.Linq.Expressions;

namespace Wizardsgroup.Utilities.EntityFluentFilter.ExpressionFilter
{
    public sealed class OrElseExpressionFilter<T> : AbstractBitwiseExpressionFilter<T>
    {
        protected override Expression CombineExpression(Expression leftNode, Expression rightNode)
        {
            return Expression.OrElse(leftNode, rightNode);
        }
    }
}

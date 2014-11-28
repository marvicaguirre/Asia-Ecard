using System.Linq.Expressions;

namespace Wizardsgroup.Utilities.EntityFluentFilter.ExpressionFilter
{
    public sealed class AndExpressionFilter<T> : AbstractBitwiseExpressionFilter<T>
    {
        protected override Expression CombineExpression(Expression leftNode, Expression rightNode)
        {
            return Expression.And(leftNode, rightNode);
        }
    }
}

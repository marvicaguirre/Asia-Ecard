using System;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.EntityFluentFilter.ExpressionFilter
{
    public abstract class AbstractBitwiseExpressionFilter<T> : IExpressionFilter<T>
    {        
        public Expression<Func<T, bool>> Create(IFilterExpressionNode<T> node)
        {
            node.Guard("IFilterExpressionNode must not be null.");
            node.LeftNode.Guard("LeftNode must not be null.");
            node.LeftNode.Guard("RightNode must not be null.");
            node.ParameterExpression.Guard("ParameterExpresion must not be null.");

            ExpressionVisitor visitor = new ExpressionFilterParameterReplacerVisitor(node.RightNode.Parameters, node.LeftNode.Parameters);
            var rightExpressionNode = visitor.Visit(node.RightNode.Body);
            var combineExpression = CombineExpression(node.LeftNode.Body, rightExpressionNode);
            var expression = Expression.Lambda<Func<T, bool>>(combineExpression, node.ParameterExpression);
            return expression;            
        }
        protected abstract Expression CombineExpression(Expression leftNode,Expression rightNode);
    }
}

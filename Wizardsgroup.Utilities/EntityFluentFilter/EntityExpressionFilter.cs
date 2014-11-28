using System;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.EntityFluentFilter.ExpressionFilter;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.EntityFluentFilter
{
    internal sealed class EntityExpressionFilter<T> : IEntityBitwiseFilter<T>, IEntityBitwiseConditionalFilter<T>
    {
        private readonly EntityFilter<T> _parentFilter;
        private readonly ParameterExpression _parentParameterExpression;

        public EntityExpressionFilter(EntityFilter<T> parentFilter, ParameterExpression parentParameterExpression)
        {
            parentParameterExpression.Guard("parentParameterExpression must not be null.");
            parentFilter.Guard("parentFilter must not be null.");
            _parentFilter = parentFilter;
            _parentParameterExpression = parentParameterExpression;
        }
        #region Public Functions/Methods
        public IEntityFilter<T> And(Expression<Func<T, bool>> filter)
        {
            return UpdateFilterExpressionAndReturnParentFilter(() => new AndExpressionFilter<T>(),_parentFilter.FilterExpression, filter);
        }
        public IEntityFilter<T> Or(Expression<Func<T, bool>> filter)
        {
            return UpdateFilterExpressionAndReturnParentFilter(() => new OrExpressionFilter<T>(), _parentFilter.FilterExpression, filter);
        }
        public IEntityFilter<T> AndAlso(Expression<Func<T, bool>> filter)
        {
            return UpdateFilterExpressionAndReturnParentFilter(() => new AndAlsoExpressionFilter<T>(), _parentFilter.FilterExpression, filter);
        }
        public IEntityFilter<T> OrElse(Expression<Func<T, bool>> filter)
        {
            return UpdateFilterExpressionAndReturnParentFilter(() => new OrElseExpressionFilter<T>(), _parentFilter.FilterExpression, filter);
        }
        #endregion

        #region Private Functions/Methods
        IEntityFilter<T> UpdateFilterExpressionAndReturnParentFilter(Expression<Func<IExpressionFilter<T>>> expressionFilterer,
            Expression<Func<T, bool>> parentFilterExpression,
            Expression<Func<T, bool>> newfilter)
        {
            newfilter.Guard("newfilter must not be null.");
            parentFilterExpression.Guard("parentFilterExpression must not be null.");
                        
            var filterNode = CreateInstanceOfFilterExpressionNode(_parentParameterExpression,parentFilterExpression, newfilter);
            var instance = expressionFilterer.Compile()();
            _parentFilter.FilterExpression =  instance.Create(filterNode);
            return _parentFilter;
        }

        private FilterExpressionNode<T> CreateInstanceOfFilterExpressionNode(ParameterExpression parentParameterExpression, Expression<Func<T, bool>> parentFilterExpression, Expression<Func<T, bool>> newfilter)
        {
            var filterNode = new FilterExpressionNode<T>
                {
                    LeftNode = parentFilterExpression,
                    RightNode = newfilter,
                    ParameterExpression = parentParameterExpression,
                };
            return filterNode;
        }

        #endregion
    }
}

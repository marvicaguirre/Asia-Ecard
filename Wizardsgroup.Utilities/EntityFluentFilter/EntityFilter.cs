using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.EntityFluentFilter
{
    internal sealed class EntityFilter<T> : IEntityFilter<T>
    {
        #region Members
        internal Expression<Func<T,bool>> FilterExpression { get; set; }
        internal ParameterExpression ParentParameterExpression { get; set; }
        private readonly IEntityService<T> _service;
        #endregion

        #region Constructor
        public EntityFilter(IEntityService<T> service, Expression<Func<T, bool>> startFilterWith)
        {
            Guard(startFilterWith,service);
            _service = service;
            ParentParameterExpression = startFilterWith.Parameters[0];
            FilterExpression = startFilterWith;
        }

        private void Guard(Expression<Func<T, bool>> startFilterWith,IEntityService<T> service)
        {
            startFilterWith.Guard("startFilter must not be null.");
            service.Guard("service must not be null.");

            if (startFilterWith.Parameters.Count == 0)
                throw new ArgumentException("startFilterWith must have a parameter");
        }

        #endregion

        #region Public Functions,Methods

        /// <summary>
        ///  A bitwise AND operation.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IEntityFilter<T> And(Expression<Func<T, bool>> filter)
        {
            return new EntityExpressionFilter<T>(this, ParentParameterExpression).And(filter);
        }

        /// <summary>
        /// A bitwise OR operation.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IEntityFilter<T> Or(Expression<Func<T, bool>> filter)
        {
            return new EntityExpressionFilter<T>(this, ParentParameterExpression).Or(filter);
        }

        /// <summary>
        /// A conditional AND operation that evaluates the second operand only if the first operand evaluates to true.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>        
        public IEntityFilter<T> AndAlso(Expression<Func<T, bool>> filter)
        {
            return new EntityExpressionFilter<T>(this, ParentParameterExpression).AndAlso(filter);
        }

        /// <summary>
        /// A conditional OR operation that evaluates the second operand only if the first operand evaluates to false.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>        
        public IEntityFilter<T> OrElse(Expression<Func<T, bool>> filter)
        {
            return new EntityExpressionFilter<T>(this, ParentParameterExpression).OrElse(filter);
        }

        public IEnumerable<T> ExecuteFilter()
        {            
            return _service.Filter(FilterExpression);
        }
        #endregion        
    }
}
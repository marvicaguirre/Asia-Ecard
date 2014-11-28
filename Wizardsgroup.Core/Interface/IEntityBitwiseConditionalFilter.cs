using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Interface
{
    public interface IEntityBitwiseConditionalFilter<T>
    {
        /// <summary>
        /// A conditional AND operation that evaluates the second operand only if the first operand evaluates to true.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns> 
        IEntityFilter<T> AndAlso(Expression<Func<T, bool>> filter);
        /// <summary>
        /// A conditional OR operation that evaluates the second operand only if the first operand evaluates to false.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>   
        IEntityFilter<T> OrElse(Expression<Func<T, bool>> filter);
    }
}
using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Service.Specification
{
    [Serializable]
    public sealed class NegateSpecification<T> : ISpecification<T> where T : class, new()
    {
        public NegateSpecification(ISpecification<T> specification)
        {
            var negatedExpression = Expression.Not(specification.IsSatisfiedBy.Body);
            IsSatisfiedBy = Expression.Lambda<Func<T, bool>>(negatedExpression, specification.IsSatisfiedBy.Parameters[0]);
        }

        public Expression<Func<T, bool>> IsSatisfiedBy { get; set; }
    }
}
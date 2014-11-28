using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Service.Specification
{
    public interface ISpecification<T> where T : class,new()
    {
        Expression<Func<T, bool>> IsSatisfiedBy { get; set; }
    }
}
using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Service.Specification
{
    [Serializable]
    public sealed class NullSpecification<T> : ISpecification<T> where T: class,new() 
    {
        public NullSpecification()
        {
            IsSatisfiedBy = arg => true;
        }
        public Expression<Func<T, bool>> IsSatisfiedBy { get; set; }
    }
}

using System;
using System.Linq.Expressions;
using Wizardsgroup.Domain.Base;
using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Service.Specification
{
    [Serializable]
    public class ActiveRecordSpecification<T> : ISpecification<T> where T :AbstractBaseModel, new()
    {
        public ActiveRecordSpecification()
        {            
            IsSatisfiedBy = arg => arg.RecordStatus == RecordStatus.Active;
        }
        public Expression<Func<T, bool>> IsSatisfiedBy { get; set; }
    }
}

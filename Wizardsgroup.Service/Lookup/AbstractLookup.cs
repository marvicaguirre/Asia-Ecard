using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Service.Lookup
{
    public abstract class AbstractLookup<T> : AbstractBaseLookup<T>, ICustomLookup where T : class,new()
    {
        #region Constructor
        protected AbstractLookup(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        #endregion

        #region Public Methods/Function
        public override IEnumerable<ILookupValueField> GetRecordsForLookup()
        {
            var filterSpecification = ResolveSpecification();
            var records = GetRecordsForLookupWorker();
            records = records.Where(filterSpecification.IsSatisfiedBy);
            records = string.IsNullOrEmpty(TextFilter) ? records : records.Where(CreateContainsSearchExpression());
            records = records.Take(1000);
            return GetResult(() => records);
        }

        public override IEnumerable<ILookupValueField> GetRecordsForCascade(int id)
        {
            var filterSpecification = ResolveSpecification().IsSatisfiedBy.Compile();
            return GetResult(() => GetCascadeResultHelper(id).Where(filterSpecification));
        }
        #endregion
    }
}

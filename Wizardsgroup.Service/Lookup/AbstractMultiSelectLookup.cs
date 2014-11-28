using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Service.Lookup
{    
    public abstract class AbstractMultiSelectLookup<T> : AbstractBaseLookup<T>, IMultiSelectLookup where T : class,new()
    {
        #region Constructor
        protected AbstractMultiSelectLookup(IUnitOfWork unitOfWork) : base(unitOfWork)
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

        #region Helper Functions

        protected override IEnumerable<ILookupValueField> GetResult(Func<IEnumerable<T>> getFunction)
        {
            var records = getFunction();
            var result = LookupDataResult(records, LookupConverter.ConvertRecordToLookUp<MultiSelectLookup>);            
            return result;
        }
        #endregion
    }
}

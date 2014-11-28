using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pfizer.Domain.Constants;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service.Attributes;
using Wizardsgroup.Service.Lookup;

namespace Pfizer.Service.Lookup
{
    [EntityLookup(EntityLookupConstant.EmployeeType)]
    public class EmployeeTypeLookup : AbstractLookup<EmployeeType>
    {
        public EmployeeTypeLookup(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override IEnumerable<EmployeeType> GetCascadeResultHelper(int id)
        {
            return GetRecordsForLookupWorker();
        }

        protected override Expression<Func<EmployeeType, string>> GetTextFieldHelper()
        {
            return o => o.Name;
        }

        protected override Expression<Func<EmployeeType, int>> GetValueFieldHelper()
        {
            return o => o.EmployeeTypeId;
        }
    }
}

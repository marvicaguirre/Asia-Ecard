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
    public class CompanyLookup
    {
        [EntityLookup(EntityLookupConstant.Company)]
        public class DepartmentLookup : AbstractLookup<Company>
        {
            public DepartmentLookup(IUnitOfWork unitOfWork)
                : base(unitOfWork)
            {
            }

            protected override IEnumerable<Company> GetCascadeResultHelper(int id)
            {
                return GetRecordsForLookupWorker();
            }

            protected override Expression<Func<Company, string>> GetTextFieldHelper()
            {
                return o => o.Name;
            }

            protected override Expression<Func<Company, int>> GetValueFieldHelper()
            {
                return o => o.CompanyId;
            }
        }
    }
}

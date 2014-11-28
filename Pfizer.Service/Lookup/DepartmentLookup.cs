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
    [EntityLookup(EntityLookupConstant.Department)]
    public class DepartmentLookup : AbstractLookup<Department>
    {
        public DepartmentLookup(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }
        protected override IEnumerable<Department> GetCascadeResultHelper(int id)
        {
            return GetRecordsForLookupWorker();
        }

        protected override Expression<Func<Department, string>> GetTextFieldHelper()
        {
            return o => o.Name;
        }

        protected override Expression<Func<Department, int>> GetValueFieldHelper()
        {
            return o => o.DepartmentId;
        }

    }
}

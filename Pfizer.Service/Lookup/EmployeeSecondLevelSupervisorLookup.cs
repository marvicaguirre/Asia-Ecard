using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service.Attributes;
using Wizardsgroup.Service.Lookup;
using Pfizer.Domain.Constants;
using Pfizer.Domain.Models;

namespace Pfizer.Service.Lookup
{
    [EntityLookup(EntityLookupConstant.EmployeeSecondLevelSupervisor)]
    public class EmployeeSecondLevelSupervisorLookup : AbstractLookup<Employee>
    {
        public EmployeeSecondLevelSupervisorLookup(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        
        }

        protected override IEnumerable<Employee> GetCascadeResultHelper(int id)
        {
            return GetRecordsForLookupWorker().Where(o => o.EmployeeId != id);
        }

        protected override Expression<Func<Employee, string>> GetTextFieldHelper()
        {
            return o => o.FullName;
        }

        protected override Expression<Func<Employee, int>> GetValueFieldHelper()
        {
            return o => o.EmployeeId;
        }

        protected override IQueryable<Employee> GetRecordsForLookupWorker()
        {
            var records = Repository.Query().Filter(o => o.IsSupervisor).GetResult();
            return records;
        }
    }
}
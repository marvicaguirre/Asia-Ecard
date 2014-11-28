using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service.Attributes;
using Wizardsgroup.Service.Lookup;
using Pfizer.Domain.Constants;
using Pfizer.Domain.Models;

namespace Pfizer.Service.Lookup
{
    [EntityLookup(EntityLookupConstant.Employee)]
    public class EmployeeLookup : AbstractLookup<Employee>
    {
        public EmployeeLookup(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        
        }

        protected override IEnumerable<Employee> GetCascadeResultHelper(int id)
        {
            return GetRecordsForLookupWorker();
        }

        protected override Expression<Func<Employee, string>> GetTextFieldHelper()
        {
            return o => o.FullName;
        }

        protected override Expression<Func<Employee, int>> GetValueFieldHelper()
        {
            return o => o.EmployeeId;
        } 
    }
}
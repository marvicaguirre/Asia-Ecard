using System;
using System.Linq;
using System.Linq.Expressions;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
    public class EmployeeTypeService : AbstractEntityService<EmployeeType>
    {
        public EmployeeTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Expression<Func<EmployeeType, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.EmployeeTypeId == id;
        }

        protected override Expression<Func<EmployeeType, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<EmployeeType> OrderBy(IQueryable<EmployeeType> arg)
        {
            return arg.OrderBy(o => o.Name);
        }
    }
}

using System;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;
using Pfizer.Domain.Models;

namespace Pfizer.Service
{
    public class DepartmentService : AbstractEntityService<Department>
    {
        #region Constructor
        public DepartmentService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        #endregion

        #region Function/Method

        protected override Expression<Func<Department, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.DepartmentId == id;
        }

        protected override Expression<Func<Department, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<Department> OrderBy(IQueryable<Department> arg)
        {
            return arg.OrderBy(o => o.Name);
        }
        #endregion
    }
}

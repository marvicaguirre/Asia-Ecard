using System;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
    public class CentralModuleService : AbstractEntityService<CentralModule>
    {
        public CentralModuleService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region Overrides of AbstractEntityService<CentralModule>

        protected override Expression<Func<CentralModule, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.CentralModuleId == id;
        }

        protected override Expression<Func<CentralModule, object>>[] Include()
        {
            return new Expression<Func<CentralModule, object>>[]
                {
                    o => o.CentralFunctions
                };
        }

        protected override IOrderedQueryable<CentralModule> OrderBy(IQueryable<CentralModule> arg)
        {
            return arg.OrderBy(o => o.ModuleName);
        }
        #endregion
    }
}
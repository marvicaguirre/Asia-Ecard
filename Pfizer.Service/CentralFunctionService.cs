using System;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
    public class CentralFunctionService : AbstractEntityService<CentralFunction>
    {
        public CentralFunctionService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region Overrides of AbstractEntityService<CentralFunction>

        protected override Expression<Func<CentralFunction, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.CentralFunctionId == id;
        }

        protected override Expression<Func<CentralFunction, object>>[] Include()
        {
            return new Expression<Func<CentralFunction, object>>[]
                       {
                           o => o.CentralModule
                           , o => o.UserGroupFunctions
                       };
        }

        protected override IOrderedQueryable<CentralFunction> OrderBy(IQueryable<CentralFunction> arg)
        {
            return arg.OrderBy(o => o.CentralModuleId).ThenBy(c => c.DisplayName); //CentralModuleId, ShortDisplayName
        }
        #endregion
    }
}
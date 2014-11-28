using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;
using Pfizer.Domain.Enumerations;
using Pfizer.Domain.Models;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Service
{
    public class SystemSettingService : AbstractEntityService<SystemSetting>
    {

        public SystemSettingService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        protected override Expression<Func<SystemSetting, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.SystemSettingId == id;
        }

        protected override Expression<Func<SystemSetting, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<SystemSetting> OrderBy(IQueryable<SystemSetting> arg)
        {
            return arg.OrderBy(o => o.SystemSettingId);
        }
    }
}

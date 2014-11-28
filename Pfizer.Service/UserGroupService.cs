using System;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;
using Pfizer.Domain.Models;

namespace Pfizer.Service
{
    public class UserGroupService : AbstractEntityService<UserGroup>
    {

        #region Constructor
        public UserGroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        protected override Expression<Func<UserGroup, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.UserGroupId == id;
        }

        protected override Expression<Func<UserGroup, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<UserGroup> OrderBy(IQueryable<UserGroup> arg)
        {
            return arg.OrderBy(o => o.Name);
        }
    }
}

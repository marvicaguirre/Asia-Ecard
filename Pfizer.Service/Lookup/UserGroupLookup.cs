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
    [EntityLookup(EntityLookupConstant.UserGroup)]
    public class UserGroupLookup : AbstractLookup<UserGroup>
    {
        public UserGroupLookup(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override IEnumerable<UserGroup> GetCascadeResultHelper(int id)
        {
            return Repository.Query().GetResult();
        }

        protected override Expression<Func<UserGroup, string>> GetTextFieldHelper()
        {
            return o => o.Name;
        }

        protected override Expression<Func<UserGroup, int>> GetValueFieldHelper()
        {
            return o => o.UserGroupId;
        }
    }
}

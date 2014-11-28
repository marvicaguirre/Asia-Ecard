using System;
using System.Collections.Generic;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Domain.Models;

namespace Pfizer.Service
{
    public interface IUserGroupFunctionService : IEntityService<UserGroupFunction>
    {
        IEnumerable<CentralFunctionEx> GetAssignedFunctionFromGroupId(int userId);
        IEnumerable<CentralFunctionEx> GetUnassignedFunctionFromGroupId(int userId);
        void AssignFunctionToGroup(int userId, int[] userGroupIds);
    }
}
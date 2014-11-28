using System;
using System.Collections.Generic;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;

namespace Pfizer.Service
{
    public interface IUserGroupMapService : IEntityService<UserGroupMap>
    {
        IEnumerable<UserGroup> GetAssignedUserGroupFromUserId(int userId);
        IEnumerable<UserGroup> GetUnassignedUserGroupFromUserId(int userId);
        void AssignUserGroupToUser(int userId, int[] userGroupIds);
    }
}
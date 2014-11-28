using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Enumerations;
using Wizardsgroup.Service;
using Pfizer.Domain.Models;

namespace Pfizer.Service
{
    public class UserGroupMapService : AbstractEntityService<UserGroupMap>, IUserGroupMapService
    {
        private readonly IEntityService<User> _userService;
        private readonly IEntityService<UserGroup> _userGroupService;

        #region Constructor
        public UserGroupMapService(IUnitOfWork unitOfWork) : this(unitOfWork, new UserService(unitOfWork),new UserGroupService(unitOfWork))
        {
            
        }

        public UserGroupMapService(IUnitOfWork unitOfWork,IEntityService<User> userService,IEntityService<UserGroup> userGroupService) : base(unitOfWork)
        {
            _userService = userService;
            _userGroupService = userGroupService;
        }

        #endregion

        protected override Expression<Func<UserGroupMap, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.UserGroupMapId == id;
        }

        protected override Expression<Func<UserGroupMap, object>>[] Include()
        {
            return new Expression<Func<UserGroupMap, object>>[] {o => o.AbstractUserGroup, o => o.AbstractUser};
        }

        protected override IOrderedQueryable<UserGroupMap> OrderBy(IQueryable<UserGroupMap> arg)
        {
            return arg.OrderBy(o => o.UserGroupMapId);
        }

        public IEnumerable<UserGroup> GetAssignedUserGroupFromUserId(int userId)
        {
            var ids = Filter(o => o.UserId == userId).Select(o => o.UserGroupId);
            return _userGroupService.Filter(o => ids.Contains(o.UserGroupId));
        }

        public IEnumerable<UserGroup> GetUnassignedUserGroupFromUserId(int userId)
        {
            var ids = Filter(o => o.UserId == userId).Select(o => o.UserGroupId);
            return _userGroupService.FilterBuilder()
                .Filter(o => !ids.Contains(o.UserGroupId))
                .AndAlso(o=>o.RecordStatus == RecordStatus.Active)
                .ExecuteFilter().ToList();
        }

        public void AssignUserGroupToUser(int userId, int[] userGroupIds)
        {
            if (userGroupIds == null) userGroupIds = new int[] {};

            var parent = _userService.Find(userId);
            parent.UserGroupMaps.RemoveAll(o => o.UserGroupId != default(int));            
            var userGroups = _userGroupService.Filter(o => userGroupIds.Contains(o.UserGroupId));
            parent.UserGroupMaps.AddRange(userGroups.Select(o=> new UserGroupMap
                {
                    UserGroupId = o.UserGroupId,
                    CreatedBy = parent.CreatedBy,
                    CreatedDate = DateTime.Now,                    
                }));

            _userService.Update(parent);
            _userService.Save();
        }
    }
}

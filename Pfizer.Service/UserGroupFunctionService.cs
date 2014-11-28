using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Service;
using Pfizer.Domain.Models;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Service
{
    public class UserGroupFunctionService : AbstractEntityService<UserGroupFunction>, IUserGroupFunctionService
    {
        readonly IEntityService<CentralFunction> _centralFunctionService;
        readonly IEntityService<UserGroup> _userGroupService;

        public UserGroupFunctionService(IUnitOfWork unitOfWork)
            : this(unitOfWork, new CentralFunctionService(unitOfWork), new UserGroupService(unitOfWork))
        {

        }

        public UserGroupFunctionService(IUnitOfWork unitOfWork, IEntityService<CentralFunction> centralFunctionService
                                            , IEntityService<UserGroup> userGroupService)
            : base(unitOfWork)
        {
            _centralFunctionService = centralFunctionService;
            _userGroupService = userGroupService;
        }

        #region Overrides of AbstractEntityService<UserGroupFunction>

        protected override Expression<Func<UserGroupFunction, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.UserGroupFunctionId == id;
        }

        protected override Expression<Func<UserGroupFunction, object>>[] Include()
        {
            return new Expression<Func<UserGroupFunction, object>>[]
                       {
                           o=>o.CentralFunction.CentralModule
                       };
        }

        protected override IOrderedQueryable<UserGroupFunction> OrderBy(IQueryable<UserGroupFunction> arg)
        {
            return arg.OrderBy(o => o.Description);
        }
        #endregion

        public IEnumerable<CentralFunctionEx> GetAssignedFunctionFromGroupId(int usergroupId)
        {
            var ids = EntityRepository.GetAll.Where(o => o.UserGroupId == usergroupId).Select(o => o.CentralFunctionId);
            var retVal = _centralFunctionService.Filter(o => ids.Contains(o.CentralFunctionId));
            return _Convert(retVal);
        }

        public IEnumerable<CentralFunctionEx> GetUnassignedFunctionFromGroupId(int usergroupId)
        {
            var ids = EntityRepository.GetAll.Where(o => o.UserGroupId == usergroupId).Select(o => o.CentralFunctionId);
            var retVal = _centralFunctionService.Filter(o => !ids.Contains(o.CentralFunctionId));
            return _Convert(retVal);
        }

        private IEnumerable<CentralFunctionEx> _Convert(IEnumerable<CentralFunction> source)
        {
            var list = new List<CentralFunctionEx>();
            foreach (var function in source)
            {
                var viewModel = new CentralFunctionEx
                    {
                        CentralFunctionId = function.CentralFunctionId,
                        FunctionName = function.FunctionName,
                        ModuleName = function.CentralModule.ModuleName                        
                    };
                if (function.CentralModule != null)
                {
                    viewModel.FullFunctionName = string.Format("{0}-{1}", function.CentralModule.ModuleName, function.FunctionName);
                }

                list.Add(viewModel);
            }
            return list;
        }

        public void AssignFunctionToGroup(int groupId, int[] functionIds)
        {
            if (functionIds == null) functionIds = new int[] { };

            DeleteUserGroupBaseFromNegatedFunctionIds(groupId, functionIds);

            var existingItems = EntityRepository.GetAll
                .Where(o => o.UserGroupId == groupId)
                .Where(o => o.CentralFunctionId.HasValue)
                .Where(o => functionIds.Contains(o.CentralFunctionId.Value))
                .Select(o => o.CentralFunctionId.Value);

            var cleanedUpIds = functionIds.Where(o => !existingItems.Contains(o));
            var centralFunctions = _centralFunctionService.Filter(o => cleanedUpIds.Contains(o.CentralFunctionId));
            if (centralFunctions.Any())
            {
                var grp = _userGroupService.Find(groupId);
                grp.UserGroupFunctions.AddRange(centralFunctions.Select(o => new UserGroupFunction
                {
                    UserGroupId = groupId,
                    CentralFunctionId = o.CentralFunctionId,
                    CreatedBy = grp.CreatedBy,
                    CreatedDate = DateTime.Now,
                }));    
            }            
            Save();
        }

        private void DeleteUserGroupBaseFromNegatedFunctionIds(int groupId, int[] functionIds)
        {
            var itemsToDelete = EntityRepository.GetAll
                .Where(o => o.UserGroupId == groupId)
                .Where(o => o.CentralFunctionId.HasValue)
                .Where(o => !functionIds.Contains(o.CentralFunctionId.Value))
                .Select(o => o.UserGroupFunctionId);

            itemsToDelete.ForEach(id => Delete(id));
        }
    }
}
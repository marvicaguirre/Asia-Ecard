using System;
using System.Collections.Generic;
using System.Linq;
using Pfizer.Web.Areas.Common.Controllers;
using Pfizer.Web.Areas.Security.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Domain.Models;

namespace Pfizer.Web.Areas.Security.Controllers
{

    public class UserGroupController : AbstractEntryController<UserGroup, UserGroupViewModel>
    {        
        private readonly IEntityService<UserGroup> _userGroupService;


        public UserGroupController(IEntityService<UserGroup> userGroupService)
        {
            _userGroupService = userGroupService;

        }

        protected override IEntityService<UserGroup> GetService()
        {
            return _userGroupService;
        }

        protected override UserGroup AssignViewModelToEntity(UserGroupViewModel viewModel)
        {
            return viewModel.Convert<UserGroupViewModel, UserGroup>();
        }
        protected override UserGroupViewModel AssignEntityToViewModel(UserGroup entity)
        {
            return entity.Convert<UserGroup, UserGroupViewModel>();
        }
        protected override IEnumerable<UserGroupViewModel> GetModelRecordsToBindInGrid()
        {
            return _userGroupService.GetAll().Select(o => o.Convert<UserGroup, UserGroupViewModel>());
        }
        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _userGroupService.Filter(o => checkedRecords.Contains(o.UserGroupId))
                              select new
                              {
                                  GroupName = data.Name, data.Description
                              };

            return displayData;
        }

        protected override void SetViewBagsForCreate(int? id)
        {
            ViewBagParentId = (id == null ? 0 : (int)id);
        }

        protected override void SetViewBagsForEdit(int? id)
        {

        }
        protected override string GetIndexViewTitle()
        {
            return "User Group";
        }

        protected override bool IsIndexPartialView()
        {
            return false;
        }
        protected override void SetViewBagForIndexView(int? id)
        {
            ViewBagParentId = (id == null ? 0 : (int)id);
        }


    }
}

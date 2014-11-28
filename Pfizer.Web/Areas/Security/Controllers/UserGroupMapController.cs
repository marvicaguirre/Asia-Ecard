using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pfizer.Repository;
using Pfizer.Service;
using Pfizer.Web.Areas.Common.Controllers;
using Pfizer.Web.Areas.Security.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Core.Web.Helpers;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Domain.Models;

namespace Pfizer.Web.Areas.Security.Controllers
{

    public class UserGroupMapController : AbstractEntryController<UserGroupMap, UserGroupMapViewModel>
    {
        private readonly IUserGroupMapService _userGroupMapService;        

        public UserGroupMapController(IUserGroupMapService userGroupMapService)
        {
            _userGroupMapService = userGroupMapService;
        }

        protected override IEntityService<UserGroupMap> GetService()
        {
            return _userGroupMapService;
        }

        protected override UserGroupMap AssignViewModelToEntity(UserGroupMapViewModel viewModel)
        {
            return null;
        }
        protected override UserGroupMapViewModel AssignEntityToViewModel(UserGroupMap entity)
        {
            return null;
        }
        protected override IEnumerable<UserGroupMapViewModel> GetModelRecordsToBindInGrid()
        {
            var id = ViewBagParentId.ToInteger();
            var records = _userGroupMapService.Filter(m => m.AbstractUser.UserId == id)
                .Select(o=> new UserGroupMapViewModel
                {
                    UserGroupMapId = o.UserGroupMapId,
                    UserGroupName = o.AbstractUserGroup.Name,
                    UserGroupDesc = o.AbstractUserGroup.Description
                });

            return records;
        }
        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _userGroupMapService.Filter(o => checkedRecords.Contains(o.UserGroupMapId))
                              select new
                              {
                                  GroupName = data.AbstractUserGroup.Name
                              };

            return displayData;
        }



        protected override void SetViewBagsForCreate(int? id)
        {

        }
        protected override void SetViewBagsForEdit(int? id)
        {

        }
        protected override string GetIndexViewTitle()
        {
            var userService = new UserService(new UnitOfWorkWrapper());
            var user = userService.Find(ParentEntityId.ToInteger());
            return string.Format("{0} - User Group Map", user.UserName);
        }

        protected override bool IsIndexPartialView()
        {
            return false;
        }
        protected override void SetViewBagForIndexView(int? id)
        {

        }

        public ActionResult AssignUserGroup(int id)
        {
            var result = new DualListboxHelper<UserGroup>()
                .DualListboxFluentBuilder()
                .AssignParentId(id)
                .AssignFunctionForUnassignedRecord(() => _userGroupMapService.GetUnassignedUserGroupFromUserId(id))
                .AssignFunctionForAssignedRecord(()=>_userGroupMapService.GetAssignedUserGroupFromUserId(id))
                .AssignTextField(o => o.Name)
                .AssignValueField(o => o.UserGroupId)
                .AssignSaveAction(this,SaveRecord)
                .BuildDualListBox()
                .RenderView();
            return result;  
        }

        public ActionResult SaveRecord(int parentId,int[] ids)
        {
            _userGroupMapService.AssignUserGroupToUser(parentId,ids);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

    }
}


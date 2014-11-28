using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pfizer.Repository;
using Pfizer.Service;
using Pfizer.Web.Areas.Common.Controllers;
using Pfizer.Web.Areas.Security.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Core.Web.Helpers;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Security.Controllers
{
    public class UserGroupFunctionController : AbstractEntryController<UserGroupFunction, UserGroupFunctionViewModel>
    {
        private readonly IUserGroupFunctionService _service;
        private string _indexViewTitle;
        public UserGroupFunctionController(IUserGroupFunctionService userGroupFunctionService)
        {
            _service = userGroupFunctionService;
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _service.Filter(o => checkedRecords.Contains(o.UserGroupFunctionId))
                              select new
                              {
                                  data.CentralFunction.FunctionName, data.CentralFunction.CentralModule.ModuleName
                              };
            return displayData;
        }

        protected override IEntityService<UserGroupFunction> GetService()
        {
            return _service;
        }

        protected override IQueryable<UserGroupFunctionViewModel> GetModelRecordsToBindInGrid2()
        {
            var theParentId = ParentEntityId.ToInteger();
            var unitOfWork = new UnitOfWorkWrapper();
            var model = unitOfWork.Repository<UserGroupFunction>().GetAllIncluding(o => o.CentralFunction.CentralModule)
                .Where(c => c.UserGroupId == (theParentId == 0 ? c.UserGroupId : theParentId))
                .Select(source => new UserGroupFunctionViewModel
                {
                    UserGroupFunctionId = source.UserGroupFunctionId,
                    ModuleName = source.CentralFunction.CentralModule.ModuleName,
                    FunctionName = source.CentralFunction.FunctionName,
                    CentralFunctionId = source.CentralFunction.CentralFunctionId,
                    UserGroupId = source.UserGroupId
                });
            //var model = _service.Filter(c => c.UserGroupId == (theParentId == 0 ? c.UserGroupId : theParentId))
            //    .Select(source => new UserGroupFunctionViewModel
            //    {
            //        UserGroupFunctionId = source.UserGroupFunctionId,
            //        ModuleName = source.CentralFunction.CentralModule.ModuleName,
            //        FunctionName = source.CentralFunction.FunctionName,
            //        CentralFunctionId = source.CentralFunction.CentralFunctionId,
            //        UserGroupId = source.UserGroupId
            //    });
            //.OrderBy(c => c.CentralFunction.FunctionName)
            //.Select(AssignEntityToViewModel);

            return model;
        }

        protected override IEnumerable<UserGroupFunctionViewModel> GetModelRecordsToBindInGrid()
        {
            var theParentId = ParentEntityId.ToInteger();
            var unitOfWork = new UnitOfWorkWrapper();
            var model = unitOfWork.Repository<UserGroupFunction>().GetAllIncluding(o => o.CentralFunction.CentralModule)
                .Where(c => c.UserGroupId == (theParentId == 0 ? c.UserGroupId : theParentId))
                .Select(source => new UserGroupFunctionViewModel
                {
                    UserGroupFunctionId = source.UserGroupFunctionId,
                    ModuleName = source.CentralFunction.CentralModule.ModuleName,
                    FunctionName = source.CentralFunction.FunctionName,
                    CentralFunctionId = source.CentralFunction.CentralFunctionId,
                    UserGroupId = source.UserGroupId
                });
            //var model = _service.Filter(c => c.UserGroupId == (theParentId == 0 ? c.UserGroupId : theParentId))
            //    .Select(source => new UserGroupFunctionViewModel
            //    {
            //        UserGroupFunctionId = source.UserGroupFunctionId,
            //        ModuleName = source.CentralFunction.CentralModule.ModuleName,
            //        FunctionName = source.CentralFunction.FunctionName,
            //        CentralFunctionId = source.CentralFunction.CentralFunctionId,
            //        UserGroupId = source.UserGroupId
            //    });
                //.OrderBy(c => c.CentralFunction.FunctionName)
                //.Select(AssignEntityToViewModel);

            return model;
        }

        protected override UserGroupFunction AssignViewModelToEntity(UserGroupFunctionViewModel model)
        {
            return model.Convert<UserGroupFunctionViewModel, UserGroupFunction>();
        }

        protected override UserGroupFunctionViewModel AssignEntityToViewModel(UserGroupFunction entity)
        {
            return entity.Convert<UserGroupFunction, UserGroupFunctionViewModel>((source, model) =>
            {
                if (source.CentralFunction == null) return;

                model.ModuleName = source.CentralFunction.CentralModule.ModuleName;
                model.FunctionName = source.CentralFunction.FunctionName;
                model.CentralFunctionId = source.CentralFunction.CentralFunctionId;
            });
        }

        protected override string GetIndexViewTitle()
        {
            return _indexViewTitle;
        }

        protected override bool IsIndexPartialView()
        {
            return false;
        }

        protected override void SetViewBagForIndexView(int? id)
        {            
            if (id.HasValue)
            {
                var svc = new UserGroupService(new UnitOfWorkWrapper());
                ViewBag.ParentId = id;
                _indexViewTitle = svc.Find(id.Value).Name + " - Functions";
                ViewBag.UserGroupId = id;
            }
        }

        protected override void SetViewBagsForCreate(int? id)
        {
        }

        protected override void SetViewBagsForEdit(int? id)
        {

        }

        public ActionResult AssignGroupFunction(int id)
        {
            var result = new DualListboxHelper<CentralFunctionEx>()
                .DualListboxFluentBuilder()
                .AssignParentId(id)
                .AssignFunctionForUnassignedRecord(() => _service.GetUnassignedFunctionFromGroupId(id))
                .AssignFunctionForAssignedRecord(() => _service.GetAssignedFunctionFromGroupId(id))
                .AssignTextField(o => o.FullFunctionName)
                .AssignValueField(o => o.CentralFunctionId)
                .AssignSaveAction(this, SaveRecord)
                .BuildDualListBox()
                .RenderView();
            return result;
        }

        public ActionResult SaveRecord(int parentId, int[] ids)
        {
            _service.AssignFunctionToGroup(parentId, ids);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
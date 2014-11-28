using System.Collections.Generic;
using System.Linq;
using Pfizer.Repository;
using Pfizer.Service;
using Pfizer.Web.Areas.Common.ViewModels;
using System;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Domain.Models;
using System.Web.Mvc;
using Wizardsgroup.Domain.Lookup;


namespace Pfizer.Web.Areas.Common.Controllers
{
    public class DepartmentController : AbstractEntryController<Department, DepartmentViewModel>
    {
        readonly IEntityService<Department> _service = new DepartmentService(new UnitOfWorkWrapper());

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _service.Filter(o => checkedRecords.Contains(o.DepartmentId))
                              select new
                              {
                                  Department = data.Name,
                                  Description = data.Description,
                                  Status = data.Status
                              };
            return displayData;
        }

        protected override IEntityService<Department> GetService()
        {
            return _service;
        }

        protected override IEnumerable<DepartmentViewModel> GetModelRecordsToBindInGrid()
        {
            return _service.GetAll().Select(o => o.Convert<Department, DepartmentViewModel>());
        }

        protected override Department AssignViewModelToEntity(DepartmentViewModel model)
        {
            if (model.Name != null && model.Description != null)
            {
                model.Name = model.Name.Trim();
                model.Description = model.Description.Trim();   
            }
            return model.Convert<DepartmentViewModel, Department>();
        }

        protected override DepartmentViewModel AssignEntityToViewModel(Department entity)
        {
            return entity.Convert<Department, DepartmentViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Department";
        }

        protected override bool IsIndexPartialView()
        {
            return false;
        }

        protected override void SetViewBagForIndexView(int? id)
        {

        }

        protected override void SetViewBagsForCreate(int? id)
        {

        }

        protected override void SetViewBagsForEdit(int? id)
        {

        }

        public ActionResult GetDepartments()
        {
            var lookupData = DepartmentLookup();

            return Json(lookupData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDepartmentsWithWildcard()
        {
            var lookupData = DepartmentLookup(true);

            return Json(lookupData, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<LookupData> DepartmentLookup(bool includeWildcard = false)
        {
            IEntityService<Department> departmentService = new DepartmentService(new UnitOfWorkWrapper());

            var departments = new List<LookupData>();

            if (includeWildcard)
            {
                departments.Add(LookupData.Create("All", Guid.Empty.ToString()));
            }

            foreach (var department in _service.FilterActive())
            {
                departments.Add(LookupData.Create(department.Name, department.DepartmentId.ToString()));
            }

            return departments;
        }
    }
}
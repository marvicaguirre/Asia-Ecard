using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pfizer.Repository;
using Pfizer.Service;
using Pfizer.Web.Areas.Common.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service.Lookup;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Domain.Models;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class EmployeeController : AbstractEntryController<Employee, EmployeeViewModel>
    {
        readonly IEntityService<Employee> _employeeService = new EmployeeService(new UnitOfWorkWrapper());

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _employeeService.Filter(o => checkedRecords.Contains(o.EmployeeId))
                              select new
                              {
                                  FullName = data.FullName,
                                  CompanyName = data.Company.Name,
                                  TelephoneNo = data.TelephoneNo,
                                  //Status = data.Status,
                                  EmployeeTypes = (data.EmployeeType != null ? data.EmployeeType.Name : String.Empty),
                                  IsSupervisor = data.IsSupervisor,
                                  //MobileNo=data.MobileNo,
                                  //Email=data.Email,
                                  Department = (data.Department != null ? data.Department.Name : String.Empty)
                              };
            return displayData;
        }

        protected override IEntityService<Employee> GetService()
        {
            return _employeeService;
        }

        protected override IEnumerable<EmployeeViewModel> GetModelRecordsToBindInGrid()
        {
            var employees = _employeeService.GetAll().Select(o => o.Convert<Employee, EmployeeViewModel>());
            return employees;
        }

        protected override Employee AssignViewModelToEntity(EmployeeViewModel model)
        {
            if (model.LastName != null && model.FirstName != null && model.MiddleName != null)
            {
                model.LastName = model.LastName.Trim();
                model.FirstName = model.FirstName.Trim();
                model.MiddleName = model.MiddleName.Trim();
            }
            return model.Convert<EmployeeViewModel, Employee>();
        }

        protected override EmployeeViewModel AssignEntityToViewModel(Employee entity)
        {
            return entity.Convert<Employee, EmployeeViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Employee";
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

        public ActionResult GetEmployeeType()
        {
            var lookupResult = new EnumLookup().LookupFluentBuilder<EmployeeType>()
                .SetFlagValueAsValueField()
                .SetFlagNameAsValueField()
                .SplitCamelCaseInTextField()
                .GenerateLookupData()
                .GetLookup();

            return Json(lookupResult, JsonRequestBehavior.AllowGet);
        }
    }
}
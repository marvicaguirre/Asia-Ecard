using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pfizer.Repository;
using Pfizer.Web.Areas.Common.ViewModels;
using System;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Service;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class RuleDatastoreController : AbstractEntryController<RuleDatastore,RuleDatastoreViewModel>
    {
        private readonly RuleService _ruleService = new RuleService(new UnitOfWorkWrapper());

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            return _ruleService.Filter(o => checkedRecords.Contains(o.RuleDatastoreId))
                               .Select(o=>new
                                   {
                                       o.Controller,
                                       o.ControllerAction,
                                       o.Field
                                   });
        }

        protected override IEntityService<RuleDatastore> GetService()
        {
            return _ruleService;
        }

        protected override IEnumerable<RuleDatastoreViewModel> GetModelRecordsToBindInGrid()
        {
            return _ruleService.GetAll().Select(o => o.Convert<RuleDatastore, RuleDatastoreViewModel>());
        }

        protected override RuleDatastore AssignViewModelToEntity(RuleDatastoreViewModel viewModel)
        {
            return viewModel.Convert<RuleDatastoreViewModel, RuleDatastore>();
        }

        protected override RuleDatastoreViewModel AssignEntityToViewModel(RuleDatastore entity)
        {
            return entity.Convert<RuleDatastore, RuleDatastoreViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Rules";
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
            ViewBag.RuleOperator = new SelectList(_ruleService.GetAvailableRules(), "RuleId", "RuleOperation");
        }

        protected override void SetViewBagsForEdit(int? id)
        {

        }

        public ActionResult GetAvalaibleRules()
        {
            var listOfRules = _ruleService.GetAvailableRules();            
            return Json(listOfRules, JsonRequestBehavior.AllowGet); 
        }

    }
}
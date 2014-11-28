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
    public class DataDictionaryController : AbstractEntryController<DataDictionary,DataDictionaryViewModel>
    {
        private readonly DataDictionaryService _dataDictionary = new DataDictionaryService(new UnitOfWorkWrapper());

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var dispayData = _dataDictionary.Filter(o => checkedRecords.Contains(o.DataDictionaryId))
                           .Select(o => new
                               {
                                   o.Model,
                                   o.FieldName
                               });
            return dispayData;
        }

        protected override IEntityService<DataDictionary> GetService()
        {
            return _dataDictionary;
        }

        protected override IEnumerable<DataDictionaryViewModel> GetModelRecordsToBindInGrid()
        {
            return _dataDictionary.GetAll().Select(o => o.Convert<DataDictionary, DataDictionaryViewModel>());
        }

        protected override DataDictionary AssignViewModelToEntity(DataDictionaryViewModel viewModel)
        {
            return viewModel.Convert<DataDictionaryViewModel, DataDictionary>();
        }

        protected override DataDictionaryViewModel AssignEntityToViewModel(DataDictionary entity)
        {
            return entity.Convert<DataDictionary, DataDictionaryViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Data Dictionary";
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

        [HttpGet]
        public ActionResult GetDataDictionary(string entityName)
        {
            var displaydata = _dataDictionary.GetDictionaryFromModel(string.Format("{0}ViewModel",entityName))                
                .Select(datadict => new
                {
                    datadict.FieldName,
                    datadict.FieldDisplayText,
                });
            return Json(displaydata, JsonRequestBehavior.AllowGet);
        }
    }
}
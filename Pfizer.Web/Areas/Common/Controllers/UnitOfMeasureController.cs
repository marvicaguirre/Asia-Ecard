using System.Collections.Generic;
using System.Linq;
using Pfizer.Web.Areas.Common.ViewModels;
using System;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Domain.Models;


namespace Pfizer.Web.Areas.Common.Controllers
{
    public class UnitOfMeasureController : AbstractEntryController<UnitOfMeasure, UnitOfMeasureViewModel>
    {
        private readonly IEntityService<UnitOfMeasure> _service;
        public UnitOfMeasureController(IEntityService<UnitOfMeasure> service)
        {
            _service = service;
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {

            var displayData = from data in _service.Filter(o => checkedRecords.Contains(o.UnitOfMeasureId))
                              select new
                              {
                                  Unit_of_Measure = data.Name, data.Description
                              };
            return displayData;
        }

        protected override IEntityService<UnitOfMeasure> GetService()
        {
            return _service;
        }

        protected override IEnumerable<UnitOfMeasureViewModel> GetModelRecordsToBindInGrid()
        {
            return _service.GetAll().Select(o => o.Convert<UnitOfMeasure, UnitOfMeasureViewModel>());
        }

        protected override UnitOfMeasure AssignViewModelToEntity(UnitOfMeasureViewModel viewModel)
        {
            if (viewModel.Name != null && viewModel.Description != null)
            {
                viewModel.Name = viewModel.Name.Trim();
                viewModel.Description = viewModel.Description.Trim();
            }
            return viewModel.Convert<UnitOfMeasureViewModel, UnitOfMeasure>();
        }

        protected override UnitOfMeasureViewModel AssignEntityToViewModel(UnitOfMeasure entity)
        {
            return entity.Convert<UnitOfMeasure, UnitOfMeasureViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Unit Of Measure";
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
    }
}
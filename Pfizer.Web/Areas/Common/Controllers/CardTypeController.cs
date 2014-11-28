using System;
using System.Collections.Generic;
using System.Linq;
using Pfizer.Domain.Models;
using Pfizer.Web.Areas.Common.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class CardTypeController : AbstractEntryController<CardType, CardTypeViewModel>
    {
        private readonly IEntityService<CardType> _service;
        private readonly IEntityService<Class> _classService;

        public CardTypeController(IEntityService<CardType> service, IEntityService<Class> classService)
        {
            _service = service;
            _classService = classService;
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _service.Filter(o => checkedRecords.Contains(o.CardTypeId))
                              select new
                              {
                                  Card_Type = data.Name, data.Description,
                              };

            return displayData;
        }

        protected override IEntityService<CardType> GetService()
        {
            return _service;
        }

        protected override IEnumerable<CardTypeViewModel> GetModelRecordsToBindInGrid()
        {
            var id = ParentEntityId.ToInteger();            
            return _service.Filter(o => o.ClassId.Equals(id)).Select(o => o.Convert<CardType, CardTypeViewModel>());
        }

        protected override CardType AssignViewModelToEntity(CardTypeViewModel viewModel)
        {
            if (viewModel.Name != null && viewModel.Description != null)
            {
                viewModel.Name = viewModel.Name.Trim();
                viewModel.Description = viewModel.Description.Trim();
            }

            return viewModel.Convert<CardTypeViewModel,CardType>();
        }

        protected override CardTypeViewModel AssignEntityToViewModel(CardType entity)
        {
            return entity.Convert<CardType, CardTypeViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Card Type";
        }

        protected override bool IsIndexPartialView()
        {
            return false;
        }

        protected override void SetViewBagForIndexView(int? id)
        {
            SetViewBags();
        }

        protected override void SetViewBagsForCreate(int? id)
        {
            SetViewBags();
        }

        protected override void SetViewBagsForEdit(int? id)
        {
            SetViewBags();
        }

        private void SetViewBags()
        {
            var classItem = _classService.Find(ParentEntityId.ToInteger());
            if (classItem != null)
            {
                ViewBag.ClassName = classItem.Name;
            }
            ViewBag.ClassId = ParentEntityId.ToString();
        }
    }
}
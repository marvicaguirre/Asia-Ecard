using System.Collections.Generic;
using System.Linq;
using Pfizer.Web.Areas.Common.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Domain.Models;


namespace Pfizer.Web.Areas.Common.Controllers
{
    public class ProgramPerCardTypeController : AbstractEntryController<Program, ProgramViewModel>
    {
        private readonly IEntityService<Program> _service;
        private readonly IEntityService<CardType> _cardTypeService;

        public ProgramPerCardTypeController(IEntityService<Program> service, IEntityService<CardType> cardTypeService)
        {
            _service = service;
            _cardTypeService = cardTypeService;
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            return null;
        }

        protected override IEntityService<Program> GetService()
        {
            return _service;
        }

        protected override IEnumerable<ProgramViewModel> GetModelRecordsToBindInGrid()
        {
            var cardTypeId = ParentEntityId.ToInteger();
            return _service.Filter(o=>o.CardTypeId == cardTypeId).Select(o => o.Convert<Program, ProgramViewModel>());
        }

        protected override Program AssignViewModelToEntity(ProgramViewModel viewModel)
        {
            if (viewModel.Name != null && viewModel.Description != null)
            {
                viewModel.Name = viewModel.Name.Trim();
                viewModel.Description = viewModel.Description.Trim();
            }
            return viewModel.Convert<ProgramViewModel, Program>();
        
        }

        protected override ProgramViewModel AssignEntityToViewModel(Program entity)
        {
            return entity.Convert<Program, ProgramViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Program";
        }

        protected override bool IsIndexPartialView()
        {
            return false;
        }

        protected override void SetViewBagForIndexView(int? id)
        {
            var cardTypeId = ParentEntityId.ToInteger();
            var cardTypeItem = _cardTypeService.Find(cardTypeId);

            if (cardTypeItem != null)
            {
                ViewBag.CardTypeName = cardTypeItem.Name;
            }
        }

        protected override void SetViewBagsForCreate(int? id)
        {
        }

        protected override void SetViewBagsForEdit(int? id)
        {
        }
    }
}
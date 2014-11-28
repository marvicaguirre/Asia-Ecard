using System.Collections.Generic;
using System.Linq;
using Pfizer.Domain.Models;
using Pfizer.Web.Areas.Common.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class CardPrefixController : AbstractEntryController<CardPrefix, CardPrefixViewModel>
    {
        private readonly IEntityService<CardPrefix> _cardPrefixService;
        private readonly IEntityService<Program> _programService;

        public CardPrefixController(IEntityService<CardPrefix> cardPrefixService,IEntityService<Program> programService)
        {
            _cardPrefixService = cardPrefixService;
            _programService = programService;
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            return _cardPrefixService.Filter(o => checkedRecords.Contains(o.CardPrefixId))
                .Select(o => new
                {
                    Prefix = o.Name
                });
        }

        protected override IEntityService<CardPrefix> GetService()
        {
            return _cardPrefixService;
        }

        protected override IEnumerable<CardPrefixViewModel> GetModelRecordsToBindInGrid()
        {
            var programId = ParentEntityId.ToInteger();
            return _cardPrefixService.Filter(o => o.ProgramId == programId)
                .Select(o => o.Convert<CardPrefix, CardPrefixViewModel>());
        }

        protected override CardPrefix AssignViewModelToEntity(CardPrefixViewModel viewModel)
        {
            return viewModel.Convert<CardPrefixViewModel, CardPrefix>();
        }

        protected override CardPrefixViewModel AssignEntityToViewModel(CardPrefix entity)
        {
            return entity.Convert<CardPrefix, CardPrefixViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            var programId = ParentEntityId.ToInteger();
            var program = _programService.Find(programId);
            return string.Format("{0} - Prefix", program.Name);
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
            var programId = ParentEntityId.ToInteger();
            var program = _programService.Find(programId);
            ViewBag.ProgramName = program.Name;
            ViewBag.ProgramId = programId;
        }

        protected override void SetViewBagsForEdit(int? id)
        {
            
        }
    }
}
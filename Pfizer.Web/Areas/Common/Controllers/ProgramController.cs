using System.Collections.Generic;
using System.Linq;
using Pfizer.Domain.Models;
using Pfizer.Web.Areas.Common.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class ProgramController : AbstractEntryController<Program, ProgramViewModel>
    {
        private readonly IEntityService<Program> _service;

        public ProgramController(IEntityService<Program> service)
        {
            _service = service;
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            return _service.Filter(o => checkedRecords.Contains(o.ProgramId))
                .Select(o => new
                {
                    Program = o.Name,
                    o.Description,
                    Card_Type = o.CardType.Name
                });
        }

        protected override IEntityService<Program> GetService()
        {
            return _service;
        }

        protected override IEnumerable<ProgramViewModel> GetModelRecordsToBindInGrid()
        {
            return _service.GetAll().Select(o => o.Convert<Program, ProgramViewModel>());
        }

        protected override Program AssignViewModelToEntity(ProgramViewModel viewModel)
        {
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
            
        }

        protected override void SetViewBagsForCreate(int? id)
        {
            
        }

        protected override void SetViewBagsForEdit(int? id)
        {
            
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pfizer.Domain.Models;
using Pfizer.Service;
using Pfizer.Web.Areas.Common.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Core.Web.Helpers;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class ProgramProductMappingController : AbstractEntryController<ProgramProductMapping, ProgramProductMappingViewModel>
    {
        private readonly IProgramProductMappingService _service;
        private readonly IEntityService<Program> _programService;

        public ProgramProductMappingController(IProgramProductMappingService service, IEntityService<Program> programService)
        {
            _service = service;
            _programService = programService;
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            return _service.Filter(o => checkedRecords.Contains(o.ProgramProductMappingId))
                .Select(o => new
                {
                    o.Program.Name
                });
        }

        protected override IEntityService<ProgramProductMapping> GetService()
        {
            return _service;
        }

        protected override IEnumerable<ProgramProductMappingViewModel> GetModelRecordsToBindInGrid()
        {
            var parentId = ParentEntityId.ToInteger();
            return _service.Filter(o => o.ProgramId == parentId)
                .Select(o => new ProgramProductMappingViewModel
                {
                    ProgramProductMappingId = o.ProgramProductMappingId,
                    ProductName = o.Product.Name,
                    Status = o.Status
                });
        }

        protected override ProgramProductMapping AssignViewModelToEntity(ProgramProductMappingViewModel viewModel)
        {
            return null;
        }

        protected override ProgramProductMappingViewModel AssignEntityToViewModel(ProgramProductMapping entity)
        {
            return null;
        }

        protected override string GetIndexViewTitle()
        {
            var programId = ParentEntityId.ToInteger();
            var program = _programService.Find(programId);
            return string.Format("{0} - Products",program.Name);
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

        public ActionResult Assign(int id)
        {
            var result = new DualListboxHelper<Product>()
                .DualListboxFluentBuilder()
                .AssignParentId(id)
                .AssignFunctionForUnassignedRecord(() => _service.GetUnassignedProductFromProgramId())
                .AssignFunctionForAssignedRecord(() => _service.GetAssignedProductFromProgramId(id))
                .AssignTextField(o => o.Name)
                .AssignValueField(o => o.ProductId)
                .AssignSaveAction(this, SaveRecord)
                .BuildDualListBox()
                .RenderView();
            return result;
        }

        public ActionResult SaveRecord(int parentId, int[] ids)
        {
            _service.AssignProductToProgram(parentId, ids,SessionContainer.UserName);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
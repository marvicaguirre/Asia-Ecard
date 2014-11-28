using System.Collections.Generic;
using System.Linq;
using Pfizer.Domain.Models;
using Pfizer.Web.Areas.Common.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class DosageController : AbstractEntryController<Dosage, DosageViewModel>
    {
        readonly IEntityService<Dosage> _dosageService;
        readonly IEntityService<Product> _productService;

        public DosageController(IEntityService<Dosage> dosageService, IEntityService<Product> productService)
        {
            _dosageService = dosageService;
            _productService = productService;
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _dosageService.Filter(o => checkedRecords.Contains(o.DosageId))
                              select new
                              {
                                  Dosage_Id = data.UniqueId,
                                  Dosage_Form = data.Name
                              };
            return displayData;
        }

        protected override IEntityService<Dosage> GetService()
        {
            return _dosageService;
        }

        protected override IEnumerable<DosageViewModel> GetModelRecordsToBindInGrid()
        {
            var id = ParentEntityId.ToInteger();
            var dosageItems = _dosageService.Filter(o=>o.ProductId == id).Select(o => o.Convert<Dosage, DosageViewModel>());
            return dosageItems;
        }

        protected override Dosage AssignViewModelToEntity(DosageViewModel viewModel)
        {
            if (viewModel.Name != null)
            {
                viewModel.Name = viewModel.Name.Trim();
            }
            return viewModel.Convert<DosageViewModel, Dosage>();
        }

        protected override DosageViewModel AssignEntityToViewModel(Dosage entity)
        {
            return entity.Convert<Dosage, DosageViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Dosage Form";
        }

        protected override bool IsIndexPartialView()
        {
            return false;
        }

        protected override void SetViewBagForIndexView(int? id)
        {
            SetViewBag();
        }

        protected override void SetViewBagsForCreate(int? id)
        {
            SetViewBag();
        }

        protected override void SetViewBagsForEdit(int? id)
        {
            SetViewBag();
        }
        private void SetViewBag()
        {
            var productId = ParentEntityId.ToInteger();
            var productItem = _productService.Find(productId);
            if (productItem != null)
            {
                ViewBag.ProductName = productItem.Name;
            }
            ViewBag.ProductId = ParentEntityId.ToString();
        }
    }
}
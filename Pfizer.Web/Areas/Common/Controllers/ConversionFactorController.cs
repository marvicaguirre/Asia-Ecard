using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pfizer.Domain.Models;
using Pfizer.Repository;
using Pfizer.Service;
using Pfizer.Web.Areas.Common.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class ConversionFactorController : AbstractEntryController<ConversionFactor, ConversionFactorViewModel>
    {
        readonly IEntityService<ConversionFactor> _service = new ConversionFactoreService(new UnitOfWorkWrapper());
        readonly IEntityService<Dosage> _dosageService = new DosageService(new UnitOfWorkWrapper());
        readonly IEntityService<Product> _productService = new ProductService(new UnitOfWorkWrapper());
        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _service.Filter(o => checkedRecords.Contains(o.ConversionFactorId))
                              select new
                              {
                                  PfizerCode = data.PfizerCode,
                                  Factor = data.Factor,
                                  UnitOfMeasure = data.UnitOfMeasure.Name,
                                  Status = data.Status
                              };
            return displayData;
        }

        protected override IEntityService<ConversionFactor> GetService()
        {
            return _service;
        }

        protected override IEnumerable<ConversionFactorViewModel> GetModelRecordsToBindInGrid()
        {

            var id = ParentEntityId;
            return _service.GetAll()
                .Where(o => o.ConversionFactorId.Equals(id))
                .Select(o => o.Convert<ConversionFactor, ConversionFactorViewModel>()
                );
        }

        protected override ConversionFactor AssignViewModelToEntity(ConversionFactorViewModel viewModel)
        {
            if (viewModel.PfizerCode != null && viewModel.UnitOfMeasureName != null)
            {
                viewModel.UnitOfMeasureName = viewModel.UnitOfMeasureName.Trim();
                viewModel.PfizerCode = viewModel.PfizerCode.Trim();
            }
            return viewModel.Convert<ConversionFactorViewModel, ConversionFactor>();
        }

        protected override ConversionFactorViewModel AssignEntityToViewModel(ConversionFactor entity)
        {
            return entity.Convert<ConversionFactor, ConversionFactorViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Pfizer Code and Conversion Factor";
        }

        protected override bool IsIndexPartialView()
        {
            return false;
        }

        protected override void SetViewBagForIndexView(int? id)
        {
            var dosageItem = _dosageService.GetAll().SingleOrDefault(o => o.DosageId.Equals(id));
            if (dosageItem != null)
            {
                ViewBag.DosageForm = dosageItem.Name;
            }
        }

        protected override void SetViewBagsForCreate(int? id)
        {
        }

        protected override void SetViewBagsForEdit(int? id)
        {
        }

        protected override ConversionFactorViewModel SetViewModelData(ConversionFactorViewModel viewModel)
        {
            viewModel.DosageId = ParentEntityId.ToInteger();
            var dosage = _dosageService.Find(viewModel.DosageId);
            viewModel.DosageForm = dosage.Name;
            viewModel.ProductName = _productService.Find(dosage.ProductId).Name;
            return base.SetViewModelData(viewModel);
        }
    }
}
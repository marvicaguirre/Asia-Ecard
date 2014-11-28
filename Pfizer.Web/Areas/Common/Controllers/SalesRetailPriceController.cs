using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pfizer.Domain.Models;
using Pfizer.Repository.SeedData.DataDictionary;
using Pfizer.Web.Areas.Common.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class SalesRetailPriceController : AbstractEntryController<SalesRetailPrice, SalesRetailPriceViewModel>
    {
        private readonly IEntityService<SalesRetailPrice> _service;
        private readonly IEntityService<Product> _productService;
        private readonly IEntityService<Dosage> _dosageService;

        public SalesRetailPriceController(IEntityService<SalesRetailPrice> service, IEntityService<Product> productService, IEntityService<Dosage> dosageService)
        {
            _service = service;
            _productService = productService;
            _dosageService = dosageService;
        }
        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _service.Filter(o => checkedRecords.Contains(o.SalesRetailPriceId))
                              select new
                              {
                                  Sales_Retail_Price = data.From, data.To,data.Price
                              };
            return displayData;
        }

        protected override IEntityService<SalesRetailPrice> GetService()
        {
            return _service;
        }

        protected override IEnumerable<SalesRetailPriceViewModel> GetModelRecordsToBindInGrid()
        {
            //TODO: check if it returns product and dosage
            var id = ParentEntityId.ToInteger();
            var dosageItem = _dosageService.Find(id);
            var productId = dosageItem.ProductId;
            var productItem = _productService.Find(productId);
            var salesRetailItems = _service.Filter(o => o.DosageId.Equals(id)).ToList();
            foreach (var item in salesRetailItems)
            {
                item.DosageId = id;
                item.ProductId = productId;
                item.Dosage.Name = dosageItem.Name;
                item.Product.Name = productItem.Name;
            
            }

            return salesRetailItems.Where(o=>o.SalesRetailPriceId.Equals(id)).Select(o => o.Convert<SalesRetailPrice, SalesRetailPriceViewModel>());
            var items = _service.Filter(o => o.SalesRetailPriceId.Equals(id)).Select(o => o.Convert<SalesRetailPrice, SalesRetailPriceViewModel>());
            return _service.Filter(o => o.SalesRetailPriceId.Equals(id)).Select(o => o.Convert<SalesRetailPrice, SalesRetailPriceViewModel>());
        
        }

        protected override SalesRetailPrice AssignViewModelToEntity(SalesRetailPriceViewModel viewModel)
        {
            return viewModel.Convert<SalesRetailPriceViewModel, SalesRetailPrice>();
        }

        protected override SalesRetailPriceViewModel AssignEntityToViewModel(SalesRetailPrice entity)
        {
            return entity.Convert<SalesRetailPrice, SalesRetailPriceViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Price";
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
            var classItem = _dosageService.Find(ParentEntityId.ToInteger());
            if (classItem != null)
            {
                //TODO:Kevin Check 
                ViewBag.ClassName = classItem.Name;
            }
            ViewBag.ClassId = ParentEntityId.ToString();
        }
    }
}
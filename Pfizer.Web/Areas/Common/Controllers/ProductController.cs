using System.Collections.Generic;
using System.Linq;
using Pfizer.Web.Areas.Common.ViewModels;
using System;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.EventAggregator.EventArguments;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Domain.Models;


namespace Pfizer.Web.Areas.Common.Controllers
{
    public class ProductController : AbstractEntryController<Product, ProductViewModel>
    {
        private readonly IEntityServiceEventAggregate<Product> _service;
        public ProductController(IEntityServiceEventAggregate<Product> service, ISubscriber<EntityCreatingArgs<Product>> productIdGenerationSubcriber)
        {
            service.EventAggregator.Subscribe(productIdGenerationSubcriber);
            _service = service;
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _service.Filter(o => checkedRecords.Contains(o.ProductId))
                              select new
                              {
                                  Product = data.Name, data.Description
                              };
            return displayData;
        }

        protected override IEntityService<Product> GetService()
        {
            return _service;
        }

        protected override IEnumerable<ProductViewModel> GetModelRecordsToBindInGrid()
        {
            return _service.GetAll().Select(o => o.Convert<Product, ProductViewModel>());
        }

        protected override Product AssignViewModelToEntity(ProductViewModel viewModel)
        {
            if (viewModel.Name != null && viewModel.Description != null)
            {
                viewModel.Name = viewModel.Name.Trim();
                viewModel.Description = viewModel.Description.Trim();
                
            }
            return viewModel.Convert<ProductViewModel, Product>();
        }

        protected override ProductViewModel AssignEntityToViewModel(Product entity)
        {
            return entity.Convert<Product, ProductViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Product";
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
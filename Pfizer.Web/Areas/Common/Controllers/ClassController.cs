using System.Collections.Generic;
using System.Linq;
using Pfizer.Web.Areas.Common.ViewModels;
using System;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Domain.Models;


namespace Pfizer.Web.Areas.Common.Controllers
{
    public class ClassController : AbstractEntryController<Class, ClassViewModel>
    {
        private readonly IEntityService<Class> _service;

        public ClassController(IEntityService<Class> service)
        {
            _service = service;
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _service.Filter(o => checkedRecords.Contains(o.ClassId))
                              select new
                              {
                                  Class = data.Name, data.Description,
                              };
            return displayData;
        }

        protected override IEntityService<Class> GetService()
        {
            return _service;
        }

        protected override IEnumerable<ClassViewModel> GetModelRecordsToBindInGrid()
        {
            return _service.GetAll().Select(o => o.Convert<Class, ClassViewModel>());
        }

        protected override Class AssignViewModelToEntity(ClassViewModel viewModel)
        {
            if (viewModel.Name != null && viewModel.Description != null)
            {
                viewModel.Name = viewModel.Name.Trim();
                viewModel.Description = viewModel.Description.Trim();
            }
            return viewModel.Convert<ClassViewModel, Class>();
        }

        protected override ClassViewModel AssignEntityToViewModel(Class entity)
        {
            return entity.Convert<Class, ClassViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "Class";
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
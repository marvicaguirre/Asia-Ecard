using Pfizer.Repository;
using Pfizer.Service;
using Pfizer.Web.Areas.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class SystemMessageController : AbstractEntryController<SystemMessage, SystemMessageViewModel>
    {
        private readonly IEntityService<SystemMessage> _service;

        public SystemMessageController()
        {
            _service = new SystemMessageService(new UnitOfWorkWrapper());
        }

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            return null;
        }

        protected override IEntityService<SystemMessage> GetService()
        {
            return _service;
        }

        protected override IEnumerable<SystemMessageViewModel> GetModelRecordsToBindInGrid()
        {
            return _service.GetAll().Select(o => o.Convert<SystemMessage, SystemMessageViewModel>());
        }

        protected override SystemMessage AssignViewModelToEntity(SystemMessageViewModel viewModel)
        {
            return viewModel.Convert<SystemMessageViewModel, SystemMessage>();
        }

        protected override SystemMessageViewModel AssignEntityToViewModel(SystemMessage entity)
        {
            return entity.Convert<SystemMessage, SystemMessageViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "System Messages";
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
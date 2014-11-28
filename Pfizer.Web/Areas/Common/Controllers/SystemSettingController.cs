using System.Collections.Generic;
using System.Linq;
using Pfizer.Repository;
using Pfizer.Service;
using Pfizer.Web.Areas.Common.ViewModels;
using System;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Domain.Models;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class SystemSettingController : AbstractEntryController<SystemSetting, SystemSettingViewModel>
    {
        readonly IEntityService<SystemSetting> _systemSettingService = new SystemSettingService(new UnitOfWorkWrapper());

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _systemSettingService.Filter(o => checkedRecords.Contains(o.SystemSettingId))
                              select new
                              {
                                  data.SystemSettingId, data.SettingName, data.SettingValue, data.DataType
                              };
            return displayData;
        }

        protected override IEntityService<SystemSetting> GetService()
        {
            return _systemSettingService;
        }

        protected override IEnumerable<SystemSettingViewModel> GetModelRecordsToBindInGrid()
        {
            return _systemSettingService.GetAll().Select(o => o.Convert<SystemSetting, SystemSettingViewModel>());
        }

        protected override SystemSetting AssignViewModelToEntity(SystemSettingViewModel model)
        {
            return model.Convert<SystemSettingViewModel, SystemSetting>();
        }

        protected override SystemSettingViewModel AssignEntityToViewModel(SystemSetting entity)
        {
            return entity.Convert<SystemSetting, SystemSettingViewModel>();
        }

        protected override string GetIndexViewTitle()
        {
            return "System Setting";
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
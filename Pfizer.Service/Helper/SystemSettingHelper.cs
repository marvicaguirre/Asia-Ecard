using System;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using System.Linq;

namespace Pfizer.Service.Helper
{
    public class SystemSettingHelper : ISystemSettingHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        public SystemSettingHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool HasSetting(string settingName)
        {
            return _unitOfWork.Repository<SystemSetting>().GetAll.Any(s => s.SettingName == settingName);
        }

        public dynamic GetSettingValue(string settingName)
        {
            var settingValue = _unitOfWork.Repository<SystemSetting>().Query()
                .Filter(s => s.SettingName == settingName)
                .GetResult()
                .FirstOrDefault();

            if (settingValue != null)
            {
                return Convert.ChangeType(settingValue.SettingValue, Type.GetType(string.Format("System.{0}", settingValue.DataType), true, true));
            }

            return string.Format("Setting name '{0}' does not exist in the System Settings.", settingName);
        }
    }
}

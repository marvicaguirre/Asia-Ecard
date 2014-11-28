using System;
using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(SystemSettingValidator))]
    public class SystemSettingViewModel:ISystemSetting
    {
        public Guid SystemSettingId { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public string DataType { get; set; }
    }
}
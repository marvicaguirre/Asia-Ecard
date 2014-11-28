using FluentValidation.Attributes;
using System;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(SystemMessageValidator))]
    public class SystemMessageViewModel : ISystemMessageViewModel
    {
        public Guid SystemMessageId { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
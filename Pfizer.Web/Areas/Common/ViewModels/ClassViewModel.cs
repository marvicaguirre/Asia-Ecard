using System;
using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(ClassValidator))]
    public class ClassViewModel : IClassViewModel
    {
        public int ClassId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
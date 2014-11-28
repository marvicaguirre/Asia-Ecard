using System;
//using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Pfizer.Service.Validators.ModelViewValidator;
using Pfizer.Domain.Interfaces.ViewModel;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(DepartmentValidators))]
    public sealed class DepartmentViewModel : IDepartment
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public bool IsSBD { get; set; }
    }
}
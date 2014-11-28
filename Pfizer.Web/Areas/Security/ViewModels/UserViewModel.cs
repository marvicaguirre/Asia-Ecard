using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Security.ViewModels
{
    [Validator(typeof(UserValidator))]
    public class UserViewModel : IUser
    {
        public int  UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        public int? EmployeeId { get; set; }
        public string Status { get; set; }
    }
}

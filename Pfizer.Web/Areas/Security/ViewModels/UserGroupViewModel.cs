using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Domain.Models;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Security.ViewModels

{
    [Validator(typeof(UserGroupValidator))]
    public class UserGroupViewModel : IUserGroup
    {
        public Guid UserId { get; set; }
        public int UserGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
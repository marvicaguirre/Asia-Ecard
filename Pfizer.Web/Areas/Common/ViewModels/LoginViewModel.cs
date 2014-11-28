using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(LoginValidator))]
    public class LoginViewModel : ILogin
    {
        //[Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        //[Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
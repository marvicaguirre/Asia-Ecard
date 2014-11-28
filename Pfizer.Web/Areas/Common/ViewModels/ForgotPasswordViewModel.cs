using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required (ErrorMessage="Email Address must not be empty")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Re-Enter Email must not be empty")]
        [Display(Name = "Re-Enter Email")]
        [Compare("EmailAddress", ErrorMessage = "Email Address did not match")]
        public string ReEmailAddress { get; set; }
    }
}
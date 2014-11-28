using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Domain.Models;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(EmployeeValidator))]
    public class EmployeeViewModel : IEmployee
    {
        public int EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String FullName { get; set; }
        public int? CompanyId { get; set; }
        public String CompanyName { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public String DepartmentName
        {
            get { return (Department != null ? Department.Name : String.Empty); }
        }
        public bool IsSupervisor { get; set; }
        public int? EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public String EmployeeTypes
        {
            get
            {
                if (EmployeeType != null)
                    return EmployeeType.Name;

                return String.Empty;
            }
        }
        public String Status { get; set; }
        [Phone(ErrorMessage = "Telephone Number field is not a valid Phone Number.")]
        public String TelephoneNo { get; set; }
        [Phone(ErrorMessage = "Mobile Number field is not a valid Mobile Number.")]
        public String MobileNo { get; set; }
        [EmailAddress]
        public String Email { get; set; }
    }
}
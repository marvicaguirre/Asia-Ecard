using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Models;

namespace Pfizer.Domain.Models
{
    public class Employee : AbstractEmployee
    {
        public bool IsSupervisor { get; set; }

        public int EmployeeTypeId { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        [MaxLength(50)]
        public string TelephoneNo { get; set; }
        [MaxLength(50)]
        public string MobileNo { get; set; }
        [MaxLength(100)]
        public String Email { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [NotMapped]
        public new String FullName
        {
            get { return String.Format("{0}, {1} {2}", LastName, FirstName, String.IsNullOrEmpty(MiddleName) ? String.Empty : MiddleName); }
        }
    }
}
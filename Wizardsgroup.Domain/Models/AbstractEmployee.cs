using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public class AbstractEmployee : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string MiddleName { get; set; }
        [NotMapped]
        public string FullName { get { return string.Format("{0}, {1} {2}", LastName, FirstName, string.IsNullOrEmpty(MiddleName) ? string.Empty : MiddleName[0].ToString(CultureInfo.InvariantCulture)); } }
        public int? CompanyId { get; set; }
        public virtual AbstractCompany Company { get; set; }
        //public Guid EmployeeClassificationId { get; set; }        
        //public virtual EmployeeClassification EmployeeClassification { get; set; }

    }
}

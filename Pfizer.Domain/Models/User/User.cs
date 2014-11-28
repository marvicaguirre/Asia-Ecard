using System;
using System.ComponentModel.DataAnnotations;
using Wizardsgroup.Domain.Models;

namespace Pfizer.Domain.Models
{
    public class User : AbstractUser
    { 
        public virtual Employee Employee { get; set; }
        public int? EmployeeId { get; set; }
    }
}

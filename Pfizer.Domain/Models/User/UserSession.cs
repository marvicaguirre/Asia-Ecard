using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    public class UserSession : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserSessionId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int RetryCount { get; set; }
        public bool IsLocked { get; set; }

    }
}

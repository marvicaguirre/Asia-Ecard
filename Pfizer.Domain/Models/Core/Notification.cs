using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    public class Notification : AbstractBaseModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        public int NotificationTypeId { get; set; }
        public virtual NotificationType NotificationType { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    public class ActivityNotification : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityNotificationId { get; set; }
        public int NotificationTypeId { get; set; }
        public virtual NotificationType NotificationType { get; set; }
        public string Message { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RouteValues { get; set; }
        public bool IsSent { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }
        
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}

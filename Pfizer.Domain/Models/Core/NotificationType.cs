using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    public class NotificationType : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationTypeId { get; set; }
        public string NotificationTypeName { get; set; }
        public string NotificationMessage { get; set; }
        public string NotificationEmail { get; set; }
        public string NotificationSms { get; set; }
    }

}

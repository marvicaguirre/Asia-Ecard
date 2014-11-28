using System;
using Pfizer.Domain.Models;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    public class ActivityNotificationViewModel
    {
        public int ActivityNotificationId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RouteValues { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

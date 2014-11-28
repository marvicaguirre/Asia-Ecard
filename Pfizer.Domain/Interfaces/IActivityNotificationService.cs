using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pfizer.Domain.Models;

namespace Pfizer.Domain.Interfaces
{
    public interface IActivityNotificationService
    {
        void AddActivityNotification(string notificationTypeName, ActivityNotification activityNotification, bool ignoreIfNotificationExists = false, params object[] args);
        void AddActivityNotification(string notificationTypeName,int userId, string customMessage = "", string area = "", string controller = "", string action = "", string routeValues = "", bool ignoreIfNotificationExists = false, params object[] args);
        List<ActivityNotification> GetActivityNotifications(int userId);
        
        //List<ActivityNotification> GetActivityNotificationByEmployeeId(Guid employeeId);
        //List<ActivityNotification> GetActivityNotificationByCompanyId(List<Guid> companyIds);
        //void SendNotifications();
        //void SendNotification(ActivityNotification activityNotification);
    }
}

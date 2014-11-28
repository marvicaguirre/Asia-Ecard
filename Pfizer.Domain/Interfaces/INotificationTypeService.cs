using System;
using Pfizer.Domain.Models;

namespace Pfizer.Domain.Interfaces
{
    public interface INotificationTypeService
    {
        int GetNotificationIdByName(string notificationTypeName);
        NotificationType GetNotificationByName(string notificationTypeName);
        NotificationType GetNotificationById(int notificationTypeId);
    }
}

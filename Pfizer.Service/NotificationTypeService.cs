using System;
using System.Linq;
using System.Linq.Expressions;
using Pfizer.Domain.Interfaces;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
       public class NotificationTypeService : AbstractEntityService<NotificationType>, INotificationTypeService
    {
        public NotificationTypeService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<NotificationType, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.NotificationTypeId == id;
        }

        protected override Expression<Func<NotificationType, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<NotificationType> OrderBy(IQueryable<NotificationType> arg)
        {
            return arg.OrderBy(o => o.NotificationTypeName);
        }


        public NotificationType GetNotificationByName(string notificationTypeName)
        {
            int notificationId = GetNotificationIdByName(notificationTypeName);

            return GetNotificationById(notificationId);
        }

        public int GetNotificationIdByName(string notificationTypeName)
        {
            return UnitOfWork.Repository<NotificationType>().GetAll
                .Where(nt => nt.NotificationTypeName == notificationTypeName)
                .Select(nt => nt.NotificationTypeId)
                .FirstOrDefault();
        }

        public NotificationType GetNotificationById(int notificationTypeId)
        {
            return UnitOfWork.Repository<NotificationType>().GetAll.FirstOrDefault(nt => nt.NotificationTypeId == notificationTypeId);
        }
    }
}

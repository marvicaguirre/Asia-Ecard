using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pfizer.Domain.Interfaces;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
    public class ActivityNotificationService : AbstractEntityService<ActivityNotification>, IActivityNotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityNotificationService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override Expression<Func<ActivityNotification, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.ActivityNotificationId == id;
        }

        protected override Expression<Func<ActivityNotification, object>>[] Include()
        {
            return new Expression<Func<ActivityNotification, object>>[] { o => o.NotificationType };
        }

        protected override IOrderedQueryable<ActivityNotification> OrderBy(IQueryable<ActivityNotification> arg)
        {
            return arg.OrderByDescending(o => o.CreatedDate);
        }

        public void AddActivityNotification(string notificationTypeName, ActivityNotification activityNotification, bool ignoreIfNotificationExists = false, params object[] args)
        {
            AddActivityNotification(notificationTypeName,
                activityNotification.UserId.Value,
                activityNotification.Message,
                activityNotification.Area,
                activityNotification.Controller,
                activityNotification.Action,
                activityNotification.RouteValues,
                ignoreIfNotificationExists,
                args);
        }

        public void AddActivityNotification(string notificationTypeName,int userId, string customMessage = "", string area = "", string controller = "", string action = "", string routeValues = "", bool ignoreIfNotificationExists = false, params object[] args)
        {
            NotificationType notificationType = new NotificationTypeService(_unitOfWork).GetNotificationByName(notificationTypeName);
            string notificationMessage = customMessage != null ?
                            string.Format((customMessage.Trim() != string.Empty ?
                            customMessage :
                                notificationType.NotificationMessage), args) :
                            string.Format(notificationType.NotificationMessage, args);
            DateTime todayDate = DateTime.Now;
            bool notificationExists = _unitOfWork.Repository<ActivityNotification>().GetAll
                    .Any(a => a.Message == notificationMessage &&
                            a.CreatedDate.Year == todayDate.Year && a.CreatedDate.Month == todayDate.Month && a.CreatedDate.Day == todayDate.Day);

            if (!ignoreIfNotificationExists || (ignoreIfNotificationExists && !notificationExists))
            {
                _unitOfWork.Repository<ActivityNotification>().Insert(
                    new ActivityNotification
                    {
                        NotificationTypeId = notificationType.NotificationTypeId,
                        Message = notificationMessage,
                        Area = area != null ? area : string.Empty,
                        Controller = controller != null ? controller : string.Empty,
                        Action = action != null ? action : string.Empty,
                        RouteValues = routeValues != null ? routeValues : string.Empty,
                        IsSent = false,
                        UserId = userId,
                        CreatedBy = "medicardadmin",
                        CreatedDate = DateTime.Now,
                        RecordStatus = 0
                    });

                _unitOfWork.Save();
            }
        }

        public List<ActivityNotification> GetActivityNotifications(int userId)
        {
            var notifications = _unitOfWork.Repository<ActivityNotification>().GetAll
                .Where(o => o.UserId == userId)
                .Distinct()
                .OrderByDescending(o => o.CreatedDate)
                .ToList();

            return notifications;
        }

        //public List<ActivityNotification> GetActivityNotificationByDepartment(Guid departmentId)
        //{
        //    var notifications = _unitOfWork.Repository<ActivityNotification>().GetAll
        //        .Where(o => o.DepartmentId == departmentId)
        //        .Distinct()
        //        .OrderBy(o => o.CreatedDate)
        //        .ToList();

        //    return notifications;
        //}

        //public List<ActivityNotification> GetActivityNotificationByEmployeeId(Guid employeeId)
        //{
        //    var employeeCompanyAssignments = (from a in _unitOfWork.Repository<User>().GetAll
        //                                      join b in _unitOfWork.Repository<UserCompanyMapping>().GetAll on a.UserId equals b.UserId
        //                                      where a.EmployeeId == employeeId
        //                                      select b.CompanyId).ToList();

        //    var notifications = GetActivityNotificationByCompanyId(employeeCompanyAssignments);

        //    return notifications;
        //}

        //public List<ActivityNotification> GetActivityNotificationByCompanyId(List<Guid> companyIds)
        //{
        //    var notifications = (from a in _unitOfWork.Repository<ActivityNotification>().GetAll
        //                         join b in _unitOfWork.Repository<UserCompanyMapping>().GetAll on a.CompanyId equals b.CompanyId
        //                         where companyIds.Contains(b.CompanyId)
        //                         select a)
        //                            .Distinct()
        //                            .OrderByDescending(n => n.CreatedDate)
        //                            .ToList();

        //    return notifications;
        //}

        //public void SendNotifications()
        //{
        //    bool emailNotificationsEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["EmailNotificationsEnabled"].ToString());

        //    if (emailNotificationsEnabled)
        //    {
        //        var notificationsToSend = UnitOfWork.Repository<ActivityNotification>().GetAll
        //            .Where(n => n.IsSent == false)
        //            .ToList();

        //        foreach (var notification in notificationsToSend)
        //        {
        //            SendNotification(notification);
        //        }
        //    }
        //}

        //public void SendNotification(ActivityNotification activityNotification)
        //{
        //    List<Employee> employeesToNotify = new List<Employee>();

        //    if (activityNotification.CompanyId != null)
        //    {
        //        employeesToNotify.AddRange((
        //            from a in UnitOfWork.Repository<Employee>().GetAll
        //            join b in UnitOfWork.Repository<Customer>().GetAll on a.CompanyId equals b.CompanyId
        //            join c in UnitOfWork.Repository<CustomerSite>().GetAll on b.CustomerId equals c.CustomerId
        //            join d in UnitOfWork.Repository<NotificationMapping>().GetAll on a.EmployeeTypeId equals d.EmployeeTypeId
        //            where a.CompanyId == activityNotification.CompanyId &&
        //                  c.CustomerSiteId == activityNotification.CustomerSiteId &&
        //                  d.NotificationTypeId == activityNotification.NotificationTypeId &&
        //                  a.Email != null
        //            select a)
        //                .Distinct()
        //                .ToList());
        //    }
        //    else
        //    {
        //        employeesToNotify.AddRange((
        //            from a in UnitOfWork.Repository<Employee>().GetAll
        //            join b in UnitOfWork.Repository<NotificationMapping>().GetAll on a.EmployeeTypeId equals b.EmployeeTypeId
        //            where a.CompanyId == activityNotification.CompanyId &&
        //                  b.NotificationTypeId == activityNotification.NotificationTypeId &&
        //                  a.Email != null
        //            select a)
        //                .Distinct()
        //                .ToList());
        //    }

        //    if (employeesToNotify.Count > 0)
        //    {
        //        string subject = string.Format("Spark Notification: {0}", activityNotification.NotificationType.NotificationTypeName);
        //        var emailAddresses = employeesToNotify.Select(e => e.CompanyEmailAddress).ToList();

        //        EmailHelper.Send(emailAddresses, subject, activityNotification.Message);

        //        if (EmailHelper.LastErrorMessage != string.Empty)
        //        {
        //            Logger.Log(string.Format("[EmailHelper] Error: {0}", EmailHelper.LastErrorMessage));
        //        }
        //        else
        //        {
        //            activityNotification.IsSent = true;
        //            _unitOfWork.Repository<ActivityNotification>().Update(activityNotification);
        //            _unitOfWork.Save();
        //        }
        //    }
        //}

    }
}

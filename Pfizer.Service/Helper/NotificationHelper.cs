using System;
using System.Linq;
using System.Collections.Generic;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Service.Helper
{
    public abstract class NotificationHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmployeeService _employeeService;
        private readonly NotificationTypeService _notificationTypeService;
        private readonly UserService _userService;

        public NotificationHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _employeeService = new EmployeeService(_unitOfWork);
            _notificationTypeService = new NotificationTypeService(_unitOfWork);
            _userService = new UserService(_unitOfWork);

        }

        public void NotifyByDashboard(string notificationTypeName, ActivityNotification activityNotification, bool ignoreIfDuplicate = false, params object[] args)
        {
            new ActivityNotificationService(_unitOfWork).AddActivityNotification(
                notificationTypeName,
                activityNotification,
                ignoreIfDuplicate,
                args: args);
        }

        public void NotifyByEmail(string subject, string message, List<string> emailAddresses)
        {
            if (emailAddresses.Any())
            {
                EmailHelper.Send(emailAddresses, subject, message);
            }
        }

        public void NotifyBySms(string message, List<string> mobileNumbers)
        {
            if (mobileNumbers.Any())
            {
                //SMS client requirement
                //SMSHelper.Send(mobileNumbers, message);
            }
        }

        public void Notify(string notificationTypeName, ActivityNotification activityNotification,
                            bool ignoreIfDuplicate = false, 
                            bool forEmailNotification = false, 
                            bool forSmsNotification = false,
                            bool forDashboardNotification = false, 
                            params object[] args)
        {
            try
            {
                if (forDashboardNotification)
                {
                    NotifyByDashboard(
                        notificationTypeName,
                        activityNotification,
                        ignoreIfDuplicate,
                        args);
                }

                if (forEmailNotification)
                {
                    var notificationMessage = _notificationTypeService.GetNotificationByName(notificationTypeName);
                    var employeesToEmail = (from a in _employeeService.GetAll()
                                    join b in _userService.GetAll() on a.EmployeeId equals b.EmployeeId
                                    where b.UserId == activityNotification.UserId
                                    select a.Email)
                                        .ToList();

                    NotifyByEmail(
                        notificationTypeName.SplitCamelCase(),
                        string.Format(notificationMessage.NotificationEmail, args),
                        employeesToEmail);
                }

                if (forSmsNotification)
                {
                    //sms helper
                    var notificationMessage = _notificationTypeService.GetNotificationByName(notificationTypeName);
                    var mobileNumbers = (from a in _employeeService.GetAll()
                                        join b in _userService.GetAll() on a.EmployeeId equals b.EmployeeId
                                        where b.UserId == activityNotification.UserId
                                        select a)
                                        .Distinct().Select(o => o.MobileNo)
                                        .ToList();
                   
                    NotifyBySms(
                        string.Format(notificationMessage.NotificationSms, args),
                        mobileNumbers);
                }
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException != null ? ex.InnerException : ex;

                while (innerException != null)
                {
                    innerException = innerException.InnerException;
                }

                Logger.Log(string.Format("[NotificationUtility].[Notify] Error: {0} at {1}", ex.Message, ex.StackTrace));
            }
        }

        public virtual void Notify(string notificationTypeName, ActivityNotification activityNotification, bool ignoreIfDuplicate = false, params object[] args)
        {
            Notify(notificationTypeName, activityNotification, ignoreIfDuplicate, false, false, true, args: args);
        }
    }
}

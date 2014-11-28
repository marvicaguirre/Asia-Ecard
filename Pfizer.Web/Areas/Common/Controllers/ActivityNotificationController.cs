using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pfizer.Domain.Models;
using Pfizer.Repository;
using Pfizer.Service;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Web.Areas.Common.ViewModels;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class ActivityNotificationController : AbstractEntryController<ActivityNotification, ActivityNotificationViewModel>
    {
        private readonly IEntityService<ActivityNotification> _service = new ActivityNotificationService(new UnitOfWorkWrapper());

        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _service.Filter(o => checkedRecords.Contains(o.ActivityNotificationId))
                              select new
                              {
                                  message = data.Message,
                                  url = data.Area + "/" + data.Controller + "/" + data.Action + "/" + data.RouteValues
                              };
            return displayData;
        }

        protected override IEntityService<ActivityNotification> GetService()
        {
            return _service;
        }

        protected override IEnumerable<ActivityNotificationViewModel> GetModelRecordsToBindInGrid()
        {
            return GetActivityNotifications(SessionContainer.UserId);
        }

        protected override ActivityNotification AssignViewModelToEntity(ActivityNotificationViewModel viewModel)
        {
            return viewModel.Convert<ActivityNotificationViewModel, ActivityNotification>();
        }

        protected override ActivityNotificationViewModel AssignEntityToViewModel(ActivityNotification entity)
        {
            var vm = new ActivityNotificationViewModel
            {
                ActivityNotificationId = entity.ActivityNotificationId,
                Message = entity.Message,
                Url = entity.Area + "/" + entity.Controller + "/" + entity.Action + "/" + entity.RouteValues,
                //User = entity.User,
                //UserId = entity.UserId
            };
            return vm;
        }

        protected override string GetIndexViewTitle()
        {
            return "Activity Notification";
        }

        protected override bool IsIndexPartialView()
        {
            return false;
        }

        protected override void SetViewBagForIndexView(int? id)
        {
            ViewBag.Notifications = GetActivityNotifications(SessionContainer.UserId);
        }

        protected override void SetViewBagsForCreate(int? id)
        {
        }

        protected override void SetViewBagsForEdit(int? id)
        {
        }

        private List<ActivityNotificationViewModel> GetActivityNotifications(int userId)
        {
            var data = ((ActivityNotificationService)_service).GetActivityNotifications(userId);
            var model = data.ToList();

            var list = model.Select(notification => new ActivityNotificationViewModel
            {
                Date = notification.CreatedDate,
                ActivityNotificationId = notification.ActivityNotificationId,
                Message = notification.Message,
                Url = notification.Area + "/" + notification.Controller + "/" + notification.Action + "/" + notification.RouteValues,
                Area = notification.Area,
                Controller = notification.Controller,
                Action = notification.Action,
                RouteValues = notification.RouteValues,
                User = notification.User,
                UserId = notification.UserId.Value
            }).ToList();

            return list;
        }

        [HttpGet]
        public ActionResult Notifications()
        {
            ViewBag.Notifications = GetActivityNotifications(SessionContainer.UserId);
            return PartialView("Notifications");
        }
    }
}
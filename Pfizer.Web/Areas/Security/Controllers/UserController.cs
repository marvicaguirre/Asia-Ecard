using System;
using System.Collections.Generic;
using System.Linq;
using Pfizer.Web.Areas.Common.Controllers;
using Pfizer.Web.Areas.Security.ViewModels;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Helpers;
using Pfizer.Domain.Models;

namespace Pfizer.Web.Areas.Security.Controllers
{

    public class UserController : AbstractEntryController<User, UserViewModel>
    {        
        readonly IEntityService<User> _userService;
        public UserController(IEntityService<User> userService)
        {
            _userService = userService;
        }

        //
        protected override IEnumerable<object> GetSelectedItems(int[] checkedRecords)
        {
            var displayData = from data in _userService.GetAll().Where(o => checkedRecords.Contains(o.UserId))
                              select new
                              {
                                  data.UserName, data.Employee.FullName,
                              };
            return displayData;
        }

        protected override IEntityService<User> GetService()
        {
            return _userService;
        }

        protected override IEnumerable<UserViewModel> GetModelRecordsToBindInGrid()
        {
            var records = _userService.GetAll().Select(o=>o.Convert<User,UserViewModel>((source, destination) =>
            {
                destination.FullName = source.Employee.FullName;
            }));
            return records;

        }
        private string HashLoginDetails(string username,string password)
        {
            string result = string.Empty;

            if (username != null && password != null)
            {
                string salt = PasswordHelper.CreateSalt(username);
                result = PasswordHelper.HashPassword(salt, password);
            }

            return result;
        }

        protected override User AssignViewModelToEntity(UserViewModel model)
        {
            var userEntity = _userService.Find(model.UserId) ?? new User();
            userEntity.UserName = model.UserName;
            userEntity.EmployeeId = model.EmployeeId;            
            userEntity.Password = HashLoginDetails(model.UserName, model.UserPassword);            

            return userEntity;
        }

        protected override UserViewModel AssignEntityToViewModel(User entity)
        {
            return entity.Convert<User, UserViewModel>((user, model) =>
            {
                model.UserPassword = string.Empty;
            });
        }

        protected override string GetIndexViewTitle()
        {
            return "Users";
        }

        protected override bool IsIndexPartialView()
        {
            return false;
        }

        protected override void SetViewBagForIndexView(int? id)
        {

        }

        protected override void SetViewBagsForCreate(int? id)
        {

        }

        protected override void SetViewBagsForEdit(int? id)
        {

        }
    }
}

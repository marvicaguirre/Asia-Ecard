using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Pfizer.Repository;
using Pfizer.Service;
using Pfizer.Web.Areas.Common.ViewModels;
using Wizardsgroup.Core.Web.Extensions;
using Wizardsgroup.Core.Web.Helpers;
using Wizardsgroup.Core.Web.SessionManagement;
using Wizardsgroup.Domain.Containers;
using Pfizer.Domain.Infrastructure;
using Pfizer.Domain.Models;
using Wizardsgroup.Utilities.Helpers;


namespace Pfizer.Web.Areas.Common.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;            
        }

        public ActionResult Logoff()
        {
            UserTracker.Instance.ClearUserName();
            SessionManager.ClearSession();            
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult RedirectToLogin()
        {
            SessionManager.ClearSession();
            UserTracker.Instance.ClearUserName();
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {            
            var errorMessages = string.Empty;
            ViewBag.ResetPassword = false;

            if (ModelState.IsValid)
            {
                bool validateAll = ValidateUser(model);

                if (validateAll)
                {                    
                    return RedirectToAction("Default", "Home");
                }
            }
            else
            {
                errorMessages = "Password must not be empty";
            }

            ModelState.AddModelError("", errorMessages);
            return View(model);
        }

        private bool ValidateUser(LoginViewModel model)
        {
            var validatedUser = _userService.GetUserByUsernameAndPassword(model.UserName, model.Password);
            var isValid = validatedUser != null;

            if (isValid)
            {               
                StoreSessionForValidUser(model, true, validatedUser);
                UserSecurityAccess.Instance.SetSecurityAccessToUser(GetUserAccess, validatedUser.UserId);
                UserSecurityAccess.Instance.SetModuleFunctionContainers(RegisterModuleFunctionContainer.Instance.Container);
            }
            return isValid;
        }

        private void StoreSessionForValidUser(LoginViewModel model, bool isValid, User validatedUser)
        {
            if (!isValid) return;

            try
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
            }
            catch
            {
                //intentionally ignored;
            }

            var httpSessionStateBase = ControllerContext.HttpContext.Session;
            if (httpSessionStateBase == null) return;
            var httpSessionId = httpSessionStateBase.SessionID;            
            var userInfo = new UserInfo(validatedUser.Employee);
            ISessionContainer userSession = new SessionContainer(httpSessionId, new SessionKeyCollection())
            {
                UserId = validatedUser.UserId,
                UserName = validatedUser.UserName,
                UserInfo = userInfo,
                //Role = GetRole(userInfo),
            };
            UserTracker.Instance.SetUserName(httpSessionId, model.UserName);            
            SessionManager.StoreUserSessionContainer(userSession);
        }

        private List<CentralFunctionEx> GetUserAccess(int userId)
        {
            return _userService.GetUserAccess(() => new UnitOfWorkWrapper(), userId);
        }
    }
}

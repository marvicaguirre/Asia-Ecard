#define ENABLE_SECURITY_MENUCHECK
#define ENABLE_SECURITY_FORBUTTONCHECK
using System;
using System.Linq;
using Wizardsgroup.Core.Web.Constants;
using Wizardsgroup.Core.Web.SessionManagement;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Helpers;
using Wizardsgroup.Utilities.Security;

namespace Wizardsgroup.Core.Web.Helpers
{
    public class SecurityHelper
    {
        public static bool HasAccess(string moduleName, string functionName, string userId)
        {
            if (functionName.ToLower() == "logoff") return true;

#if ENABLE_SECURITY_MENUCHECK
            if (string.IsNullOrEmpty(moduleName))
            {
                _Log(string.Format("Possible Configuration Issue: Module name is missing from function '{0}'",functionName));
                return false;
            }
            
            if(string.IsNullOrEmpty(userId))
            {
                _Log("User Id is required!");
                return false;
            }

            if (UserSecurityAccess.Instance.Containers == null)
            {
                throw new Exception(ErrorMessage.UnauthorizedAccess);    
            }            
            if (UserSecurityAccess.Instance.Containers.Any(function => function.FunctionName.ToLower() == functionName.ToLower() && function.UserId == userId.ToInteger()))
            {
                //_Log(string.Format(" [{0}] : [{1}] : {2} group", moduleName, functionName, map.AbstractUserGroup.Name));
                return true;
            }
            var message = string.Format("!!! Function Name ('{0}':'{1}') not found for {2} user!", moduleName, functionName, userId);
            _Log(message);

            return false;
#else
            return true;
#endif
        }

        public static bool IsRequestedActionValid(string action, string controller)
        {
#if ENABLE_SECURITY_FORBUTTONCHECK           
            var functionHelper = new FunctionHelper(UserSecurityAccess.Instance.ModuleFunctionContainers);
            var functionPrefix = functionHelper.GetSecurityInfoFromFunctionName(string.Format("{0}{1}", action, controller));
            if (string.IsNullOrEmpty(functionPrefix.FunctionName))
            {
                _Log(string.Format("{0}'s '{1}' function not found in the list of allowed functions! Please reseed your database in case you haven't yet.", controller, action));
                return false;
            }

            //var functionName = string.Format("{0}{1}", functionPrefix.FunctionName, _CleanUpController(controller));

            var moduleName = functionHelper.GetModuleFromFunctionName(functionPrefix.FunctionName);

            return HasAccess(moduleName, functionPrefix.FunctionName, SessionManager.GetUserId().ToString());
#else
            return true;
#endif
        }
        private static string _CleanUpController(string controllerName)
        {
            return controllerName.Replace("Central", "");
        }
        private static void _Log(string message)
        {
            var finalMessage = string.Format("{0} : {1}{2}", DateTime.Now.ToLongTimeString(), message, Environment.NewLine);
            Logger.Log(finalMessage);
        }
    }
}
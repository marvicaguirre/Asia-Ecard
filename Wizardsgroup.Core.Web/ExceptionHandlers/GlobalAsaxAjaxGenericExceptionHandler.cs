using System;
using Wizardsgroup.Core.Web.Constants;

namespace Wizardsgroup.Core.Web.ExceptionHandlers
{
    public class GlobalAsaxAjaxGenericExceptionHandler : AbstractGlobalAsaxExceptionHandler
    {
        public GlobalAsaxAjaxGenericExceptionHandler(IWebUtilityWrapper webUtilityWrapper) : base(webUtilityWrapper)
        {
        }

        public override bool IsMatchErrorHandler(AjaxRequestResult ajaxRequestResult, Exception exception)
        {
            var errorMessageResult = exception.Message.ToLower() != ErrorMessage.UnauthorizedAccess.ToLower();
            var errorRedirectResult = exception.Message.ToLower() != LoginPageNotFound;
            var isAjaxRequest = ajaxRequestResult.IsAjaxRequest;
            return errorMessageResult && isAjaxRequest && errorRedirectResult;
        }

        public override void HandleException(Exception exception)
        {
            WebUtilityWrapper.Response.Clear();
            WebUtilityWrapper.Response.Write(string.Format("(Global.asax.cs){0}", SeekFullErrorMessage(exception)));
            WebUtilityWrapper.Server.ClearError();
        }
    }
}
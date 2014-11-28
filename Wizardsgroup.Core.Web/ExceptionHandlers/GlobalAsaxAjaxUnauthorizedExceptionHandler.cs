using System;
using Wizardsgroup.Core.Web.Constants;

namespace Wizardsgroup.Core.Web.ExceptionHandlers
{
    public class GlobalAsaxAjaxUnauthorizedExceptionHandler : AbstractGlobalAsaxExceptionHandler
    {
        public GlobalAsaxAjaxUnauthorizedExceptionHandler(IWebUtilityWrapper webUtilityWrapper) : base(webUtilityWrapper)
        {
        }

        public override bool IsMatchErrorHandler(AjaxRequestResult ajaxRequestResult, Exception exception)
        {
            var errorMessageResult = exception.Message.ToLower() == ErrorMessage.UnauthorizedAccess.ToLower();
            var errorRedirectResult = exception.Message.ToLower() != LoginPageNotFound;
            var isAjaxRequest = ajaxRequestResult.IsAjaxRequest;
            return errorMessageResult && isAjaxRequest && errorRedirectResult;
        }

        public override void HandleException(Exception exception)
        {
            WebUtilityWrapper.Response.Clear();
            WebUtilityWrapper.Response.Write("Unauthorized access. Please log in.");
            WebUtilityWrapper.Server.ClearError();
        }
    }
}
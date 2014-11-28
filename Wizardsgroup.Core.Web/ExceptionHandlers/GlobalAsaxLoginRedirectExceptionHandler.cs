using System;

namespace Wizardsgroup.Core.Web.ExceptionHandlers
{
    public class GlobalAsaxLoginRedirectExceptionHandler : GlobalAsaxUnauthorizedExceptionHandler
    {
        public GlobalAsaxLoginRedirectExceptionHandler(IWebUtilityWrapper webUtilityWrapper) : base(webUtilityWrapper)
        {
        }

        public override bool IsMatchErrorHandler(AjaxRequestResult ajaxRequestResult, Exception exception)
        {
            var errorMessageResult = exception.Message.ToLower() == LoginPageNotFound;
            return errorMessageResult;
        }
    }
}
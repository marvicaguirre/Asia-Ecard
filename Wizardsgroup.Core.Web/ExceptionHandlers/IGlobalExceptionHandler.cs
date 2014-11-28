using System;

namespace Wizardsgroup.Core.Web.ExceptionHandlers
{
    public interface IGlobalExceptionHandler
    {        
        bool IsMatchErrorHandler(AjaxRequestResult ajaxRequestResult,Exception exception);
        void HandleException(Exception exception);
    }
}
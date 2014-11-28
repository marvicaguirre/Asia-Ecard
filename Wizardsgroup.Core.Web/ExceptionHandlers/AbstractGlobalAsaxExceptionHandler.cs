using System;
using Wizardsgroup.Core.Web.Constants;
using Wizardsgroup.Core.Web.Helpers;

namespace Wizardsgroup.Core.Web.ExceptionHandlers
{
    public abstract class AbstractGlobalAsaxExceptionHandler : IGlobalExceptionHandler
    {

        #region Properties
        public IWebUtilityWrapper WebUtilityWrapper { get; private set; }

        protected string LoginPageNotFound
        {
            get { return string.Format(ErrorMessage.LoginPageNotFound, CommonHelper.Instance.VirtualDirectory()).Replace("//", "/").ToLower(); }
        }

        #endregion

        #region Constructor
        protected AbstractGlobalAsaxExceptionHandler(IWebUtilityWrapper webUtilityWrapper)
        {
            WebUtilityWrapper = webUtilityWrapper;
        }
        #endregion

        public abstract bool IsMatchErrorHandler(AjaxRequestResult ajaxRequestResult, Exception exception);
        public abstract void HandleException(Exception exception);

        protected string SeekFullErrorMessage(Exception exception)
        {
            var fullErrorMessage = string.Empty;
            var exceptionNode = exception;
            while (exceptionNode != null)
            {
                fullErrorMessage += exceptionNode.Message + "; ";
                exceptionNode = exceptionNode.InnerException;
            }
            return fullErrorMessage.Replace("\r\n", "\t");
        }
    }
}
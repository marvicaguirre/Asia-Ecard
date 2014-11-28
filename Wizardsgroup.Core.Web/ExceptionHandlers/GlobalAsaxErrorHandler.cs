using System;
using System.Collections.Generic;
using Wizardsgroup.Utilities.Interface;

namespace Wizardsgroup.Core.Web.ExceptionHandlers
{
    public class GlobalAsaxErrorHandler
    {
        #region Members

        private readonly IReflection _reflection;
        private readonly IWebUtilityWrapper _webUtilityWrapper;
        private List<IGlobalExceptionHandler> _handlers; 
        #endregion

        #region Constructor
        public GlobalAsaxErrorHandler(IReflection reflection,IWebUtilityWrapper webUtilityWrapper)
        {
            _reflection = reflection;
            _webUtilityWrapper = webUtilityWrapper;
        }

        #endregion

        #region Public Functions/Method
        public void HandleException(Exception exception,Action<string> logAction)
        {            
            CreateErrorHandlers(_webUtilityWrapper);
            var ajaxRequestCheckerResult = _webUtilityWrapper.AjaxRequestChecker.RequestResult();
            logAction(exception.Message);
            _handlers.FindAll(o => o.IsMatchErrorHandler(ajaxRequestCheckerResult, exception))
                .ForEach(o => o.HandleException(exception));
        } 
        #endregion

        #region Private Function/Methods
        private void CreateErrorHandlers(IWebUtilityWrapper webUtilityWrapper)
        {
            var types = _reflection.GetTypesFromAssembly("Wizardsgroup.Core.Web");
            var globalExceptionHandlers = _reflection.GetTypesWithImplementingInterface<IGlobalExceptionHandler>(types);
            _handlers = new List<IGlobalExceptionHandler>();
            foreach (var handler in globalExceptionHandlers)
            {
                _handlers.Add(_reflection.CreateInstanceOfType<IGlobalExceptionHandler>(handler, webUtilityWrapper));
            }
        }
        #endregion
    }
}
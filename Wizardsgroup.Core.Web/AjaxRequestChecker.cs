using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web
{
    public class AjaxRequestChecker : IAjaxRequestChecker
    {
        #region Members
        private readonly HttpRequest _httpRequest;
        private const string XRequestedWith = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";
        #endregion

        #region Constructor
        public AjaxRequestChecker(HttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }
        #endregion

        #region Function/Methods
        public AjaxRequestResult RequestResult()
        {

            var requestResult = new AjaxRequestResult
                {
                    RequestController = string.Format("{0}",_httpRequest.RequestContext.RouteData.Values["controller"] ?? string.Empty),
                    RequestControllerAction =string.Format("{0}",_httpRequest.RequestContext.RouteData.Values["action"] ?? string.Empty)
                };

            return requestResult;
        }
        #endregion

        public void EvaluateAjaxRequest(AjaxRequestResult requestResult)
        {
            //The easy way
            requestResult.IsAjaxRequest = (_httpRequest[XRequestedWith] == XmlHttpRequest)
            || (_httpRequest.Headers[XRequestedWith] == XmlHttpRequest);

            //If we are not sure that we have an AJAX request or that we have to return JSON 
            //we fall back to Reflection
            if (!requestResult.IsAjaxRequest)
            {
                try
                {
                    //We create a controller instance
                    var controllerFactory = new DefaultControllerFactory();
                    var controller = controllerFactory.CreateController(_httpRequest.RequestContext, requestResult.RequestController) as Controller;
                    //We get the controller actions
                    if (controller != null)
                    {
                        var controllerDescriptor = new ReflectedControllerDescriptor(controller.GetType());
                        var controllerActions = controllerDescriptor.GetCanonicalActions();

                        //We search for our action
                        if (SeekControllerActionWithJson(requestResult, controllerActions))
                        {
                            requestResult.IsAjaxRequest = true;
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private bool SeekControllerActionWithJson(AjaxRequestResult requestResult, IEnumerable<ActionDescriptor> controllerActions)
        {
            return controllerActions.Cast<ReflectedActionDescriptor>()
                                    .Where(actionDescriptor => actionDescriptor.ActionName.ToUpper() == requestResult.RequestControllerAction.ToUpper())
                                    .Any(actionDescriptor => actionDescriptor.MethodInfo.ReturnType == typeof(JsonResult));
        }
    }
}
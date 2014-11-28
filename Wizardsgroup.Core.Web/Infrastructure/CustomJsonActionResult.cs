using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Infrastructure
{
    public class CustomJsonActionResult : ActionResult
    {
        private readonly string _jsonString;

        public CustomJsonActionResult(string jsonString)
        {
            _jsonString = jsonString;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var httpContext = context.HttpContext;
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.Write(_jsonString);
        }
    }
}
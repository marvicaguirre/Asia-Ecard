using System;
using System.Web;

namespace Wizardsgroup.Core.Web
{
    public class WebUtilityWrapper : IWebUtilityWrapper
    {
        private readonly HttpContext _context;
        public HttpRequest Request { get { return _context.Request; } }
        public HttpResponse Response { get { return _context.Response; } }
        public HttpServerUtility Server { get { return _context.Server; } }
        public IAjaxRequestChecker AjaxRequestChecker { get; private set; }

        public WebUtilityWrapper(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
            AjaxRequestChecker = new AjaxRequestChecker(Request);
        }
    }
}
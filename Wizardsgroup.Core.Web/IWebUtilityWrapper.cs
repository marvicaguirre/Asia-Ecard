using System.Web;

namespace Wizardsgroup.Core.Web
{
    public interface IWebUtilityWrapper
    {
        HttpRequest Request { get; }
        HttpResponse Response { get; }
        HttpServerUtility Server { get; }
        IAjaxRequestChecker AjaxRequestChecker { get; }
    }
}
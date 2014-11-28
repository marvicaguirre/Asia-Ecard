namespace Wizardsgroup.Core.Web
{
    public class AjaxRequestResult
    {
        public bool IsAjaxRequest { get; set; }
        public string RequestController { get; set; }
        public string  RequestControllerAction { get; set; }
    }
}
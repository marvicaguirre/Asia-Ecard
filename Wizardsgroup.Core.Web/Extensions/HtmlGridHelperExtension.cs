using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlGridHelperExtension
    {
        public static MvcHtmlString CustomGrid(this HtmlHelper helper, string gridName,string action, string controller, int? gridWidth, int? gridHeight)
        {
            return CustomGrid(helper, gridName, action, controller, gridWidth, gridHeight,50);
        }

        public static MvcHtmlString CustomGrid(this HtmlHelper helper, string gridName, string action, string controller, int? gridWidth, int? gridHeight,params string[] paramControls)
        {
            return CustomGrid(helper, gridName, action, controller, gridWidth, gridHeight, 50,false,false,true,false,paramControls);
        }

        public static MvcHtmlString CustomGrid(this HtmlHelper helper, string gridName, string action, string controller, int? gridWidth, int? gridHeight, int pageSize = 50, bool serverFiltering = false, bool serverPaging = false, bool autoBind = true, bool groupable = false,params string[] paramControls)
        {
            var url = new UrlHelper(helper.ViewContext.RequestContext);            
            string _theUrl = url.Action(action, controller);
            int _gridWidth = gridWidth.HasValue ? gridWidth.Value : 300;
            int _gridHeight = gridHeight.HasValue ? gridHeight.Value : 300;

            //if (!_HasAccess(controller, action))
            //    return new MvcHtmlString(string.Empty);
            //var script = "";
            var script = "<script> _grid_refreshCallback = undefined</script>";
            string returnMvcHtmlString = string.Format("<div controltype=\"kendoGrid\" id=\"{0}\" url=\"{1}\" gridwidth={2} gridheight={3} gridPageSize ={4} serverFiltering={5} serverPaging={6} autoBind = {7} groupable= {8} parameter=\"{9}\"></div>", gridName, _theUrl, _gridWidth, _gridHeight, pageSize, serverFiltering, serverPaging, autoBind, groupable, string.Join(",", paramControls));
            returnMvcHtmlString = script + returnMvcHtmlString;
            return new MvcHtmlString(returnMvcHtmlString);
        }
    }
}
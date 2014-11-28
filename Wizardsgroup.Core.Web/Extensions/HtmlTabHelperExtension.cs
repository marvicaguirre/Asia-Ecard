using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Wizardsgroup.Core.Web.Enums;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlTabHelperExtension
    {
        public static MvcHtmlString CustomLinkNewTab(this HtmlHelper helper, string text, string action, string controller, object routeValue, string tabCaption, BootstrapIcon icon)
        {
            return CustomLinkNewTab(helper, text, action, controller, routeValue, tabCaption, icon.ToString().ToLower());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="text"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="routeValue"></param>
        /// <param name="tabCaption"></param>
        /// <param name="icon">icon file from the BootStrap CSS framework; see "http://twitter.github.io/bootstrap/base-css.html#icons"</param>
        /// <returns></returns>
        public static MvcHtmlString CustomLinkNewTab(this HtmlHelper helper, string text, string action, string controller, object routeValue, string tabCaption, string icon = "")
        {
            //var url = new UrlHelper(helper.ViewContext.RequestContext);

            //note: generate the ModuleId before modifying the Text
            string moduleId = _CleanUpSpecialCharacters(tabCaption);

            text = GetLinkText(text, icon);

            const string placeHolder = "[replace]";
            string theUrl = helper.ActionLink(placeHolder, action, controller, routeValue, new { @class = "linkNewTabClass" }).ToString();
            theUrl = theUrl.Replace(placeHolder, text);
            theUrl = theUrl.Replace("href=", "url=");
            theUrl = theUrl.Replace("<a ", string.Format(@"<a href='javascript:void(0);' tabText='{0}' moduleId='{1}' ", tabCaption, moduleId));
            //string returnMvcHtmlString = string.Format("<a href=\"javascript:void(0);\" class=\"linkNewTabClass\" url=\"{0}\" tabText=\"{1}\">{2}</a>", theUrl, tabCaption, text);
            string returnMvcHtmlString = theUrl;

            return new MvcHtmlString(returnMvcHtmlString);
        }


        private static string _CleanUpSpecialCharacters(string text)
        {
            var specialChars = new string[] { " ", "(", ")" };
            StringBuilder sb = new StringBuilder();
            sb.Append(text);
            //return text.Replace(" ", "").Replace("(", "").Replace(")", "");
            var theText = specialChars.Aggregate(sb, (current, oldValue) => current.Replace(oldValue, "")).ToString();
            return theText;
        }

        private static string GetLinkText(string text, string icon)
        {
            if (!string.IsNullOrEmpty(icon) && isBootStrapAvailable())
            {
                if (icon.Contains("icon-"))
                {
                    throw new Exception("Please do not send an icon parameter with the \"icon-\" prefix");
                }
                //==========================================================
                //note: this is dependent on the Bootstrap CSS framework!
                //==========================================================
                return string.Format("<i class=\"icon-{0}\"></i> {1}", icon, text);
            }
            return text;
        }

        /// <summary>
        /// Check if the BootStrap CSS framework is available
        /// </summary>
        /// <returns></returns>
        private static bool isBootStrapAvailable()
        {
            //TODO: get from config file
            return true;
        }
    }
}
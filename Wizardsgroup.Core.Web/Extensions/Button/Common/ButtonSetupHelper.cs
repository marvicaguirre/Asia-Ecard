using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Wizardsgroup.Core.Web.Helpers;
using Wizardsgroup.Utilities.Helpers;

namespace Wizardsgroup.Core.Web.Extensions
{
    public sealed class ButtonSetupHelper
    {

        #region Members and Properties
        public static ButtonSetupHelper Instance
        {
            get { return Singleton<ButtonSetupHelper>.Instance; }
        }
        #endregion

        #region Constructor
        private ButtonSetupHelper()
        {

        } 
        #endregion

        #region Public Functions

        public UrlHelper GetUrlHelper(HtmlHelper htmlHelper)
        {
            return new UrlHelper(htmlHelper.ViewContext.RequestContext);
        }

        public string GetDefaultButtonStyle()
        {
            if (IsBootStrapAvailable())
            {
                return " btn btn-info btn-block";
            }
            return string.Empty;
        }

        public string GetBootStrapIconForCreateButton()
        {
            const string icon = "icon-plus";
            return IsBootStrapAvailable() ? string.Format(GetBootStrapIconTemplate(), icon) : "";
        }

        public string GetBootStrapIconForDeleteButton()
        {
            const string icon = "icon-trash";
            return IsBootStrapAvailable() ? string.Format(GetBootStrapIconTemplate(), icon) : "";
        }

        public StringBuilder GenerateAttributes(IEnumerable<dynamic> attrs)
        {
            var attrBuilder = new StringBuilder();
            foreach (var attr in attrs)
            {
                attrBuilder.Append(string.Format("{0}=\"{1}\"", attr.Key, attr.Value));
                attrBuilder.Append(" ");
            }
            return attrBuilder;
        }

        public bool IsRequestedActionValid(string action, string controller)
        {
            //return true;
            //TODO match correct action to function
            return SecurityHelper.IsRequestedActionValid(action, controller);
        } 
        #endregion

        #region Private Functions
        private string GetBootStrapIconTemplate()
        {
            //note: don't forget the space at the end
            return "<i class=\"{0}\"></i> ";
        }

        /// <summary>
        /// Check if the BootStrap CSS framework is available
        /// </summary>
        /// <returns></returns>
        private bool IsBootStrapAvailable()
        {
            //TODO: get from config file
            return true;
        } 
        #endregion
    }
}
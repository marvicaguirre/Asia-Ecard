using System.Collections.Generic;
using System.Web.Routing;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ModalRendererStrategy : IButtonRendererStrategy
    {
        public IButtonTemplateAction InitializeTemplateAction()
        {
            IButtonTemplateAction templateAction = new ButtonTemplateAction();

            templateAction.GenerateAttribute = (helper, container) =>
            {
                var route = new RouteValueDictionary(container.Route);
                var url = container.Route != null
                    ? helper.GetUrlHelper(container.HtmlHelper).Action(container.Controller.TargetAction, container.Controller.Controller, route)
                    : helper.GetUrlHelper(container.HtmlHelper).Action(container.Controller.TargetAction, container.Controller.Controller);

                container.Button.ClassName += helper.GetDefaultButtonStyle();
                var attributes = new List<dynamic>
                            {
                                new  { Key = "class", Value = string.Format("{0}", container.Button.ClassName)}
                                , new  { Key = "url", Value = string.Format("{0}", url)}
                                , new  { Key = "modaltitle", Value = string.Format("{0}", container.Modal.Title)}
                                , new  { Key = "modalwidth", Value = string.Format("{0}", container.Modal.Width ?? 0)}
                                , new  { Key = "modalheight", Value = string.Format("{0}", container.Modal.Height ?? 0)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", container.Button.Width ?? 100)}
                                , new  { Key = "gridname", Value = string.Format("{0}", container.Button.GridName)}
                                , new  { Key = "actionName", Value = string.Format("{0}", container.Controller.TargetAction)}
                                , new  { Key = "parentId", Value = string.Format("{0}", container.Button.ParentKey)}
                                , new  { Key = "autoClose", Value = string.Format("{0}", container.Modal.AutoClose)}
                                , new  { Key = "withConfirm", Value = string.Format("{0}", container.Modal.ConfirmWindow ? "yes" : "no")}
                                , !helper.IsRequestedActionValid(container.Controller.TargetAction, container.Controller.Controller) ? new  { Key = "disabled", Value =true } : new  { Key = "openAccess", Value =true }
                            };
                return attributes;
            };

            templateAction.GenerateHtml = (helper, container, attrs) =>
            {
                var attrBuilder = helper.GenerateAttributes(attrs);
                var bootStrapIcon = helper.GetBootStrapIconForCreateButton();
                var htmlString = string.Format("<a {0}>{1}{2}</a>", attrBuilder, bootStrapIcon, container.Button.Text);
                return htmlString;
            };

            return templateAction;
        }
    }
}
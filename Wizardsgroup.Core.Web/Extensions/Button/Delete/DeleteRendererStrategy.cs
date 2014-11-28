using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class DeleteRendererStrategy : IButtonRendererStrategy
    {
        public IButtonTemplateAction InitializeTemplateAction()
        {
            IButtonTemplateAction templateAction = new ButtonTemplateAction();

            templateAction.GenerateAttribute = (helper, container) =>
            {
                var action = string.IsNullOrEmpty(container.Controller.ConfirmAction) ? "DisplaySelectedItems" : container.Controller.ConfirmAction;
                var targetAction = string.IsNullOrEmpty(container.Controller.TargetAction) ? "DeleteMultipleConfirmed" : container.Controller.TargetAction;
                var validationAction = string.IsNullOrEmpty(container.Controller.ValidationAction) ? string.Empty : container.Controller.ValidationAction;
                var url = helper.GetUrlHelper(container.HtmlHelper).Action(action, container.Controller.Controller);
                var targetUrl = helper.GetUrlHelper(container.HtmlHelper).Action(targetAction, container.Controller.Controller);                

                container.Button.ClassName += helper.GetDefaultButtonStyle();

                var attributes = new List<dynamic>
                                {
                                    new  { Key = "class", Value = string.Format("{0}", container.Button.ClassName)}
                                    , new  { Key = "url", Value = string.Format("{0}", url)}
                                    , new  { Key = "targetUrl", Value = string.Format("{0}", targetUrl)}
                                    , new  { Key = "validationAction", Value = string.Format("{0}", validationAction)}
                                    , new  { Key = "controller", Value = string.Format("{0}", container.Controller.Controller)}
                                    , new  { Key = "selectionMode", Value = string.Format("{0}", container.SelectionMode)}
                                    , new  { Key = "modaltitle", Value = string.Format("{0}", container.Modal.Title)}
                                    , new  { Key = "modalwidth", Value = string.Format("{0}", container.Modal.Width ?? 0)}
                                    , new  { Key = "modalheight", Value = string.Format("{0}", container.Modal.Height ?? 0)}
                                    , new  { Key = "style", Value = string.Format("width:{0}px", container.Button.Width ?? 100)}
                                    , new  { Key = "gridname", Value = string.Format("{0}", container.Button.GridName)}                                    
                                    , !helper.IsRequestedActionValid(targetAction, container.Controller.Controller) ? new  { Key = "disabled", Value =true} : new  { Key = "openAccess", Value =true }
                                };
                return attributes;
            };
            
            templateAction.GenerateHtml = (helper, container, attributes) =>
            {
                var bootStrapIcon = helper.GetBootStrapIconForDeleteButton();
                var attrBuilder = helper.GenerateAttributes(attributes);
                var htmlString = string.Format("<a {0}>{1}{2}</a>", attrBuilder, bootStrapIcon, container.Button.Text);
                return htmlString;
            };

            return templateAction;
        }
    }
}
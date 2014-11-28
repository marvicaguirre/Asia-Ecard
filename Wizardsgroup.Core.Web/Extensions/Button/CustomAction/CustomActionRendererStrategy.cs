using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class CustomActionRendererStrategy : IButtonRendererStrategy
    {
        public IButtonTemplateAction InitializeTemplateAction()
        {
            IButtonTemplateAction templateAction = new ButtonTemplateAction();

            templateAction.GenerateAttribute = (helper, container) =>
            {
                var url = helper.GetUrlHelper(container.HtmlHelper).Action(container.Controller.TargetAction, container.Controller.Controller);

                container.Button.ClassName += helper.GetDefaultButtonStyle();
                var attributes = new List<dynamic>
                            {
                                new  { Key = "class", Value = string.Format("{0}", container.Button.ClassName)}
                                , new  { Key = "url", Value = string.Format("{0}", url)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", container.Button.Width ?? 100)}
                                , new  { Key = "gridname", Value = string.Format("{0}", container.Button.GridName)}
                                , new  { Key = "methodName", Value = string.Format("{0}", container.ClientAction.Action)}
                                , new  { Key = "parentId", Value = string.Format("{0}", container.Button.ParentKey)}
                                , new  { Key = "targetLevel", Value = string.Format("{0}", container.ClientAction.TargetLevel)}
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
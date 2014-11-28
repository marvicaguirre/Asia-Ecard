using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ButtonHtmlRenderer : IButtonHtmlRenderer
    {
        private readonly ButtonConfigurationContainer _configuration;
        private readonly IButtonRendererStrategy _buttonRendererStrategy;

        public ButtonHtmlRenderer(ButtonConfigurationContainer configuration,IButtonRendererStrategy buttonRendererStrategy)
        {
            _configuration = configuration;
            _buttonRendererStrategy = buttonRendererStrategy;
        }

        public MvcHtmlString Render()
        {
            var template = _buttonRendererStrategy.InitializeTemplateAction();            
            var attributes = template.GenerateAttribute(ButtonSetupHelper.Instance, _configuration);
            var html = template.GenerateHtml(ButtonSetupHelper.Instance, _configuration, attributes);
            return new MvcHtmlString(html);
        }
    }
}
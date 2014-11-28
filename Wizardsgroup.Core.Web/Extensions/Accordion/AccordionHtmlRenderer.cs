using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class AccordionHtmlRenderer : IAccordionHtmlRenderer
    {
        private readonly AccordionConfigurationContainer _configurationContainer;

        public AccordionHtmlRenderer(AccordionConfigurationContainer configurationContainer)
        {
            _configurationContainer = configurationContainer;
        }

        public MvcHtmlString Render()
        {
            var panelbarWrapper = new KendoPanelbarWrapper(_configurationContainer);
            var panelbar = panelbarWrapper.Build();
            var htmlString = panelbar.ToHtmlString();
            return new MvcHtmlString(htmlString);
        }
    }
}
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class TabstripHtmlRenderer : ITabstripHtmlRenderer
    {
        private readonly TabstripItemConfigurationContainer _configurationContainer;

        public TabstripHtmlRenderer(TabstripItemConfigurationContainer configurationContainer)
        {
            configurationContainer.Guard("TabstripItemConfigurationContainer must not be null.");
            _configurationContainer = configurationContainer;
        }

        public MvcHtmlString Render()
        {
            var panelbarWrapper = new KendoTabstripWrapper(_configurationContainer);
            var panelbar = panelbarWrapper.Build();
            var htmlString = panelbar.ToHtmlString();
            return new MvcHtmlString(htmlString);
        }
    }
}
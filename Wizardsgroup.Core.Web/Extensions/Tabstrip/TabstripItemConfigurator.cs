using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class TabstripItemConfigurator : ITabstripItemConfigurator
    {
        private readonly TabstripItemConfigurationContainer _configurationContainer;

        public TabstripItemConfigurator(TabstripItemConfigurationContainer configurationContainer)
        {
            configurationContainer.Guard("TabstripItemConfigurationContainer must not be null.");
            _configurationContainer = configurationContainer;
        }

        public ITabstripItemContentConfigurator TabItemFor(string tabstripTitle)
        {
            tabstripTitle.Guard("Title must not be not null or empty.");
            var item = new TabstripItemContainer(_configurationContainer.UpdateTabItems)
            { Title = tabstripTitle,};
            _configurationContainer.AddItemTabstrip(item);
            return new TabstripItemContentConfigurator(this, item);
        }
    }
}
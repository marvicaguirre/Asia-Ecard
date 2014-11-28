using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class AccordionItemConfigurator : IAccordionItemConfigurator
    {
        private readonly AccordionConfigurationContainer _configurationContainer;

        public AccordionItemConfigurator(AccordionConfigurationContainer configurationContainer)
        {
            configurationContainer.Guard("AccordionConfigurationContainer must not be null.");
            _configurationContainer = configurationContainer;
        }

        public IAccordionItemContentConfigurator ItemFor(string title)
        {
            title.Guard("Title must not be not null or empty.");
            var item = new AccordionItemContainer { Title = title,};
            _configurationContainer.Contents.Add(item);
            return new AccordionItemContentConfigurator(this, item);
        }
    }
}
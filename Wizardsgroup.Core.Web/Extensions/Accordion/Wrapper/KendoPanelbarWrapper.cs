using System.Collections.Generic;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal sealed class KendoPanelbarWrapper
    {
        #region Members
        private readonly AccordionConfigurationContainer _configurationContainer; 
        #endregion

        #region Constructor
        public KendoPanelbarWrapper(AccordionConfigurationContainer configurationContainer)
        {
            _configurationContainer = configurationContainer;
        } 
        #endregion

        #region Public Functions/Methods
        public PanelBar Build()
        {
            var expandMode = GeExpandMode();
            var contents = GetItemFromConfiguratorAction();
            var specificationProcessor = new AccordionContentSpecificationProcessor();
            var kendoWidgetFactory = new WidgetFactory(_configurationContainer.HtmlHelper);
            var panelBar = kendoWidgetFactory.PanelBar();
            panelBar.Name(_configurationContainer.Name)
                    .ExpandMode(expandMode)
                    .Items(item => contents.ForEach(accordionItem => CreatePanelbarItem(item, accordionItem, specificationProcessor)));
            return panelBar;
        } 
        #endregion

        #region Private Functions/Methods
        private void CreatePanelbarItem(PanelBarItemFactory item, AccordionItemContainer accordionItem,IAccordionContentSpecificationProcessor specificationProcessor)
        {
            var content = specificationProcessor.GetContent(accordionItem);
            item.Add().Text(accordionItem.Title).Expanded(accordionItem.Expanded)
                .Content(content);
        }

        private List<AccordionItemContainer> GetItemFromConfiguratorAction()
        {
            IAccordionItemConfigurator item = new AccordionItemConfigurator(_configurationContainer);
            _configurationContainer.AccordionItemAction(item);
            return _configurationContainer.Contents;
        }

        private PanelBarExpandMode GeExpandMode()
        {
            return _configurationContainer.ExpandMode == ExpandMode.Multiple ? PanelBarExpandMode.Multiple : PanelBarExpandMode.Single;
        } 
        #endregion
    }
}
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class KendoTabstripWrapper
    {
        #region Members
        private readonly TabstripItemConfigurationContainer _configurationContainer;
        #endregion

        #region Constructor
        public KendoTabstripWrapper(TabstripItemConfigurationContainer configurationContainer)
        {
            _configurationContainer = configurationContainer;
        }
        #endregion

        #region Public Function/Methods
        public TabStrip Build()
        {
            var contents = GetItemFromConfiguratorAction();
            var specificationProcessor = new TabContentSpecificationProcessor();
            var kendoWidgetFactory = new WidgetFactory(_configurationContainer.HtmlHelper);
            var tabStrip = kendoWidgetFactory.TabStrip();
            tabStrip.Name(_configurationContainer.Name)
                    .HtmlAttributes(_configurationContainer.HtmlAttributes)
                    .Items(item => contents.ForEach(tabstripItem => CreatePanelbarItem(item, tabstripItem, specificationProcessor)))
                    .Animation(anim => anim.Enable(false));
                    //.Animation(anim=>anim
                    //    .Open(effect=>effect.Fade(FadeDirection.In))
                    //    .Close(effect=>effect.Fade(FadeDirection.Out)));
            return tabStrip;
        } 
        #endregion

        #region Private Function/Methods
        private void CreatePanelbarItem(TabStripItemFactory item, TabstripItemContainer tabstripItemContainer, ITabContentSpecificationProcessor specificationProcessor)
        {
            var content = specificationProcessor.GetContent(tabstripItemContainer);
            item.Add().Text(tabstripItemContainer.Title).Selected(tabstripItemContainer.Selected)
                .Content(content);
        }

        private List<TabstripItemContainer> GetItemFromConfiguratorAction()
        {
            ITabstripItemConfigurator item = new TabstripItemConfigurator(_configurationContainer);
            _configurationContainer.TabstripItemAction(item);
            return _configurationContainer.Contents.ToList();
        } 
        #endregion
    }
}
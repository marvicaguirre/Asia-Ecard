using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class TabstripItemConfigurationContainer
    {
        #region Member
        private readonly List<TabstripItemContainer> _content; 
        #endregion
        
        #region Constructor
        public TabstripItemConfigurationContainer()
        {
            _content = new List<TabstripItemContainer>();
        } 
        #endregion

        #region Properties
        public string Name { get; set; }
        public HtmlHelper HtmlHelper { get; set; }
        public IReadOnlyCollection<TabstripItemContainer> Contents { get { return _content; } }
        public Action<ITabstripItemConfigurator> TabstripItemAction { get; set; }
        public object HtmlAttributes { get; set; }
        #endregion

        #region Public Function/Methods
        public void AddItemTabstrip(TabstripItemContainer item)
        {
            _content.Add(item);
        }

        public void UpdateTabItems()
        {
            var lastItem = _content.Last();
            if (lastItem.Selected)
            {
                _content.FindAll(itemFromCollection => !itemFromCollection.Equals(lastItem))
                    .ForEach(itemFromCollection => itemFromCollection.Selected = false);
            }
        } 
        #endregion
    }
}
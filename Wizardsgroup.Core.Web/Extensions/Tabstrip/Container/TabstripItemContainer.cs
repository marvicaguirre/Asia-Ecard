using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class TabstripItemContainer
    {
        private readonly Action _updateTabItems;
        private bool _selected;

        public TabstripItemContainer(Action updateTabItems)
        {
            _updateTabItems = updateTabItems;
        }

        public string Title { get; set; }
        public object Content { get; set; }
        public bool Selected
        {
            get { return _selected; } 
            set
            {
                _selected=value;
                if (value)
                {
                    _updateTabItems();
                }
            }
        }
    }
}
using System.Collections.Generic;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ConfirmMessageContainer<TModel>
    {
        public ConfirmMessageContainer()
        {
            Items = new List<ConfirmMessageItem>();
        }
        public HtmlHelper<TModel> HtmlHelper { get; set; }
        public string Name { get; set; }
        public List<ConfirmMessageItem> Items { get; private set; }
    }
}
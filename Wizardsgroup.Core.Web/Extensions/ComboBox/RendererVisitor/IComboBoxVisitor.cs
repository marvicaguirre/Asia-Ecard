using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal interface IComboBoxVisitor<TModel, TValue>
    {
        MvcHtmlString ComboBox { get; set; }
        void Visit(ComboBoxHtmlRenderer<TModel, TValue> comboBox);
    }
}
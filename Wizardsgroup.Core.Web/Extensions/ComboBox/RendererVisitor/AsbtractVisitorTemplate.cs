using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal abstract class AsbtractVisitorTemplate<TModel, TValue> : IComboBoxVisitor<TModel, TValue>
    {
        protected AttributeConfigurator<TModel, TValue> AttributeConfigurator { get; private set; }

        protected AsbtractVisitorTemplate(AttributeConfigurator<TModel, TValue> attributeConfigurator)
        {
            AttributeConfigurator = attributeConfigurator;
        }

        public MvcHtmlString ComboBox { get; set; }
        public abstract void Visit(ComboBoxHtmlRenderer<TModel, TValue> comboBox);
    }
}
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class RegularVisitor<TModel, TValue> : AsbtractVisitorTemplate<TModel, TValue>
    {
        public RegularVisitor(AttributeConfigurator<TModel, TValue> attributeConfigurator) : base(attributeConfigurator)
        {
        }

        public override void Visit(ComboBoxHtmlRenderer<TModel, TValue> comboBox)
        {
            var configuration = comboBox.Configuration;
            var value = AttributeConfigurator.GetValueFromMetaData(configuration);
            //Note Temporary Fixed For not nullable guid.
            if (value == Guid.Empty.ToString()) value = string.Empty;
            var attributes = AttributeConfigurator.SetBaseAttribute(configuration);
            attributes["service"] = configuration.DataSource as string;
            attributes["controltype"] = "kendoComboBox";

            var combox = configuration.HtmlHelper.TextBox(ExpressionHelper.GetExpressionText(configuration.TargetExpression), value, attributes).ToString();
            ComboBox = new MvcHtmlString(combox);
        }
    }
}
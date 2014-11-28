using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class LinkedForVisitor<TModel, TValue> : AsbtractVisitorTemplate<TModel, TValue>
    {
        public LinkedForVisitor(AttributeConfigurator<TModel, TValue> attributeConfigurator) : base(attributeConfigurator)
        {
        }

        public override void Visit(ComboBoxHtmlRenderer<TModel, TValue> comboBox)
        {
            var configuration = comboBox.Configuration;
            var cascadeFromMetadata = ModelMetadata.FromLambdaExpression(configuration.CascadeFromExpression, configuration.HtmlHelper.ViewData);
            var cascadeFrom = string.Format("{0}{1}", cascadeFromMetadata.PropertyName, configuration.Index);

            var value = AttributeConfigurator.GetValueFromMetaData(configuration);
            if (value == Guid.Empty.ToString()) value = string.Empty;

            var attributes = AttributeConfigurator.SetBaseAttribute(configuration);
            attributes["service"] = configuration.DataSource as string;
            attributes["controltype"] = "kendoComboBoxLinked";
            attributes["cascadeFrom"] = cascadeFrom;

            var combox = configuration.HtmlHelper.TextBox(ExpressionHelper.GetExpressionText(configuration.TargetExpression), value, attributes).ToString();
            ComboBox = new MvcHtmlString(combox);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ComboBoxHtmlRenderer<TModel, TValue> : IComboBoxHtmlRenderer<TModel, TValue>
    {
        internal ComboBoxConfigContainer<TModel, TValue> Configuration { get; private set; }

        public ComboBoxHtmlRenderer(ComboBoxConfigContainer<TModel, TValue> configuration)
        {
            Configuration = configuration;
        }

        public MvcHtmlString RenderHtml()
        {
            var controlType = GetControlTypeFromDataSourceConfiguration();
            var visitor = GetVisitorFromControlType(controlType);
            Accept(visitor);
            return visitor.ComboBox;
        }

        private void Accept(IComboBoxVisitor<TModel, TValue> visitor)
        {
            visitor.Visit(this);
        }

        private string GetControlTypeFromDataSourceConfiguration()
        {            
            //TODO refactor this when other functionality for datasource is supported
            var controlTypeDictionary = new Dictionary<string, object>
            {
                {ControlTypeConstant.KendoComboBox, Configuration.CascadeFromExpression == null ? Configuration.DataSource as string : null},
                {ControlTypeConstant.KendoComboBoxLinked,Configuration.CascadeFromExpression == null ? null : Configuration.DataSource as string},
                {ControlTypeConstant.KendoComboBoxForCustomData, Configuration.CascadeFromExpression == null ? Configuration.DataSource as Action<ICustomActionDataSource> : null},
                {ControlTypeConstant.KendoComboBoxForCustomDataLinkedFor, Configuration.CascadeFromExpression == null ? null : Configuration.DataSource as Action<ICustomActionDataSource>}
            };
            var control = controlTypeDictionary.Single(o => o.Value != null);
            return control.Key;
        }

        private IComboBoxVisitor<TModel, TValue> GetVisitorFromControlType(string controlType)
        {
            //TODO create visitor factory
            var attributeConfigurator = new AttributeConfigurator<TModel, TValue>();
            var visitorDictionary = new Dictionary<string, IComboBoxVisitor<TModel, TValue>>
            {
                {ControlTypeConstant.KendoComboBox, new RegularVisitor<TModel, TValue>(attributeConfigurator)},
                {ControlTypeConstant.KendoComboBoxLinked,new LinkedForVisitor<TModel, TValue>(attributeConfigurator)},
                {ControlTypeConstant.KendoComboBoxForCustomData, new CustomDataVisitor<TModel, TValue>(attributeConfigurator)},
                {ControlTypeConstant.KendoComboBoxForCustomDataLinkedFor, new CustomDataLinkedForVisitor<TModel, TValue>(attributeConfigurator)}
            };

            var visitor = visitorDictionary.Single(o => o.Key.Equals(controlType));
            return visitor.Value;
        }
    }
}
﻿using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class CustomDataLinkedForVisitor<TModel, TValue> : AsbtractVisitorTemplate<TModel, TValue>
    {
        public CustomDataLinkedForVisitor(AttributeConfigurator<TModel, TValue> attributeConfigurator) : base(attributeConfigurator)
        {
        }

        public override void Visit(ComboBoxHtmlRenderer<TModel, TValue> comboBox)
        {
            var configuration = comboBox.Configuration;
            var urlParameterContainer = ConfigureUrlAndParameter(configuration);

            var cascadeFromMetadata = ModelMetadata.FromLambdaExpression(configuration.CascadeFromExpression, configuration.HtmlHelper.ViewData);
            var cascadeFrom = string.Format("{0}{1}", cascadeFromMetadata.PropertyName, configuration.Index);

            var value = AttributeConfigurator.GetValueFromMetaData(configuration);
            if (value == Guid.Empty.ToString()) value = string.Empty;

            var attributes = AttributeConfigurator.SetBaseAttribute(configuration);
            attributes["url"] = urlParameterContainer.Url;
            attributes["controltype"] = "kendoComboBoxForCustomDataLinkedFor";
            attributes["parameter"] = urlParameterContainer.Parameter;
            attributes["cascadeFrom"] = cascadeFrom;

            var combox = configuration.HtmlHelper.TextBox(ExpressionHelper.GetExpressionText(configuration.TargetExpression), value, attributes).ToString();
            ComboBox = new MvcHtmlString(combox);
        }

        private UrlParameterContainer ConfigureUrlAndParameter(ComboBoxConfigContainer<TModel, TValue> configuration)
        {
            UrlParameterContainer urlParameterContainer;
            var customDataSourceAction = (Action<ICustomActionDataSource>)configuration.DataSource;
            var customDataSource = new CustomActionDataSource();
            customDataSourceAction(customDataSource);

            if (customDataSource.Configuration.Url != null)
            {
                urlParameterContainer = new UrlParameterContainer(customDataSource.Configuration.Url, customDataSource.Configuration.Parameter);
            }
            else
            {
                var readAction = new ReadAction();
                customDataSource.Configuration.ReadAction(readAction);
                var urlHelper = new UrlHelper(configuration.HtmlHelper.ViewContext.RequestContext);
                var url = urlHelper.Action(readAction.Configuration.Action, readAction.Configuration.Controller);
                urlParameterContainer = new UrlParameterContainer(url, readAction.Configuration.Parameter);
            }
            return urlParameterContainer;
        }
    }
}
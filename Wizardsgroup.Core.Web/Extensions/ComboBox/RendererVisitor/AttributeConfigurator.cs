using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Wizardsgroup.Core.Web.Infrastructure.Storage;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class AttributeConfigurator<TModel, TValue>
    {
        public RouteValueDictionary SetBaseAttribute(ComboBoxConfigContainer<TModel, TValue> configuration)
        {
            var attributes = configuration.HtmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(configuration.HtmlAttributes);            
            attributes["serverFiltering"] = false;
            attributes["placeHolder"] = configuration.PlaceHolder;
            attributes["customType"] = "text";
            attributes["name"] = GetFieldName(configuration);
            attributes["id"] = string.Format("{0}{1}",GetFieldName(configuration),configuration.Index);
            attributes["onchange"] = StringifyOnChange(configuration.ClientEvent);
            attributes["onblur"] = StringifyOnBlur(configuration.ClientEvent);
            attributes["key"] = configuration.Key;
            StatefulStorage.PerSession.GetOrAdd(configuration.Key, () => configuration.Specification);
            return attributes;
        }

        public ModelMetadata GetMetadata(ComboBoxConfigContainer<TModel, TValue> configuration)
        {
            return ModelMetadata.FromLambdaExpression(configuration.TargetExpression, configuration.HtmlHelper.ViewData);
        }

        public string GetValueFromMetaData(ComboBoxConfigContainer<TModel, TValue> configuration)
        {
            var value = GetMetadata(configuration).Model == null ? string.Empty : GetMetadata(configuration).Model.ToString();
            return value;
        }

        public string GetFieldName(ComboBoxConfigContainer<TModel, TValue> configuration)
        {
            var fieldId = GetMetadata(configuration).PropertyName;
            return fieldId;
        }

        private string StringifyOnChange(Action<IClientEvent> clientAction)
        {            
            var clientEvent = new ClientEvent();            
            if (clientAction != null) clientAction(clientEvent);
            if (!clientEvent.ClientEventContainer.ListOfClientChangeEvents.Any()) return string.Empty;

            var aggregateClientEvent =new StringBuilder();
            foreach (var evt in clientEvent.ClientEventContainer.ListOfClientChangeEvents)
            {
                aggregateClientEvent.Append(string.Format("$(this).change({0}); ", evt));
            }
            return aggregateClientEvent.ToString();
        }

        private string StringifyOnBlur(Action<IClientEvent> clientAction)
        {
            var clientEvent = new ClientEvent();
            if (clientAction != null) clientAction(clientEvent);
            if (!clientEvent.ClientEventContainer.ListOfClientBlurEvents.Any()) return string.Empty;

            var aggregateClientEvent = new StringBuilder();
            foreach (var evt in clientEvent.ClientEventContainer.ListOfClientBlurEvents)
            {
                aggregateClientEvent.Append(string.Format("$(this).blur({0}); ", evt));
            }
            return aggregateClientEvent.ToString();
        }

    }
}
using System;
using System.Web.Mvc;
using Wizardsgroup.Domain.Base;
using Wizardsgroup.Service.Specification;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ComboBoxConfigurator<TModel, TValue> : IComboBoxConfigurator<TModel, TValue>
    {
        private readonly ComboBoxConfigContainer<TModel, TValue> _configuration;

        public ComboBoxConfigurator(ComboBoxConfigContainer<TModel, TValue> configuration)
        {
            _configuration = configuration;
        }

        public IComboBoxConfigurator<TModel, TValue> SubcribeToEvent(Action<IClientEvent> clientEvent)
        {
            clientEvent.Guard("ClientEvent must not be null.");
            _configuration.ClientEvent = clientEvent;
            return this;
        }

        public IComboBoxConfigurator<TModel, TValue> FilterSpecification<T>(Func<ISpecification<T>> filterSpecification) where T : AbstractBaseModel, new()
        {            
            _configuration.Specification = filterSpecification;
            return this;
        }

        public IComboBoxConfigurator<TModel, TValue> PlaceHolder(string placeHolder)
        {            
            _configuration.PlaceHolder = placeHolder ?? "Select A Record";
            return this;
        }

        public IComboBoxConfigurator<TModel, TValue> HtmlAttribute(object attributes)
        {
            attributes.Guard("HtmlAttributes must not be null.");
            _configuration.HtmlAttributes = attributes;
            return this;
        }

        public IComboBoxConfigurator<TModel, TValue> Index(string index)
        {            
            _configuration.Index = index ?? string.Empty;
            return this;
        }

        public MvcHtmlString RenderHtml()
        {
            return new ComboBoxHtmlRenderer<TModel, TValue>(_configuration).RenderHtml();
        }
    }
}
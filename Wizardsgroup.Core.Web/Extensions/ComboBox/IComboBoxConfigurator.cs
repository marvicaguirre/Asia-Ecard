using System;
using System.Web.Mvc;
using Wizardsgroup.Domain.Base;
using Wizardsgroup.Service.Specification;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IComboBoxConfigurator<TModel, TValue>
    {
        IComboBoxConfigurator<TModel, TValue> SubcribeToEvent(Action<IClientEvent> clientEvent);
        IComboBoxConfigurator<TModel, TValue> FilterSpecification<T>(Func<ISpecification<T>> filterSpecification) where T : AbstractBaseModel, new();
        IComboBoxConfigurator<TModel, TValue> PlaceHolder(string placeHolder);
        IComboBoxConfigurator<TModel, TValue> HtmlAttribute(object attributes);
        IComboBoxConfigurator<TModel, TValue> Index(string index);
        MvcHtmlString RenderHtml();
    }
}
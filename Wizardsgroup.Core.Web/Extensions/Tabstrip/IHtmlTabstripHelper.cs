using System;
using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IHtmlTabstripHelper
    {
        IHtmlTabstripHelper HtmlAttributes(object attributes);
        IHtmlTabstripHelper HtmlAttributes(IDictionary<string,object> attributes);
        ITabstripHtmlRenderer Content(Action<ITabstripItemConfigurator> itemRegistration);
    }
}
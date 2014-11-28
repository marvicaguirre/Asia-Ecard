using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ITabstripItemContentConfigurator
    {
        ITabstripItemContentConfigurator Selected(bool selected = true);
        ITabstripItemConfigurator Content(Func<object, object> value);
        ITabstripItemConfigurator Content(MvcHtmlString mvcHtmlString);    
    }
}
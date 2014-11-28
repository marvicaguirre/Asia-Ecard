using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ITemplateHtmlSourceRenderer<TModel, TValue> where TValue : IEnumerable<object>
    {        
        void RenderHtml();
    }
}
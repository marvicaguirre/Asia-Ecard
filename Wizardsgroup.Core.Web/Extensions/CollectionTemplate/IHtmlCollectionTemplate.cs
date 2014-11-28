using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IHtmlCollectionTemplate<TModel, TValue> where TValue : IEnumerable<object>
    {
        ITemplateHtmlSourceRenderer<TModel, TValue> TemplateSource(string template);        
    }
}
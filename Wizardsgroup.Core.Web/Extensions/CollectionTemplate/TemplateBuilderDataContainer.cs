using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public class TemplateBuilderDataContainer<TModel, TValue>
    {
        public HtmlHelper<TModel> HtmlHelper { get; set; }
        public Expression<Func<TModel, TValue>> Expression { get; set; }
        public string PartialViewName { get; set; }
    }
}
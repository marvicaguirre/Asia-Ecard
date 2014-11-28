using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class HtmlCollectionTemplate<TModel, TValue> : IHtmlCollectionTemplate<TModel, TValue> where TValue : IEnumerable<object>
    {
        private readonly TemplateBuilderDataContainer<TModel, TValue> _templateBuilderDataContainer = new TemplateBuilderDataContainer<TModel, TValue>();

        public HtmlCollectionTemplate(HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            helper.Guard("HtmlHelper must not be null.");
            expression.Guard("Expression must not be null.");
            _templateBuilderDataContainer.Expression = expression;
            _templateBuilderDataContainer.HtmlHelper = helper;
        }

        public ITemplateHtmlSourceRenderer<TModel, TValue> TemplateSource(string template)
        {
            _templateBuilderDataContainer.PartialViewName = template;        
            return new TemplateHtmlSourceRenderer<TModel, TValue>(_templateBuilderDataContainer);
        }
    }
}
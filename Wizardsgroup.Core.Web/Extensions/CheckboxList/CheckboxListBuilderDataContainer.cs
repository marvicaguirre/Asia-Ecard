using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class CheckboxListBuilderDataContainer<TModel, TValue> where TValue : IEnumerable<IMultiSelectLookupValueField>
    {
        public HtmlHelper<TModel> HtmlHelper { get; set; }
        public Expression<Func<TModel, TValue>> Expression { get; set; }
        public IEnumerable<IMultiSelectLookupValueField> LookupValueFields { get; set; }
        public Mode Mode { get; set; }
        public bool IsSelectedByDefault { get; set; }
    }
}
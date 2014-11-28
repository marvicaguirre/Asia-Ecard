using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class MultiselectBuilderDataContainer<TModel, TValue>
    {
        public Expression<Func<TModel, TValue>> Expression { get; set; }
        public HtmlHelper<TModel> HtmlHelper { get; set; }
        public IEnumerable<IMultiSelectLookupValueField> LookupValueFields { get; set; }
        public string Name
        {
            get
            {
                if (Expression == null) return string.Empty;
                var memberExpression = (MemberExpression)Expression.Body;
                return memberExpression.Member.Name;
            }
        }
        public MultiselectSettingContainer FieldSetting { get; set; }
    }
}
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ComboBoxConfigContainer<TModel, TValue>
    {
        public ComboBoxConfigContainer()
        {
            PlaceHolder = "Select A Record";
            Index = string.Empty;
        }
        public HtmlHelper<TModel> HtmlHelper { get; set; }
        public Expression<Func<TModel, TValue>> TargetExpression { get; set; }
        public Expression<Func<TModel, TValue>> CascadeFromExpression { get; set; }
        public object DataSource { get; set; }
        public Action<IClientEvent> ClientEvent { get; set; }
        public string PlaceHolder { get; set; }
        public object HtmlAttributes { get; set; }
        public string Index { get; set; }
        public object Specification { get; set; }
        public string Key { get { return string.Format("{0}_{1}_{2}", typeof(TModel).Name, typeof(TValue).Name, GetHashCode()); } }
    }
}
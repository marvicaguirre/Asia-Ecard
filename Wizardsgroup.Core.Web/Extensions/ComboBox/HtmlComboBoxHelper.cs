using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class HtmlComboBoxHelper<TModel, TValue> : IHtmlComboBoxHelper<TModel, TValue>
    {
        #region Member
        readonly ComboBoxConfigContainer<TModel, TValue> _configuration = new ComboBoxConfigContainer<TModel, TValue>(); 
        #endregion

        #region Contructor
        public HtmlComboBoxHelper(HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            helper.Guard("HtmlHelper must not be null.");
            expression.Guard("Expression must not be null.");
            _configuration.TargetExpression = expression;
            _configuration.HtmlHelper = helper;
        } 
        #endregion

        #region Datasource
        public IHtmlComboBoxHelper<TModel, TValue> CascadeFrom(Expression<Func<TModel, TValue>> expression)
        {
            expression.Guard("Expression must not be null.");
            _configuration.CascadeFromExpression = expression;
            return this;
        }

        //public IComboBoxConfigurator<TModel, TValue> DataSource(Action<ILookupDataSource> dataSource)
        //{
        //    _configuration.DataSource = dataSource;
        //    return new ComboBoxConfigurator<TModel, TValue>(_configuration);
        //}

        public IComboBoxConfigurator<TModel, TValue> DataSource(string entityLookup)
        {
            _configuration.DataSource = entityLookup;
            return new ComboBoxConfigurator<TModel, TValue>(_configuration);
        }

        public IComboBoxConfigurator<TModel, TValue> DataSource(Action<ICustomActionDataSource> dataSource)
        {
            _configuration.DataSource = dataSource;
            return new ComboBoxConfigurator<TModel, TValue>(_configuration);
        }

        //public IComboBoxConfigurator<TModel, TValue> DataSource(IEnumerable<ILookupValueField> lookupItems)
        //{
        //    _configuration.DataSource = lookupItems;
        //    return new ComboBoxConfigurator<TModel, TValue>(_configuration);
        //} 
        #endregion
    }
}
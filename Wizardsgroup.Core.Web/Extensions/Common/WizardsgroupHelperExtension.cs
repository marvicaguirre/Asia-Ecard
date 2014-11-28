using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Core.Web.Extensions
{
    public class WizardsgroupHelperExtension<TModel> : IWizardsgroupHelperExtension<TModel>        
    {
        private readonly HtmlHelper<TModel> _helper;

        public WizardsgroupHelperExtension(HtmlHelper<TModel> helper)
        {
            _helper = helper;
        }

        public IHtmlSubmitConfirmMessageHelper<TModel> ConfirmOnSubmitFor(string name)
        {
            return new HtmlSubmitConfirmMessageHelper<TModel>(_helper, name);
        }

        public IHtmlTabstripHelper TabstripFor(string name)
        {
            return new HtmlTabstripHelper(_helper,name);
        }

        public IHtmlAccordionHelper AccordionFor(string name)
        {
            return new HtmlAccordionHelper(_helper,name);
        }

        public IHtmlButtonHelper ButtonFor(string buttonText)
        {
            return new HtmlButtonHelper(_helper, buttonText);
        }

        public IHtmlComboBoxHelper<TModel, TValue> ComboBoxFor<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            return new HtmlComboBoxHelper<TModel, TValue>(_helper,expression);
        }

        public IHtmlCheckboxListHelper<TModel, TValue> CheckboxListFor<TValue>(Expression<Func<TModel, TValue>> expression, Mode mode) 
            where TValue : IEnumerable<IMultiSelectLookupValueField>
        {
            return new HtmlCheckboxListHelper<TModel, TValue>(_helper, expression, mode);
        }

        public ISingleFieldToMultiselectListHelper<TModel, TValue> SingleFieldToMultiselectFor<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            return new SingleFieldToMultiselectListHelper<TModel, TValue>(_helper, expression);
        }

        public IHtmlCollectionTemplate<TModel, TValue> CollectionTemplateFor<TValue>(Expression<Func<TModel, TValue>> expression) where TValue : IEnumerable<object>
        {
            return new HtmlCollectionTemplate<TModel, TValue>(_helper,expression);
        }

        public IHtmlCollectionModelIndexer<TModel> BeginFormCollectionItemFor(Action<ITemplateIndexRegistrator> registerCollection)
        {
            return new HtmlCollectionModelIndexer<TModel>(_helper, registerCollection);
        }
    }
}
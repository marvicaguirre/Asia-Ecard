using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IWizardsgroupHelperExtension<TModel>
    {
        IHtmlSubmitConfirmMessageHelper<TModel> ConfirmOnSubmitFor(string name);
        IHtmlTabstripHelper TabstripFor(string name);

        IHtmlAccordionHelper AccordionFor(string name);

        IHtmlButtonHelper ButtonFor(string buttonText);

        IHtmlComboBoxHelper<TModel, TValue> ComboBoxFor<TValue>(Expression<Func<TModel, TValue>> expression);

        IHtmlCheckboxListHelper<TModel, TValue> CheckboxListFor<TValue>(Expression<Func<TModel, TValue>> expression,Mode mode)            
            where TValue : IEnumerable<IMultiSelectLookupValueField>;

        ISingleFieldToMultiselectListHelper<TModel, TValue> SingleFieldToMultiselectFor<TValue>(Expression<Func<TModel, TValue>> expression);

        IHtmlCollectionTemplate<TModel, TValue> CollectionTemplateFor<TValue>(Expression<Func<TModel, TValue>> expression)
            where TValue : IEnumerable<object>;

        IHtmlCollectionModelIndexer<TModel> BeginFormCollectionItemFor(Action<ITemplateIndexRegistrator> regCollection);
    }
}
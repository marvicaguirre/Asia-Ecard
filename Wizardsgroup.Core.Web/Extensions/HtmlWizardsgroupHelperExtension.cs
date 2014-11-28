using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlWizardsgroupHelperExtension
    {
        public static IWizardsgroupHelperExtension<TModel> Wizardsgroup<TModel>(this HtmlHelper<TModel> helper)
        {            
            return new WizardsgroupHelperExtension<TModel>(helper);
        }
    }
}
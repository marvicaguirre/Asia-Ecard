using System.ComponentModel;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Infrastructure
{
    public class StringModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return base.BindModel(controllerContext, bindingContext);
        }

        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor)
        {
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }

    //public class PersonnelModelBinder : DefaultModelBinder
    //{
    //    public override object BindModel
    //    (ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        Personnel p = base.BindModel(controllerContext, bindingContext) as Personnel;
    //    }
    //} 
}
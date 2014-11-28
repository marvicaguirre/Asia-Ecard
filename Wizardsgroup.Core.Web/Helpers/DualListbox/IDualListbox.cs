using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Helpers
{
    public interface IDualListbox<T>
    {
        IDualListboxFluentActionHelper<T> DualListboxFluentBuilder();
        ActionResult RenderView(string partialViewName = "_DualListboxMover");
    }
}
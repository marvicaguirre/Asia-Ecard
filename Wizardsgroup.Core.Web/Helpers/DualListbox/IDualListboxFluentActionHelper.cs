using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Helpers
{
    public interface IDualListboxFluentActionHelper<T>
    {
        IDualListboxFluentActionHelper<T> AssignParentId(int parentId);
        IDualListboxFluentActionHelper<T> AssignFunctionForUnassignedRecord(Func<IEnumerable<T>> getUnassingedFunction);
        IDualListboxFluentActionHelper<T> AssignFunctionForAssignedRecord(Func<IEnumerable<T>> getAssingedFunction);
        IDualListboxFluentActionHelper<T> AssignTextField(Expression<Func<T, string>> expressionText);
        IDualListboxFluentActionHelper<T> AssignValueField(Expression<Func<T, int>> expressionValue);        
        IDualListboxFluentActionHelper<T> AssignSaveAction(Controller controller, Func<int,int[], ActionResult> saveAction);
        IDualListboxFluentActionHelper<T> AssignFilterUnassignedAction(Controller controller, Func<int?, int?, ActionResult> filterAction);
        IDualListboxFluentActionHelper<T> AssignFilterAssignedAction(Controller controller, Func<int?, int?, ActionResult> filterAction);
        IDualListboxFluentActionHelper<T> WindowTitle(string title = "Record assignment");
        IDualListboxFluentActionHelper<T> UnassignedLabel(string label = "Record(s) to be assigned");
        IDualListboxFluentActionHelper<T> AssignedLabel(string label = "Assigned record(s)");
        IDualListbox<T> BuildDualListBox();
    }
}
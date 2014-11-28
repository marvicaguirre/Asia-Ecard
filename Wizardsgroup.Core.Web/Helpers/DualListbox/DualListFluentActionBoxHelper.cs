using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Helpers
{    
    internal class DualListFluentActionBoxHelper<T> : IDualListboxFluentActionHelper<T>
    {
        #region Member
        private readonly DualListboxHelper<T> _dualListbox;
        private Func<IEnumerable<T>> _getUnassingedFunction;
        private Func<IEnumerable<T>> _getAssingedFunction;
        private Expression<Func<T, int>> _expressionValue;
        private Expression<Func<T, string>> _expressionText;
        private string _title;
        private string _unassignedLabel;
        private string _assignedLabel;        
        private string _assignedRecordSaveAction;
        private string _assignedFilterUnassignedRecordAction;
        private string _assignedFilterAssignedRecordAction;
        private int _parentId;

        #endregion

        #region Constructor
        public DualListFluentActionBoxHelper(DualListboxHelper<T> dualListbox)
        {
            _dualListbox = dualListbox;
            _title = "Record assignment";
            _unassignedLabel = "Record(s) to be assigned";
            _assignedLabel = "Assigned record(s)";

        } 
        #endregion

        #region Public Functions

        public IDualListboxFluentActionHelper<T> AssignParentId(int parentId)
        {
            _parentId = parentId;
            return this;
        }

        public IDualListboxFluentActionHelper<T> AssignFunctionForUnassignedRecord(Func<IEnumerable<T>> getUnassingedFunction)
        {
            _getUnassingedFunction = getUnassingedFunction;
            return this;
        }

        public IDualListboxFluentActionHelper<T> AssignFunctionForAssignedRecord(Func<IEnumerable<T>> getAssingedFunction)
        {
            _getAssingedFunction = getAssingedFunction;
            return this;
        }

        public IDualListboxFluentActionHelper<T> AssignTextField(Expression<Func<T, string>> expressionText)
        {
            _expressionText = expressionText;
            return this;
        }

        public IDualListboxFluentActionHelper<T> AssignValueField(Expression<Func<T, int>> expressionValue)
        {
            _expressionValue = expressionValue;
            return this;
        }

        private string BuildActionLink(Controller controller, string saveActionMethodName)
        {
            var actionName = saveActionMethodName;
            var arrayNamespace = controller.GetType().ToString().Split('.').ToList();
            var index = arrayNamespace.FindIndex(o => o.Equals("Areas"));
            var area = arrayNamespace[index + 1];
            var controllerName = arrayNamespace.Last().Replace("Controller", "");

            return string.Format("/{0}/{1}/{2}/{3}", CommonHelper.Instance.VirtualDirectory(), area, controllerName, actionName).Replace("//", "/");
        }

        public IDualListboxFluentActionHelper<T> AssignSaveAction(Controller controller, Func<int,int[], ActionResult> saveAction)
        {
            _assignedRecordSaveAction = BuildActionLink(controller, saveAction.Method.Name);

            return this;
        }
        
        public IDualListboxFluentActionHelper<T> AssignFilterUnassignedAction(Controller controller, Func<int?, int?, ActionResult> saveAction)
        {
            _assignedFilterUnassignedRecordAction = BuildActionLink(controller, saveAction.Method.Name);

            return this;
        }

        public IDualListboxFluentActionHelper<T> AssignFilterAssignedAction(Controller controller, Func<int?, int?, ActionResult> saveAction)
        {
            _assignedFilterAssignedRecordAction = BuildActionLink(controller, saveAction.Method.Name);

            return this;
        }

        public IDualListboxFluentActionHelper<T> WindowTitle(string title = "Record assignment")
        {
            _title = title;
            return this;
        }

        public IDualListboxFluentActionHelper<T> UnassignedLabel(string label = "Record(s) to be assigned")
        {
            _unassignedLabel = label;
            return this;
        }

        public IDualListboxFluentActionHelper<T> AssignedLabel(string label = "Assigned record(s)")
        {
            _assignedLabel = label;
            return this;
        }

        public IDualListbox<T> BuildDualListBox()
        {
            var assignMethod = _dualListbox.GetType().GetMethod("_Assign", BindingFlags.NonPublic | BindingFlags.Instance);
            assignMethod.Invoke(_dualListbox, new object[] { _getUnassingedFunction, _getAssingedFunction, _expressionValue,_expressionText, 
                _assignedRecordSaveAction, _assignedFilterUnassignedRecordAction, _assignedFilterAssignedRecordAction, _parentId,_title, _unassignedLabel, _assignedLabel });
            return _dualListbox;
        } 
        #endregion

    }
}
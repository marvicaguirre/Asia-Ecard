using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Helpers
{
    public class DualListboxHelper<T> : IDualListbox<T>
    {
        #region Member
        private Func<IEnumerable<T>> _getUnassingedFunction;
        private Func<IEnumerable<T>> _getAssingedFunction;
        private Expression<Func<T, int>> _expressionValue;
        private Expression<Func<T, string>> _expressionText;
        private int _parentId;
        private readonly IDualListboxFluentActionHelper<T> _dualListFluentActionBoxHelper;
        private string _title;
        private string _unassignedLabel;
        private string _assignedLabel;
        private string _assignedRecordAction;
        private string _assignedFilterUnassignedRecordAction;
        private string _assignedFilterAssignedRecordAction;

        #endregion

        #region Constructor
        public DualListboxHelper()
        {
            _dualListFluentActionBoxHelper = new DualListFluentActionBoxHelper<T>(this);
        } 
        #endregion

        #region Public Methods
        public IDualListboxFluentActionHelper<T> DualListboxFluentBuilder()
        {
            return _dualListFluentActionBoxHelper;
        }

        public ActionResult RenderView(string partialViewName = "_DualListboxMover")
        {                                   
            var partialViewResult = new PartialViewResult();
            var unassignedList = _InvokeFunctionForGetRecords(_getUnassingedFunction,_expressionValue,_expressionText);
            var assignedList = _InvokeFunctionForGetRecords(_getAssingedFunction, _expressionValue, _expressionText);            
            partialViewResult.ViewBag.UnassignedRecords = unassignedList;     
            partialViewResult.ViewBag.AssignedRecords = assignedList;
            partialViewResult.ViewBag.WindowTitle = _title;
            partialViewResult.ViewBag.LeftLabel = _unassignedLabel;
            partialViewResult.ViewBag.RightLabel = _assignedLabel;
            partialViewResult.ViewBag.Action = _assignedRecordAction;
            partialViewResult.ViewBag.FilteredUnassignedItemsAction = _assignedFilterUnassignedRecordAction;
            partialViewResult.ViewBag.FilteredAssignedItemsAction = _assignedFilterAssignedRecordAction;
            partialViewResult.ViewBag.SourceId = _parentId;
            partialViewResult.ViewName = partialViewName;
            return partialViewResult;
        }

        #endregion

        #region Private

        SelectList _InvokeFunctionForGetRecords(Func<IEnumerable<T>> getRecords,Expression<Func<T,int>> funcValue,Expression<Func<T,string>> functText )
        {           
            var valueCompiled = funcValue.Compile();
            var textCompiled = functText.Compile();
            var list = new SelectList(getRecords.Invoke()
                .Select(o => new { Value = valueCompiled(o).ToString(), Text = textCompiled(o) }),"Value","Text");
            return list;
        }

        void _Assign(Func<IEnumerable<T>> getUnassingedFunction,
            Func<IEnumerable<T>> getAssingedFunction,
            Expression<Func<T, int>> expressionValue,
            Expression<Func<T, string>> expressionText,
            string assignedRecordAction,      
            string assignedFilteredUnassignedRecordAction,
            string assignedFilteredAssignedRecordAction,
            int parentId,
            string title,string labelLeft,string labelRight)
        {
            getUnassingedFunction.Guard("getUnassingedFunction");

            getAssingedFunction.Guard("getAssingedFunction");

            expressionValue.Guard("expressionValue");

            expressionText.Guard("expressionText");    
        
            if (0 == parentId)
                throw new ArgumentNullException("parentId");

            _parentId = parentId;
            _getUnassingedFunction = getUnassingedFunction;
            _getAssingedFunction = getAssingedFunction;
            _expressionValue = expressionValue;
            _expressionText = expressionText;
            _assignedRecordAction = assignedRecordAction;
            _assignedFilterUnassignedRecordAction = assignedFilteredUnassignedRecordAction;
            _assignedFilterAssignedRecordAction = assignedFilteredAssignedRecordAction;
            _title = title;
            _unassignedLabel = labelLeft;
            _assignedLabel = labelRight;
        }
        #endregion
    }
}
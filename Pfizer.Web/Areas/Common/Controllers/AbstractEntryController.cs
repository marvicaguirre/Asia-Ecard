using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Pfizer.Repository;
using Pfizer.Service.Helper;
using Pfizer.Web.Code.Extensions;
using RuleEngine;
using Pfizer.Domain.Constants;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Core.Web;
using Wizardsgroup.Core.Web.Constants;
using Wizardsgroup.Core.Web.Helpers;
using Wizardsgroup.Core.Web.Infrastructure;
using Wizardsgroup.Core.Web.SessionManagement;
using Wizardsgroup.Domain.Base;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Domain.Enumerations;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Web.Areas.Common.Controllers
{
    [HandleError]
    [Authorize]
    public abstract class AbstractEntryController<TEntity, TViewModel> : AbstractCommonController
        where TEntity : AbstractBaseModel, new()
        where TViewModel : class, new()
    {
        #region Constants
        const string Createdby = "CreatedBy";
        const string Createddate = "CreatedDate";
        const string RecordStatus = "RecordStatus";
        #endregion

        private readonly string _parentIdSessionName = GetParentIdSessionName("Id");
        private readonly string _searchFilterSessionName = GetParentIdSessionName("SearchFilter");
        readonly SystemMessageHelper _setting = new SystemMessageHelper(new UnitOfWorkWrapper());

        public AbstractEntryController()
        {
            _parentIdSessionName = string.Format("{0}_{1}", _parentIdSessionName, GetType().Name);
        }

        protected int FakeParentId
        {
            get { return default(int); }
        }

        public string EntitySearchFilter
        {
            get { return Session[_searchFilterSessionName] != null ? Session[_searchFilterSessionName].ToString() : string.Empty; }
            set { Session[_searchFilterSessionName] = value ?? string.Empty; }
        }

        public int? ParentEntityId
        {
            get { return Session[_parentIdSessionName] != null ? Session[_parentIdSessionName].ToInteger() :  0; }
            set { Session[_parentIdSessionName] = value ?? 0; }
        }

        internal int? GetSpecificParentEntityId(string parentEntityIdName, string controllerNameInContext)
        {
            var sessionKeyName = string.Format("{0}_{1}", parentEntityIdName, controllerNameInContext);
            var value = Session[sessionKeyName];

            return value != null ? (int?)value.ToInteger() : null;
        }

        internal void SetSpecificParentEntityIdValue(string parentEntityIdName, string controllerName, Guid? value)
        {
            var sessionKeyName = string.Format("{0}_{1}", parentEntityIdName, controllerName);

            Session[sessionKeyName] = value;
        }

        internal static string GetParentIdSessionName(string suffixName = "")
        {
            return string.Format("{0}{1}", typeof(TEntity).Name, suffixName);
        }

        [HttpPost]
        public ActionResult SetParentEntityId(int? id)
        {
            ParentEntityId = id != null ? id.Value : 0;
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SetEntitySearchFilter(string filterText)
        {
            EntitySearchFilter = filterText ?? string.Empty;
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        #region Index

        public virtual ActionResult Index(int? id)
        {
            ViewBagJQueryLoaded = id != null;
            ParentEntityId = id;
            // Clear the entity search filter.
            EntitySearchFilter = string.Empty;

            //1. set the initial data first
            SetViewBagForIndexView(id);

            //2. Assign the data to the ViewBags; GetTitle might be dependent on SetViewBagForIndexView;            
            ViewBagParentId = id;
            //ParentId = id;
            ViewBagTitle = GetIndexViewTitle();

            bool isPartialView = IsIndexPartialView();
            if (!isPartialView)
            {
                return View();
            }
            ViewBagTitle = !string.IsNullOrWhiteSpace(ViewBagTitle) ? ViewBagTitle : id.ToString();
            return PartialView();
        }

        public ActionResult GetRecords([DataSourceRequest]DataSourceRequest request, string gridName)
        {
            var resultToBind = GetModelRecordsToBindInGrid2() ?? GetModelRecordsToBindInGrid().AsQueryable();
            //request.Page = ResizePagingInGrid(request.PageSize,request.Page, resultToBind.Count());
            //filter first before converting to dynamic object
            var dataSourceResult = resultToBind.ToDataSourceResult(request);
            var viewModelToDynamicObject = dataSourceResult.Data
                .AsQueryable().AsParallel().AsOrdered()
                .Cast<TViewModel>().Select(o => o.ToDynamicFromGridSetting(gridName));
            dataSourceResult.Data = viewModelToDynamicObject;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoJsonConverter() });
            var serializeJson = serializer.Serialize(dataSourceResult);
            return new CustomJsonActionResult(serializeJson);
        }

        public ActionResult GetRecordsForListView([DataSourceRequest]DataSourceRequest request)
        {
            var resultToBind = GetModelRecordsToBindInGrid();
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoJsonConverter() });
            var serializeJson = serializer.Serialize(resultToBind);
            return new CustomJsonActionResult(serializeJson);
        }

        private int ResizePagingInGrid(int pageSize, int pageRequestLocation, int recordCount)
        {
            decimal divResult = ((decimal)recordCount / (decimal)pageSize);
            var computedPage = (int)Math.Ceiling(divResult);
            var isComputedPagingLessThanRequestPage = computedPage < pageRequestLocation;
            var page = isComputedPagingLessThanRequestPage ? computedPage : pageRequestLocation;
            return page;
        }

        #endregion

        #region Create

        public virtual ActionResult Create(int? id = null)
        {
            ViewBagParentId = id;
            SetViewBagsForCreate(id);
            var viewModel = new TViewModel();
            object model = SetViewModelData(viewModel);
            if (model != null)
            {
                return PartialView(model);
            }
            return PartialView();
        }

        protected virtual object GetModel()
        {
            return null;
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TViewModel viewModel)
        {
            TEntity entity = AssignViewModelToEntity(viewModel);
            var helper = new ActionResultHelper<TEntity>();
            helper.Method += Add;

            var result = Validate(entity, _CleanUpControllerName(), "Create");
            if (!result.Passed)
                _CreateReturnPartialViewOnError(viewModel);

            //var actionResultMessage = helper.Process(entity, ModelState, CrudTransactionResultConstant.Create);
            var actionResultMessage = helper.Process(entity, ModelState, _setting.GetMessage(SystemMessageConstant.RecordAdded));

            return actionResultMessage.ActionStatus == ActionStatusResult.Failed
                       ? _CreateReturnPartialViewOnError(viewModel)
                       : Json(actionResultMessage, JsonRequestBehavior.AllowGet);
        }

        protected void Add(TEntity entity)
        {
            var service = GetService();
            if (service == null) return;

            var audit = entity as AbstractBaseModel;
            if (audit != null)
            {
                audit.CreatedBy = SessionManager.GetUserName();
                audit.CreatedDate = DateTime.Now;
            }
            service.Insert(entity);
            service.Save();
        }

        private ActionResult _CreateReturnPartialViewOnError(TViewModel viewModel)
        {
            SetViewBagsForCreate(ViewBagParentId.ToInteger());
            ViewBag.ParentId = ViewBagParentId;
            SetViewModelData(viewModel);
            return PartialView(viewModel);
        }

        #endregion

        #region Edit

        public virtual ActionResult Edit(int id)
        {
            ViewBagId = id;
            SetViewBagsForEdit(id);
            return LoadEditView(id, true);
        }

        protected ActionResult LoadEditView(int id, bool isPartial)
        {
            var entity = GetRecord(id);
            if (entity == null)
                return isPartial ? (ActionResult)PartialView() : View();

            _AssignTrailInCreateForEditAction(entity);
            TViewModel viewModel = SetViewModelData(AssignEntityToViewModel(entity));
            return isPartial ? (ActionResult)PartialView(viewModel) : View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TViewModel viewModel)
        {
            TEntity entity = AssignViewModelToEntity(viewModel);
            var actionExceptionHelper = new ActionResultHelper<TEntity>();
            actionExceptionHelper.Method += Update;

            var result = Validate(entity, _CleanUpControllerName(), "Edit");
            if (!result.Passed)
                _EditReturnPartialViewOnError(viewModel);

            //var actionResultMessage = actionExceptionHelper.Process(entity, ModelState, CrudTransactionResultConstant.Update);
            var actionResultMessage = actionExceptionHelper.Process(entity, ModelState, _setting.GetMessage(SystemMessageConstant.RecordUpdated));

            return actionResultMessage.ActionStatus == ActionStatusResult.Failed
               ? _EditReturnPartialViewOnError(viewModel)
               : Json(actionResultMessage, JsonRequestBehavior.AllowGet);
        }

        protected TEntity GetRecord(int id)
        {
            return GetService().Find(id);
        }

        internal void Update(TEntity entity)
        {
            var service = GetService();
            if (service == null) return;

            var audit = entity as AbstractBaseModel;
            if (audit != null)
            {
                _SetCreateAuditFieldValuesForUpdate(audit);
                audit.ModifiedBy = SessionContainer.UserName;
                audit.ModifiedDate = DateTime.Now;
            }
            service.Update(entity);
            service.Save();
        }

        protected void _AssignTrailInCreateForEditAction(TEntity entity)
        {
            var audit = entity as AbstractBaseModel;
            if (audit == null) return;
            SessionContainer.KeyCollection.TryAddKey(this, Createdby, audit.CreatedBy);
            SessionContainer.KeyCollection.TryAddKey(this, Createddate, audit.CreatedDate);
            SessionContainer.KeyCollection.TryAddKey(this, RecordStatus, audit.RecordStatus);
        }

        private void _SetCreateAuditFieldValuesForUpdate(AbstractBaseModel audit)
        {
            object createdBy;
            SessionContainer.KeyCollection.TryGetKey(this, Createdby, out createdBy);
            audit.CreatedBy = createdBy.ToString();

            object createdDate;
            SessionContainer.KeyCollection.TryGetKey(this, Createddate, out createdDate);
            audit.CreatedDate = Convert.ToDateTime(createdDate);

            object recordStatus;
            SessionContainer.KeyCollection.TryGetKey(this, RecordStatus, out recordStatus);
            audit.RecordStatus = recordStatus is RecordStatus
                                     ? (RecordStatus)recordStatus
                                     : Wizardsgroup.Domain.Enumerations.RecordStatus.Active;
        }

        private ActionResult _EditReturnPartialViewOnError(TViewModel viewModel)
        {
            SetViewBagsForEdit(ViewBagId.ToInteger());
            return PartialView(viewModel);
        }

        #endregion

        #region Delete

        public ActionResult ConfirmItems(int[] checkedRecords)
        {
            var displayData = GetSelectedItems(checkedRecords);
            return Json(displayData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult DeleteItems(int[] checkedRecords)
        {
            try
            {
                _DeleteMultiple(checkedRecords);

                //return Json(CrudTransactionResultConstant.Delete, JsonRequestBehavior.AllowGet);
                var actionResultMessage = new ActionResultMessage { ActionStatus = ActionStatusResult.Success };
                actionResultMessage.Messages.Add(_setting.GetMessage(SystemMessageConstant.Delete));
                return Json(actionResultMessage, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException ex)
            {                
                Logger.Log(SqlErrorHelper.SqlErrorMessage(ex));
                //string errormessage = string.Format(CrudTransactionResultConstant.NoRecordDeleted);
                string errormessage = string.Format(_setting.GetMessage(SystemMessageConstant.NoRecordDeleted));

                return Json(errormessage, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                string errormessage = SqlErrorHelper.SqlErrorMessage(ex);

                return Json(errormessage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        protected virtual void _DeleteMultiple(int[] checkedRecords)
        {
            var service = GetService();
            checkedRecords.ToList().ForEach(id => service.Delete(id));
            service.Save();
        }

        #endregion

        #region Toggle Status
        [HttpPost]
        public ActionResult ToggleStatus(int[] checkedRecords)
        {
            try
            {
                var service = GetService();
                service.ToogleStatus(checkedRecords);
                service.Save();
                //return Json(CrudTransactionResultConstant.Toggle, JsonRequestBehavior.AllowGet);
                var actionResultMessage = new ActionResultMessage { ActionStatus = ActionStatusResult.Success };
                actionResultMessage.Messages.Add(_setting.GetMessage(SystemMessageConstant.Toggle));
                return Json(actionResultMessage, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException ex)
            {
                string errormessage = SqlErrorHelper.SqlErrorMessage(ex);
                return Json(errormessage, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                string errormessage = SqlErrorHelper.SqlErrorMessage(ex);
                return Json(errormessage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Data Validation

        protected ValidationResult Validate(TEntity model, string contoller, string action)
        {
            var validatorHelper = new ValidationHelper<TEntity>(new UnitOfWorkWrapper());
            var rules = validatorHelper.CompileRuleForControllerAndAction(contoller, action);
            var result = validatorHelper.Validate(model, rules);

            if (!result.Passed)
            {
                var failedResult = result.ValidationDetails.Where(o => !o.Passed);
                foreach (var error in failedResult)
                {
                    ModelState.AddModelError(string.Empty, error.ValidationMessage);
                }
            }

            if (result.Passed)
            {
                result.Passed = ModelState.IsValid;
            }

            return result;
        }

        #endregion Data Validation

        #region Abstract Functions

        protected abstract IEnumerable<object> GetSelectedItems(int[] checkedRecords);

        protected abstract IEntityService<TEntity> GetService();

        protected abstract IEnumerable<TViewModel> GetModelRecordsToBindInGrid();

        protected virtual IQueryable<TViewModel> GetModelRecordsToBindInGrid2()
        {
            return null;
        }

        //Make this abstract
        protected virtual IPagingQueryResult<TViewModel> GetModelRecordsToBindInGrid(int take, int skip, List<CustomParameter> args)
        {
            return new PagingQueryResult<TViewModel>();
        }

        protected abstract TEntity AssignViewModelToEntity(TViewModel viewModel);

        protected abstract TViewModel AssignEntityToViewModel(TEntity entity);

        protected abstract string GetIndexViewTitle();

        protected abstract bool IsIndexPartialView();

        protected abstract void SetViewBagForIndexView(int? id);

        protected abstract void SetViewBagsForCreate(int? id);

        protected abstract void SetViewBagsForEdit(int? id);

        #endregion Abstract Functions

        #region PrivateFunctions
        internal string _CleanUpControllerName()
        {
            var listOfStrings = ToString().Split('.');
            return listOfStrings.Last().ToLower().Replace("controller", "");
        }
        #endregion

        #region Virtual Functions
        protected virtual TViewModel SetViewModelData(TViewModel viewModel)
        {
            return viewModel;
        }

        protected virtual IPagingQueryResult<TViewModel> ConvertPagingQueryResultToViewModel(IPagingQueryResult<TEntity> entityPagingResult)
        {
            var pagingViewModelResult = new PagingQueryResult<TViewModel>
            {
                Result = entityPagingResult.Result.Select(AssignEntityToViewModel),
                TotalRecord = entityPagingResult.TotalRecord
            };
            return pagingViewModelResult;
        }

        #endregion
    }
}
using System;
using System.Web.Mvc;
using Wizardsgroup.Core.Web.SessionManagement;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Web.Areas.Common.Controllers
{    
    public class AbstractCommonController : Controller
    {
        private const string ParentId = "ParentId";
        private const string Title = "Title";
        private const string PrimaryId = "PrimaryId";
        private const string ReadOnlyView = "ReadOnlyView";
        private const string ReadOnlyViewOverrideWithThis = "ReadOnlyViewOverrideWithThis";
        private const string ReadOnlyBypassArea = "ReadOnlyBypassArea";

        public AbstractCommonController()
        {
            UserTracker.Instance.SetSessionDelegateGetter(() => SessionManager.GetUserSessionContainer().SessionId);
        }
        protected ISessionContainer SessionContainer
        {
            get
            {
                SessionManager.CheckSession();
                var userSessionContainer = SessionManager.GetUserSessionContainer();
                //Session_End(object sender, EventArgs e) in Global.asax does not work with Http Request
                if (userSessionContainer == null)
                    Response.RedirectToRoute("DefaultArea", new { controller = "Account", action = "RedirectToLogin" });

                return userSessionContainer;
            }
        }

        #region Common ViewBag Data

        /// <summary>
        /// Gets or sets the view bag id.
        /// </summary>
        /// <value>
        /// The view bag id.
        /// </value>
        protected int ViewBagId
        {
            get
            {
                object primaryId;
                SessionContainer.KeyCollection.TryGetKey(this, ParentId, out primaryId);
                return ViewBag.ID ?? primaryId;
            }
            set
            {
                if (SessionContainer != null) SessionContainer.KeyCollection.TryAddKey(this, ParentId, value);
                ViewBag.ID = value;
            }
        }


        protected bool ViewBagJQueryLoaded
        {
            get
            {
                var casted = ViewBag.JQueryLoaded is bool && (bool) ViewBag.JQueryLoaded;
                return casted;
            }
            set
            {
                ViewBag.JQueryLoaded = value;
            }
        }
        /// <summary>
        /// Gets or sets the view bag parent.
        /// </summary>
        /// <value>
        /// The view bag parent.
        /// </value>
        protected object ViewBagParentId
        {
            get
            {
                object parentId;
                SessionContainer.KeyCollection.TryGetKey(this, PrimaryId, out parentId);
                return ViewBag.ParentId ?? parentId;
            }
            set
            {
                if (SessionContainer != null) SessionContainer.KeyCollection.TryAddKey(this, PrimaryId, value);
                ViewBag.ParentId = value;
            }
        }

        /// <summary>
        /// Gets or sets the view bag title.
        /// </summary>
        /// <value>
        /// The view bag title.
        /// </value>
        protected string ViewBagTitle
        {
            get
            {
                object title;
                SessionContainer.KeyCollection.TryGetKey(this, Title, out title);
                return ViewBag.Title ?? title;
            }
            set
            {
                SessionContainer.KeyCollection.TryAddKey(this, Title, value);
                ViewBag.Title = value;
            }
        }

        #region ReadOnlyView implementation
        /// <summary>
        /// This will affect how the page is displayed (editable or not) depending on implementation. How ever
        /// take note that the default behavior:
        ///     #1. If the current controller is a member of current active tabstip (module set) then the system will
        ///         consider this value
        ///     #2. If the current controller is not a member of the current active tabstrip (module set) then the
        ///         system will set this to TRUE (read only view) regardless of implementation above
        ///     #3. If the over ride (ViewBagReadOnlyViewOverride) is set to TRUE then the system will display
        ///         the page for viewing only regardless of previous two items above
        /// </summary>
        public Boolean? ViewBagReadOnlyView
        {
            get
            {
                object readonlyview;
                SessionContainer.KeyCollection.TryGetKey(this, ReadOnlyView, out readonlyview);
                return ViewBag.ReadOnlyView ?? readonlyview;
            }
            set
            {
                SessionContainer.KeyCollection.TryAddKey(this, ReadOnlyView, value);
                ViewBag.ReadOnlyView = value;
            }
        }

        /// <summary>
        /// If this is set to TRUE then the page will be displayed for viewing only (read only)
        /// If this is set to FALSE then the page will be displayed as unchanged (as-is)
        /// If this is set to NULL (default) then then the page displaying mode will depend on the 
        ///     default implementation of ReadOnlyView logic:
        ///         * If controller is not a member of current module set then page is ReadOnly
        ///         * Otherwise, the value of "ReadOnlyView" will be used
        /// </summary>
        public Boolean? ViewBagReadOnlyViewOverrideWithThis
        {
            get
            {
                object viewbagreadonlyviewoverridewiththis;
                SessionContainer.KeyCollection.TryGetKey(this, ReadOnlyViewOverrideWithThis, out viewbagreadonlyviewoverridewiththis);
                return ViewBag.ViewBagReadOnlyViewOverrideWithThis ?? viewbagreadonlyviewoverridewiththis;
            }
            set
            {
                SessionContainer.KeyCollection.TryAddKey(this, ReadOnlyViewOverrideWithThis, value);
                ViewBag.ViewBagReadOnlyViewOverrideWithThis = value;
            }
        }

        public Boolean? ViewBagReadOnlyBypassArea
        {
            get
            {
                object viewbagreadonlybypassarea;
                SessionContainer.KeyCollection.TryGetKey(this, ReadOnlyBypassArea, out viewbagreadonlybypassarea);
                return ViewBag.ViewBagReadOnlyBypassArea ?? viewbagreadonlybypassarea;
            }
            set
            {
                SessionContainer.KeyCollection.TryAddKey(this, ReadOnlyBypassArea, value);
                ViewBag.ViewBagReadOnlyByPassArea = value;
            }
        }

        /// <summary>
        /// This is the equivalent active module based on the active tab-strip in the UI. The value of this property will
        /// be used by ReadOnlyView process (for determining if the current controller is a member of current active tabstrip
        /// module). Usually this is being set by ajax call (SetViewBagActiveModule) in UI which is triggered if the user 
        /// change the active tab 
        /// 
        /// Note: this is not using the common SessionContainer.KeyCollection because we need the value to persist
        ///     independent with current controller.
        /// </summary>
        public String ViewBagActiveModule
        {
            get
            {
                object viewbagactivemodule = Session["ActiveModule"] ?? "";
                return ViewBag.ActiveModule ?? viewbagactivemodule;
            }
            set
            {
                Session["ActiveModule"] = value;
                ViewBag.ActiveModule = value;
            }
        }

        /// <summary>
        /// This will simple save the caption (tab text) of the active tabstrip (ajax call on tab strip click event handler)
        /// </summary>
        /// <param name="tabText">The tabText of tabstrip</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public void SetViewBagActiveModule(String tabText)
        {
            ViewBagActiveModule = tabText;
        }

        #endregion

        #endregion Common ViewBag Data


        protected override void OnException(ExceptionContext filterContext)
        {
            Logger.Log(filterContext.Exception.ToString());

            if (filterContext.HttpContext.IsCustomErrorEnabled)
            {
                filterContext.ExceptionHandled = true;
                this.View("_Error").ExecuteResult(this.ControllerContext);
            }
        }

 
    }
}

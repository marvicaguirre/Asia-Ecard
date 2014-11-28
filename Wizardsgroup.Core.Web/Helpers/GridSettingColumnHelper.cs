using System;
using System.Globalization;
using Wizardsgroup.Core.Web.Models;

namespace Wizardsgroup.Core.Web.Helpers
{
    public class GridSettingColumnHelper
    {
        public static ColumnFormat FormatColumn(GridSettingViewModel type, string primaryKey, Func<string, string, string, bool> hasAccess)
        {
            var width = "100";
            if (type.GridColumnWidth.HasValue)
            {
                width = type.GridColumnWidth.Value.ToString(CultureInfo.InvariantCulture) + "px";
            }

            var columnFormat = new ColumnFormat
            {
                field = type.DataSourceEntityColumnName,
                title = type.GridHeaderTitle,
                filterable = type.GridColumnFilterable,
                sortable = type.GridColumnSortable,
                locked = GetLocked(type),
                lockable = GetLockable(type),
                width = width,
                groupable = type.GridColumnGroupable,
                menu = type.GridColumnMenu,
            };

            switch (type.GridCellType)
            {
                case "ActionLink":
                    columnFormat.template = CreateActionLink(type, primaryKey, hasAccess);
                    break;
                case "LinkModal":
                    columnFormat.template = CreateLinkModal(type, primaryKey, hasAccess);
                    break;
                case "CheckBox":
                    columnFormat.template = CreateGridCheckBox(type);
                    break;
                case "LinkDetails":
                    columnFormat.template = CreateCustomLinkDetails(type, primaryKey);
                    break;
                case "LinkPlane":
                    columnFormat.template = CreateCustomLinkPlane(type, primaryKey);
                    break;
                case "Date":
                    columnFormat.template = ParseDate(type);
                    break;
                case "DateGroupable":
                    columnFormat.template = ParseDateGroupable(type);
                    break;
                case "LinkDateModal":
                    columnFormat.template = ParseLinkDateModal(type, primaryKey);
                    break;
                case "LinkNewTab":
                    columnFormat.template = CreateLinkNewTab(type, primaryKey);
                    break;
            }
            return columnFormat;
        }

        #region --- Grid Column ---
        private static string CreateLinkModal(GridSettingViewModel type, string primaryKeyName, Func<string, string, string, bool> hasAccess)
        {
            InitializeLink(type);
            var controllerAction = GetControllerAction(type, hasAccess);
            string windowTitle = GetWindowTitle(controllerAction, type.ModalWindowTitle);
            return string.Format("<a  href=\"{0}/#={1}#\" primaryId=\"#={1}#\" class=\"linkModalClass\" modaltitle=\"{2}\"  modalwidth={3} modalheight={4} gridname=\"{5}\" actionname=\"{7}\">{6}</a>", _url, primaryKeyName, windowTitle, _modalWidth, _modalHeight, type.GridName, _displayText, controllerAction);
        }

        private static string CreateActionLink(GridSettingViewModel type, string primaryKeyName, Func<string, string, string, bool> hasAccess)
        {
            InitializeLink(type);
            var controllerAction = GetControllerAction(type, hasAccess);
            var template = string.Format(
                "<a href='javascript:void(0)' url=\"{0}\" primaryId=\"#={1}#\" class=\"linkActionClass\" gridname=\"{2}\" actionname=\"{4}\">{3}</a>",
                _url, primaryKeyName, type.GridName, _displayText, controllerAction);

            return AttachedAlignment(type.GridColumnAlignment, template);
        }

        /// <summary>
        /// Replaces 'Edit' action with 'View' action if user has no access to the function
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="hasAccess">The has access.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns></returns>
        private static string GetControllerAction(GridSettingViewModel type, Func<string, string, string, bool> hasAccess)
        {
            //Note: type.ControllerName = not actually the moduleName; just a placeholder!
            var moduleName = type.ControllerName;
            var functionName = string.Format("{0}{1}", type.ControllerAction, type.ControllerName);
            var controllerAction = type.ControllerAction;
            if (type.ControllerAction.ToLower() == "edit")
            {
                var hasEditRights = hasAccess(moduleName, functionName, SessionManagement.SessionManager.GetUserId().ToString());
                controllerAction = hasEditRights ? controllerAction : "View";
            }
            return controllerAction;
        }

        /// <summary>
        /// Replaces 'Edit' in the title with 'View'
        /// </summary>
        /// <param name="controllerAction"></param>
        /// <param name="modalWindowTitle"></param>
        /// <returns></returns>
        private static string GetWindowTitle(string controllerAction, string modalWindowTitle)
        {
            if (controllerAction.ToLower() == "edit" || controllerAction.ToLower() == "wizardedit")
                modalWindowTitle.Replace("Edit", "View");

            return modalWindowTitle;
        }

        private static string CreateCustomLinkDetails(GridSettingViewModel type, string primaryKeyName)
        {
            InitializeLink(type);
            return string.Format("<a href='javascript:void(0)' url=\"{0}\" primaryId=\"#={2}#\" id=\"lnkView\" targetLevel=\"{3}\" gridname=\"{4}\" class=\"linkDetailsClass\">{1}</a>", _url, _displayText, primaryKeyName, type.LinkDetailLevel, type.GridName);
        }

        private static string CreateGridCheckBox(GridSettingViewModel type)
        {
            return string.Format("<input name=\"checkedRecords\" type=\"checkbox\" primaryId=\"#={0}#\" value=\"{0}\" title=\"checkedRecords\" />", type.DataSourceEntityColumnName);
        }

        private static string CreateCustomLinkPlane(GridSettingViewModel type, string primaryKeyName)
        {
            InitializeLink(type);
            return string.Format("<a href='javascript:void(0)' url=\"{0}\" primaryId=\"#={2}#\" id=\"lnkView\" targetLevel=\"{3}\" class=\"linkPlaneClass\">{1}</a>", _url, _displayText, primaryKeyName, type.LinkDetailLevel);
        }

        private static string CreateLinkNewTab(GridSettingViewModel type, string primaryKeyName)
        {
            InitializeLink(type);
            var htmlString = string.Format("<a  href='javascript:void(0);' tabText='{0}' moduleId='{0}' primaryId=\"#={1}#\" class=\"linkNewTabClass\" url=\"{2}\">{0}</a>", type.GridHeaderTitle, primaryKeyName, _url);
            return htmlString;
        }

        #endregion

        #region ---Date Column---
        private static string ParseDate(GridSettingViewModel type)
        {
            string gridCellDisplayFormat = type.GridCellDisplayFormat ?? GlobalVariable.DateFormat;
            string formatedDate = string.Format("#=kendo.toString(new Date(parseInt({0}.substr(6))),\"{1}\") #", type.DataSourceEntityColumnName, gridCellDisplayFormat);
            return string.Format("#if ({0}) {{#{1}#}}  #", type.DataSourceEntityColumnName, formatedDate);
        }

        //Note: used for the DateGroupable GridCellType; 
        //function onCustomKendoGridSchema(response) should be overridden from Abojeb-module-layout.js in the specific cshtml file where CustomGrid is being used;
        private static string ParseDateGroupable(GridSettingViewModel type)
        {
            string gridCellDisplayFormat = type.GridCellDisplayFormat ?? GlobalVariable.DateFormat;
            string formatedDate = string.Format("#=kendo.toString({0},\"{1}\") #", type.DataSourceEntityColumnName, gridCellDisplayFormat);
            return string.Format("#if ({0}) {{#{1}#}}  #", type.DataSourceEntityColumnName, formatedDate);
        }

        private static string ParseLinkDateModal(GridSettingViewModel type, string primaryKeyName)
        {
            InitializeLink(type);
            string gridCellDisplayFormat = type.GridCellDisplayFormat ?? GlobalVariable.DateFormat;
            _displayText = string.Format("#=kendo.toString(new Date(parseInt({0}.substr(6))),\"{1}\") #", type.DataSourceEntityColumnName, gridCellDisplayFormat);
            string formatedDate = string.Format("<a  href=\"{0}\" primaryId=\"#={1}#\" class=\"linkModalClass\" modaltitle=\"{2}\"  modalwidth={3} modalheight={4} gridname=\"{5}\" actionname=\"{7}\">{6}</a>", _url, primaryKeyName, type.ModalWindowTitle, _modalWidth, _modalHeight, type.GridName, _displayText, type.ControllerAction);
            type.GridCellDisplayText = string.IsNullOrEmpty(type.GridCellDisplayText) ? string.Empty : string.Format("<a  href=\"{0}\" primaryId=\"#={1}#\" class=\"linkModalClass\" modaltitle=\"{2}\"  modalwidth={3} modalheight={4} gridname=\"{5}\" actionname=\"{7}\">{6}</a>", _url, primaryKeyName, type.ModalWindowTitle, _modalWidth, _modalHeight, type.GridName, type.GridCellDisplayText, type.ControllerAction);
            return string.Format("#if ({0}) {{#{1}#}} else {{#{2}#}}  #", type.DataSourceEntityColumnName, formatedDate, type.GridCellDisplayText);
        }
        #endregion

        #region --- Grid Column Header ---
        //static string CreateGridHeaderCheckBox(GridSetting type)
        //{
        //    return string.Format("<input name=\"checkedRecords\" type=\"checkbox\" class = 'selectAll' />");
        //}
        #endregion


        private static string AttachedAlignment(string alignment, string template)
        {
            return string.Format("<div style=\"text-align:{0}\">{1}</div>", alignment, template);
        }

        private static void InitializeLink(GridSettingViewModel type)
        {
            string area = "Common";
            if (type.Area != null)
            {
                area = type.Area;
            }
            var vDir = CommonHelper.Instance.VirtualDirectory();
            string virtualDir = "";
            if (!string.IsNullOrEmpty(vDir) && vDir.Trim() != "/")
            {
                virtualDir = vDir;
            }

            _url = string.Format("{0}/{1}/{2}/{3}", virtualDir, area.Trim(), type.ControllerName.Trim(), type.ControllerAction.Trim());

            if (type.ModalWindowWidth.HasValue)
                _modalWidth = string.IsNullOrEmpty(type.ModalWindowWidth.Value.ToString(CultureInfo.InvariantCulture))
                                  ? 0
                                  : Convert.ToInt32(
                                      type.ModalWindowWidth.Value.ToString(CultureInfo.InvariantCulture)
                                          .Replace("px", ""));
            else
                _modalWidth = 0;

            if (type.ModalWindowHeight.HasValue)
                _modalHeight = string.IsNullOrEmpty(type.ModalWindowHeight.Value.ToString(CultureInfo.InvariantCulture))
                                   ? 0
                                   : Convert.ToInt32(
                                       type.ModalWindowHeight.Value.ToString(CultureInfo.InvariantCulture)
                                           .Replace("px", ""));
            else
                _modalHeight = 0;

            _displayText = string.IsNullOrEmpty(type.GridCellDisplayText) ? string.Format("#={0}#", type.DataSourceEntityColumnName) : type.GridCellDisplayText;
        }

        private static bool? GetLocked(GridSettingViewModel type)
        {
            var returnValue = type.GridColumnLocked.HasValue && type.GridColumnLocked.Value;
            if (returnValue) return true;
            return null;
        }

        private static bool? GetLockable(GridSettingViewModel type)
        {
            var returnValue = type.GridColumnLockable.HasValue && type.GridColumnLockable.Value;
            return returnValue;
        }

        private static int _modalWidth;
        private static int _modalHeight;
        private static string _displayText;
        private static string _url;

    }

    public static class GlobalVariable
    {
        //temporary only this needs to be set on SysParam and store in session
        public static string DateFormat { get { return "dd/MM/yyyy"; } }
    }
}
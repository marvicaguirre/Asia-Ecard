using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Pfizer.Repository;
using Wizardsgroup.Core.Web;
using Wizardsgroup.Core.Web.Helpers;
using Wizardsgroup.Core.Web.Models;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Service;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class GridController : Controller
    {
        #region Members
        private readonly GridSettingService _setting; 
        #endregion

        #region Constructor
        public GridController()
        {
            _setting = new GridSettingService(new UnitOfWorkWrapper());
        } 
        #endregion

        [OutputCache(Duration = 3600, VaryByParam = "gridName;parameter", Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult GetColumnFormat(string gridName, string parameter)
        {
            var list = new List<ColumnFormat>();
            
            var gridSettings = _setting.GetAllSettingsForGrid(gridName);            
            if (gridSettings != null)
            {
                var settingViewModels = gridSettings.ToList().Select(o => o.Convert<GridSetting, GridSettingViewModel>());                                    
                if (settingViewModels.Any())
                {
                    //var primaryKeyColumnName = settingViewModels.First().DataSourceEntityKeyColumnName;
                    list.AddRange(settingViewModels.Select(gridSetting => GridSettingColumnHelper.FormatColumn(gridSetting, gridSetting.DataSourceEntityKeyColumnName, SecurityHelper.HasAccess)));
                }
                else
                {
                    var dto = new GridSettingViewModel
                    {
                        DataSourceEntityColumnName = "!!!Missing entry in GridSettings table for GridName: " + gridName,
                        GridColumnWidth = 500,
                        ModalWindowHeight = 500,
                        ModalWindowWidth = 500,
                    };
                    list.Add(GridSettingColumnHelper.FormatColumn(dto, "X", SecurityHelper.HasAccess));
                }
            }

            if (parameter.Trim().Length > 0)
            {
                List<String> parameterList = parameter.Split(',').ToList();
                // ***** For hideColumns **********
                // Extract hideColumns from parameter then the columns (field) to "hide" (width = 0px)
                if (parameter.Contains("hideColumns"))
                {
                    List<String> hideColumnList = parameterList.Find(o => o.StartsWith("hideColumns")).Split(':')[1].Split(';').ToList();
                    // TODO: refactor using LINQ
                    foreach (var columnFormat in list)
                    {
                        if (hideColumnList.Exists(o => o.Equals(columnFormat.field)))
                        {
                            columnFormat.width = "0px";
                        }
                    }
                }
            }

            var jSon = Json(list, JsonRequestBehavior.AllowGet);
            return jSon;
        }
    }    
}

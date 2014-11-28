using Pfizer.Repository;
using Wizardsgroup.Core.Web.Helpers;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Code.Extensions
{
    public static class StaticToDynamicExtension
    {
        public static dynamic ToDynamicFromGridSetting<T>(this T obj,string gridName)
        {
            //var dynamicDataToBind = GridDynamicColumnHelper.Instance.GetGridDynamicColumnSetting(() => new UnitOfWorkWrapper(), gridName, obj);
            //var dynamicDataToBind = GridDynamicColumnHelper.Instance.GetDynamicFieldAndAssignValue(() => new UnitOfWorkWrapper(), gridName, obj);
            //return obj.ToDynamic(dynamicDataToBind);
            var dynamicDataToBind = GridDynamicColumnHelper.Instance.GetGridDynamicColumnSetting(()=>new UnitOfWorkWrapper(), gridName, obj);
            return obj.ToDynamic(dynamicDataToBind);
        }
    }
}
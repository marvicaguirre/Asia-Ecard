using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Helpers;

namespace Wizardsgroup.Core.Web.Helpers
{
    // ReSharper disable ClassCannotBeInstantiated
    public sealed class GridDynamicColumnHelper
    // ReSharper restore ClassCannotBeInstantiated
    {
        private class GridDynamicColumn
        {
            public GridDynamicColumn()
            {
                TimeStamp = DateTime.Now;
                DynamicColumns = new ConcurrentDictionary<string, string>();
                StaticColumns = new List<string>();
            }
            public List<string> StaticColumns { get; set; }
            public ConcurrentDictionary<string, string> DynamicColumns { get; set; }
            public DateTime TimeStamp { get; private set; }
        }

        private static ConcurrentDictionary<string, GridDynamicColumn> _gridDynamicColumns;
        public static GridDynamicColumnHelper Instance
        {
            get { return Singleton<GridDynamicColumnHelper>.Instance; }
        }

        private GridDynamicColumnHelper()
        {
            _gridDynamicColumns = new ConcurrentDictionary<string, GridDynamicColumn>();
        }

        public PropertySetting GetGridDynamicColumnSetting(Func<IUnitOfWork> lazyLoadedUnitOfWork, string gridName, object instance)
        {
            var setting = new PropertySetting();
            var gridColumn = _gridDynamicColumns.SingleOrDefault(grid => grid.Key == gridName);
            if (gridColumn.Value != null)
            {
                if (gridColumn.Value.TimeStamp.AddHours(1) >= DateTime.Now)
                    return new PropertySetting
                    {
                        AdditionPropertyDictionary = GetDynamicFieldAndAssignValue(lazyLoadedUnitOfWork, gridName, instance),
                        Properties = gridColumn.Value.StaticColumns,
                    };

                GridDynamicColumn dynamicColumn;
                _gridDynamicColumns.TryRemove(gridName, out dynamicColumn);
                return new PropertySetting
                {
                    AdditionPropertyDictionary = GetDynamicFieldAndAssignValue(lazyLoadedUnitOfWork, gridName, instance),
                    Properties = gridColumn.Value.StaticColumns,
                };
            }

            setting.AdditionPropertyDictionary = GetDynamicFieldAndAssignValue(lazyLoadedUnitOfWork, gridName, instance);
            setting.Properties = _gridDynamicColumns.Single(o => o.Key == gridName).Value.StaticColumns;
            return setting;
        }

        public Dictionary<string, object> GetDynamicFieldAndAssignValue(Func<IUnitOfWork> lazyLoadedUnitOfWork, string gridName, object instance)
        {
            var fieldValue = new ConcurrentDictionary<string, object>();
            var fieldDictionary = GetDynamicColumns(lazyLoadedUnitOfWork, gridName);
            Parallel.ForEach(fieldDictionary.DynamicColumns, item =>
            {
                var value = ReflectionHelper.Instance.GetPropetyValue<object>(instance, item.Value);
                fieldValue.TryAdd(item.Key, value);
            });
            return fieldValue.ToDictionary(o => o.Key, o => o.Value);
        }

        private GridDynamicColumn GetDynamicColumns(Func<IUnitOfWork> lazyLoadedUnitOfWork, string gridName)
        {
            if (_gridDynamicColumns.ContainsKey(gridName))
            {
                var gridColumn = _gridDynamicColumns.Single(grid => grid.Key == gridName);
                if (gridColumn.Value.TimeStamp.AddHours(1) >= DateTime.Now) return gridColumn.Value;

                GridDynamicColumn dynamicColumn;
                _gridDynamicColumns.TryRemove(gridName, out dynamicColumn);
                return gridColumn.Value;
            }

            var concurrentDictionary = new ConcurrentDictionary<string, string>();
            var dictionary = new Dictionary<string, string>();
            var gridColumns = GetColumnsFromGrid(lazyLoadedUnitOfWork, gridName);
            gridColumns.ForEach(item =>
            {
                var dynamicColumns = gridColumns.Where(o => o.SortOrder != null);
                dynamicColumns = dynamicColumns.Where(o => o.DataSourceEntityColumnName.Contains(o.SortOrder.ToString()));
                dynamicColumns = dynamicColumns.Where(o => o.DataSourceEntityColumnName.Replace(o.SortOrder.ToString(), string.Empty) == item.DataSourceEntityColumnName);
                var dynamicColumnsNameOnly = dynamicColumns.Select(o => o.DataSourceEntityColumnName).ToList();

                dynamicColumnsNameOnly.ForEach(dynamicColumn =>
                {
                    if (dictionary.ContainsKey(dynamicColumn)) return;
                    dictionary.Add(dynamicColumn, item.DataSourceEntityColumnName);
                    concurrentDictionary.TryAdd(dynamicColumn, item.DataSourceEntityColumnName);
                });
            });
            var columnSettings = new GridDynamicColumn
            {
                DynamicColumns = concurrentDictionary,
                StaticColumns = gridColumns
                    .Where(o => !concurrentDictionary.Keys.Contains(o.DataSourceEntityColumnName))
                    .Select(o => o.DataSourceEntityColumnName)
                    .Distinct().ToList()
            };
            var firstColumn = gridColumns.First();
            var hasPrimeKeyExisting = columnSettings.StaticColumns.Any(item => item.Equals(firstColumn.DataSourceEntityKeyColumnName));

            if (!hasPrimeKeyExisting)
                columnSettings.StaticColumns.Add(firstColumn.DataSourceEntityKeyColumnName);

            _gridDynamicColumns.TryAdd(gridName, columnSettings);
            return columnSettings;
        }

        private List<GridSetting> GetColumnsFromGrid(Func<IUnitOfWork> lazyLoadedUnitOfWork, string gridName)
        {
            List<GridSetting> gridColumns;
            using (var unitOfWork = lazyLoadedUnitOfWork())
            {
                gridColumns = unitOfWork.Repository<GridSetting>().GetAll.Where(o => o.GridName == gridName).ToList();
            }
            return gridColumns;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Wizardsgroup.Domain.Enumerations;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public class GridSchemaCollectionRegistrator : IGridSchemaCollectionRegistrator
    {
        #region Members/Properties
        private readonly CommonGridDataSchema _commonGridDataSchema;
        private readonly FluentGridSchemaRegistrator _fluentGridSchemaRegistrator;
        public IContext Context { get; private set; }
        #endregion

        #region Constructor
        public GridSchemaCollectionRegistrator(IContext context, CommonGridDataSchema commonGridDataSchema)
        {
            commonGridDataSchema.Guard("CommonGridDataSchema must not be null.");
            context.Guard("IContext must not be null.");
            Context = context;
            _commonGridDataSchema = commonGridDataSchema;
            _fluentGridSchemaRegistrator = new FluentGridSchemaRegistrator
                {
                    GridSchemaContainer =
                        {
                            GridName = commonGridDataSchema.GridName,
                            ModelName = commonGridDataSchema.ModelName,
                            ModelNamespace = commonGridDataSchema.ModelNamespace,
                            PrimaryKeyName = commonGridDataSchema.PrimaryKeyName
                        }
                };
        }
        #endregion

        #region Public Functions/Methods
        public void Register(Action<IFluentGridSchemaRegistrator> fluentGridSchemaRegistrator)
        {
            fluentGridSchemaRegistrator.Guard("IFluentGridSchemaRegistrator must not be null.");
            fluentGridSchemaRegistrator(_fluentGridSchemaRegistrator);

            var gridSettings = new List<GridSetting>();
            var sameColumnNameDictionary = new List<string>();
            _fluentGridSchemaRegistrator.Container.ForEach(schema =>
                {
                    var gridSettingDataBuilder = schema.GridSchema().GetSchemaSetting();
                    CreateDynamicColumnNameForDuplicateColumn(schema, sameColumnNameDictionary);
                    BuildGridSetting(gridSettingDataBuilder, schema);
                    gridSettings.Add(gridSettingDataBuilder.GetGridInstance());
                    //Reset name to original
                    schema.ModelPropetyNameToBind = schema.ModelPropetyNameToBind.Replace(schema.Order.ToString(CultureInfo.InvariantCulture), string.Empty);
                });

            UnlockACellWhenAllAreLocked(gridSettings);

            gridSettings.ForEach(col => Context.EntitySet<GridSetting>().Add(col));
            Context.SaveChanges();
        }
        public void Unregister()
        {
            var columns = Context.EntitySet<GridSetting>().Where(g => g.GridName.Equals(_commonGridDataSchema.GridName)).ToList();
            columns.ForEach(o => Context.EntitySet<GridSetting>().Remove(o));
            Context.SaveChanges();
        }
        #endregion

        #region Private Functions/Methods
        private void BuildGridSetting(IGridSettingDataBuilder gridSettingDataBuilder, IGridSchemaContainer schema)
        {
            gridSettingDataBuilder.EntityType(schema.ModelNamespace, schema.ModelName)
                .EntityProperties(schema.PrimaryKeyName, schema.ModelPropetyNameToBind)
                .SortOrder(schema.Order)
                .GridProperty(schema.GridName);
        }

        private void CreateDynamicColumnNameForDuplicateColumn(IGridSchemaContainer schema, ICollection<string> sameColumnNameDictionary)
        {
            var hasSameKey = _fluentGridSchemaRegistrator.Container
                .Where(toCheck => toCheck.UniqueId != schema.UniqueId)
                .Any(toCheck => toCheck.ModelPropetyNameToBind == schema.ModelPropetyNameToBind);

            var isFirstInstance = sameColumnNameDictionary.Any(toCheck => toCheck == schema.ModelPropetyNameToBind);
            //Create new keyName for grid to create dynamic column
            var suffix = hasSameKey && isFirstInstance ? schema.Order.ToString(CultureInfo.InvariantCulture) : string.Empty;
            schema.ModelPropetyNameToBind = string.Format("{0}{1}", schema.ModelPropetyNameToBind, suffix);
            if (hasSameKey) sameColumnNameDictionary.Add(schema.ModelPropetyNameToBind);
        }

        private void UnlockACellWhenAllAreLocked(IReadOnlyCollection<GridSetting> gridSettings)
        {
            if (!gridSettings.All(col => col.GridColumnLocked.HasValue && col.GridColumnLocked.Value)) return;

            if (gridSettings.Count == 1)
            {
                gridSettings.First().GridColumnLocked = false;
            }
            else
            {
                UnlockNonCheckBoxCell(gridSettings);
            }
        }

        private void UnlockNonCheckBoxCell(IEnumerable<GridSetting> gridSettings)
        {
            Func<GridSetting, bool> filterSpecification =
                col => col.GridCellType == GridCellType.RegularCell.ToString() || col.GridCellType == GridCellType.LinkModal.ToString();

            var gridSetting = gridSettings.OrderBy(o => o.SortOrder).FirstOrDefault(filterSpecification);

            if (gridSetting != null)
            {
                gridSetting.GridColumnLocked = false;
            }
        }
        #endregion
    }
}

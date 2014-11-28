using System;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public class GridSchemaContainer : CommonGridDataSchema, IGridSchemaContainer
    {
        public string ModelPropetyNameToBind { get; set; }
        public int Order { get; set; }
        public Func<IGridSettingDataBuilderWrapper> GridSchema { get; set; }

        public string UniqueId
        {
            get { return string.Format("{0}{1}{2}{3}{4}{5}", GridName, ModelNamespace, ModelName, PrimaryKeyName, ModelPropetyNameToBind, Order); }
        }
    }
}

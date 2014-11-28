using System;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSchemaContainer
    {
        string GridName { get; set; }
        string PrimaryKeyName { get; set; }
        string ModelName { get; set; }
        string ModelNamespace { get; set; }
        string ModelPropetyNameToBind { get; set; }
        int Order { get; set; }
        string UniqueId { get; }
        Func<IGridSettingDataBuilderWrapper> GridSchema { get; set; }
    }
}
namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSettingCheckboxBuilder : IGridSettingBuilderWrapper
    {
        IGridSettingCheckboxBuilder CellProperties(string gridHeaderTitle, int? gridColumnWidth = null, string gridColumnName = ""); 
    }
}
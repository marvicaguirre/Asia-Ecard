

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSettingDataBuilderWrapper
    {
        IGridSettingCheckboxBuilder CreateCheckbox();
        IGridSettingLinkModalBuilder CreateLinkModal();
        IGridSettingRegularCellBuilder CreateRegularCell();
        IGridSettingLinkDetailBuilder CreateLinkDetail(bool overrideKey = false);
        IGridSettingDataBuilder GetSchemaSetting();
        IGridSettingActionLinkBuilder CreateActionLink();
        bool OverrideKey { get; set; }
    }
}

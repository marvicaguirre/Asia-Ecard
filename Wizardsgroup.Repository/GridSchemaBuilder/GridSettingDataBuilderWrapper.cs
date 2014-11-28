

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public class GridSettingDataBuilderWrapper : IGridSettingDataBuilderWrapper
    {
        private readonly IGridSettingDataBuilder _gridSettingDataBuilder = new GridSettingDataBuilder();

        public IGridSettingCheckboxBuilder CreateCheckbox()
        {
            return new GridSettingCheckboxBuilder(this, _gridSettingDataBuilder);
        }

        public IGridSettingLinkModalBuilder CreateLinkModal()
        {            
            return new GridSettingLinkModalBuilder(this, _gridSettingDataBuilder);
        }

        public IGridSettingRegularCellBuilder CreateRegularCell()
        {
            return new GridSettingRegularCellBuilder(this, _gridSettingDataBuilder);
        }

        public IGridSettingLinkDetailBuilder CreateLinkDetail(bool overrideKey = false)
        {
            OverrideKey = overrideKey;
            return new GridSettingLinkDetailBuilder(this, _gridSettingDataBuilder);
        }

        public IGridSettingActionLinkBuilder CreateActionLink()
        {
            return new GridSettingActionLinkBuilder(this, _gridSettingDataBuilder);
        }

        public IGridSettingDataBuilder GetSchemaSetting()
        {
            return _gridSettingDataBuilder;
        }

        public bool OverrideKey { get; set; }
    }
}

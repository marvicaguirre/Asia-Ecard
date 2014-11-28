using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public abstract class AbstractSpecificGridColumnBuilder : IGridSettingBuilderWrapper
    {
        private readonly IGridSettingDataBuilderWrapper _gridSettingDataBuilderWrapper;
        protected IGridSettingDataBuilder GridSettingDataBuilder { get; private set; }
    
        protected AbstractSpecificGridColumnBuilder(IGridSettingDataBuilderWrapper gridSettingDataBuilderWrapper,IGridSettingDataBuilder gridSettingDataBuilder)
        {            
            gridSettingDataBuilderWrapper.Guard("IGridSettingDataBuilderWrapper must not be null.");
            _gridSettingDataBuilderWrapper = gridSettingDataBuilderWrapper;
            GridSettingDataBuilder = gridSettingDataBuilder;
        }

        public IGridSettingDataBuilderWrapper GetWrapperInstance()
        {
            return _gridSettingDataBuilderWrapper;
        }
    }
}

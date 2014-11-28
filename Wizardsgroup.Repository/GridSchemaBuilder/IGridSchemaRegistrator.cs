using System;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSchemaRegistrator
    {
        IFluentGridSchemaRegistrator Use(Func<IGridSettingDataBuilderWrapper> schema);
    }
}

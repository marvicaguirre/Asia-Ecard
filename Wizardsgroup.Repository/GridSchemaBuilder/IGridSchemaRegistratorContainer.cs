using System.Collections.Generic;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSchemaRegistratorContainer
    {
        List<IGridSchemaContainer> Container { get; } 
    }
}
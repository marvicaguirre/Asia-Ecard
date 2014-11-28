using System.Collections.Generic;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public class FluentGridSchemaRegistrator : IFluentGridSchemaRegistrator , IGridSchemaRegistratorContainer
    {
        public List<IGridSchemaContainer> Container { get; private set; }
        public GridSchemaContainer GridSchemaContainer { get; private set; }

        public FluentGridSchemaRegistrator()
        {
            Container = new List<IGridSchemaContainer>();
            GridSchemaContainer = new GridSchemaContainer();
        }
        
        public IGridSchemaRegistrator For(string modelPropertyToBind, int columnOrder)
        {            
            var registrator = new GridSchemaRegistrator(this, modelPropertyToBind, columnOrder);
            return registrator;
        }
    }
}

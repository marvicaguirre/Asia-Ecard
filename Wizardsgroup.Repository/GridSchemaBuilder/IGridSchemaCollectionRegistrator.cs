using System;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSchemaCollectionRegistrator
    {
        IContext Context { get; }
        void Register(Action<IFluentGridSchemaRegistrator> fluentGridSchemaRegistrator);
        void Unregister();
    }
}
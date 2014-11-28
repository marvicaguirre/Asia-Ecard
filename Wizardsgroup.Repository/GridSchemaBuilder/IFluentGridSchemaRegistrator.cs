namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IFluentGridSchemaRegistrator
    {
        IGridSchemaRegistrator For(string modelPropertyToBind,int columnOrder);
    }
}
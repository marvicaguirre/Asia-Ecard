namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IBuildGridSchema
    {
        void CreateSchema(IContext context);

        void DropSchema(IContext context);
    }
}
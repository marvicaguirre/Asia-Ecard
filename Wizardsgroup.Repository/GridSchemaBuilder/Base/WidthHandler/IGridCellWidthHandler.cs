namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal interface IGridCellWidthHandler
    {
        bool Condition(GridCellWidthContainer gridSpecs);
        int? Handle(GridCellWidthContainer gridSpecs);
    }         
}
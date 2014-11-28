namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal class StatusHeaderNameWidthHandler : IGridCellWidthHandler
    {
        public bool Condition(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.Title.ToLower().Equals("status");
        }

        public int? Handle(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.Width ?? 100;
        }
    }
}
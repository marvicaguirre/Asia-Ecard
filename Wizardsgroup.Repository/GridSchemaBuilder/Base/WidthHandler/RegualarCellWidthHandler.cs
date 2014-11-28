using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal class RegularCellWidthHandler : IGridCellWidthHandler
    {
        public bool Condition(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.GridCellType == GridCellType.RegularCell && !gridSpecs.Title.ToLower().Equals("status");
        }

        public int? Handle(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.Width ?? 325;
        }
    }
}
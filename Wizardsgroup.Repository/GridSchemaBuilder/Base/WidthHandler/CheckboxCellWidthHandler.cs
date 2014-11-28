using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal class CheckboxCellWidthHandler : IGridCellWidthHandler
    {
        public bool Condition(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.GridCellType == GridCellType.CheckBox;
        }

        public int? Handle(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.Width ?? 30;
        }
    }
}
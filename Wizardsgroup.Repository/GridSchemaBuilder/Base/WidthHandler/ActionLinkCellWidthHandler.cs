using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder.Base.WidthHandler
{
    internal class ActionLinkCellWidthHandler : IGridCellWidthHandler
    {
        public bool Condition(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.GridCellType == GridCellType.ActionLink;
        }

        public int? Handle(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.Width ?? 30;
        }
    }
}

using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal class ModalCellWidthHandler : IGridCellWidthHandler
    {
        public bool Condition(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.GridCellType == GridCellType.LinkModal;
        }

        public int? Handle(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.Width ?? 275;
        }
    }
}
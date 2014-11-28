using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal sealed class GridCellWidthContainer
    {
        public int? Width { get; set; }
        public GridCellType GridCellType { get; set; }
        public string Title { get; set; }
    }
}
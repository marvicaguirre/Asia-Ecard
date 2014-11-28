using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal class LinkDetailCellWidthHandler : IGridCellWidthHandler
    {
        public bool Condition(GridCellWidthContainer gridSpecs)
        {
            return gridSpecs.GridCellType == GridCellType.LinkDetails && !gridSpecs.Title.ToLower().Equals("status");
        }

        public int? Handle(GridCellWidthContainer gridSpecs)
        {
            var isSizeLessThanSpecified = !gridSpecs.Width.HasValue || GetPixelSizeFromTextLength(gridSpecs.Title) > gridSpecs.Width.Value;
            if (isSizeLessThanSpecified)
            {
                gridSpecs.Width = GetPixelSizeFromTextLength(gridSpecs.Title);
            }
            return gridSpecs.Width;
        }

        private int GetPixelSizeFromTextLength(string text)
        {
            var computedPixel = text.Length*13;
            return computedPixel > 100 ? computedPixel : 100 ;
        }
    }
}
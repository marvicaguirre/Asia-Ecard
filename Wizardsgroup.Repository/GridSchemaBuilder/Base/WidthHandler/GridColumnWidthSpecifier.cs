using System.Collections.Concurrent;
using System.Linq;
using Wizardsgroup.Utilities.Helpers;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal sealed class GridColumnWidthSpecifier
    {
        private readonly ConcurrentBag<IGridCellWidthHandler> _handlers;
        public static GridColumnWidthSpecifier Instance
        {
            get { return Singleton<GridColumnWidthSpecifier>.Instance; }
        }

        private GridColumnWidthSpecifier() { _handlers = new ConcurrentBag<IGridCellWidthHandler>(); }

        public GridColumnWidthSpecifier RegisterHandler(IGridCellWidthHandler handler)
        {
            var hasNoContent = _handlers.Count == 0;
            if (hasNoContent)
            {
                _handlers.Add(handler);
                return this;
            }                
            var hasEntry = _handlers.Any(o => o.GetType() == handler.GetType());
            if (hasEntry) return this;
            
            _handlers.Add(handler);
            return this;
        }

        public int? SetWidth(GridCellWidthContainer gridSpecs)
        {
            var widthHandler = _handlers.First(gridCellWidthHandler => gridCellWidthHandler.Condition(gridSpecs));
            return widthHandler.Handle(gridSpecs);
        }
    }
}

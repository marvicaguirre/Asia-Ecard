using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public class GridSettingActionLinkBuilder : AbstractSpecificGridColumnBuilder, IGridSettingActionLinkBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridSettingActionLinkBuilder"/> class.
        /// </summary>
        /// <param name="gridSettingDataBuilderWrapper">The grid setting data builder wrapper.</param>
        /// <param name="gridSettingDataBuilder">The grid setting data builder.</param>
        public GridSettingActionLinkBuilder(IGridSettingDataBuilderWrapper gridSettingDataBuilderWrapper, IGridSettingDataBuilder gridSettingDataBuilder)
            : base(gridSettingDataBuilderWrapper, gridSettingDataBuilder)
        {
        }

        /// <summary>
        /// Cells the properties.
        /// </summary>
        /// <param name="gridHeaderTitle">The grid header title.</param>
        /// <param name="gridColumnWidth">Width of the grid column.</param>
        /// <param name="gridColumnName">Name of the grid column.</param>
        /// <returns></returns>
        public IGridSettingActionLinkBuilder CellProperties(string gridHeaderTitle, int? gridColumnWidth = null, string gridColumnName = "")
        {
            GridSettingDataBuilder.CellProperties(GridCellType.ActionLink, gridHeaderTitle, gridColumnWidth, gridColumnName);

            return this;
        }

        /// <summary>
        /// Cells the display format.
        /// </summary>
        /// <param name="gridCellDisplayFormat">The grid cell display format.</param>
        /// <param name="gridCellDisplayText">The grid cell display text.</param>
        /// <returns></returns>
        public IGridSettingActionLinkBuilder CellDisplayFormat(string gridCellDisplayFormat = "", string gridCellDisplayText = "")
        {
            GridSettingDataBuilder.CellDisplayFormat(gridCellDisplayFormat, gridCellDisplayText);

            return this;
        }

        /// <summary>
        /// Cells the behaviour.
        /// </summary>
        /// <param name="gridColumnFilterable">The grid column filterable.</param>
        /// <param name="gridColumnSortable">The grid column sortable.</param>
        /// <param name="gridColumnAlignment">The grid column alignment.</param>
        /// <returns></returns>
        public IGridSettingActionLinkBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable, string gridColumnAlignment = "")
        {
            GridSettingDataBuilder.CellBehaviour(gridColumnFilterable, gridColumnSortable, gridColumnAlignment);

            return this;
        }

        /// <summary>
        /// Controllers the properties.
        /// </summary>
        /// <param name="area">The area.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="controllerAction">The controller action.</param>
        /// <returns></returns>
        public IGridSettingActionLinkBuilder ControllerProperties(string area, string controllerName, string controllerAction)
        {
            GridSettingDataBuilder.ControllerProperties(area, controllerName, controllerAction);

            return this;
        }

        /// <summary>
        /// Lockables the specified lockable.
        /// </summary>
        /// <param name="lockable">if set to <c>true</c> [lockable].</param>
        /// <returns></returns>
        public IGridSettingActionLinkBuilder Lockable(bool lockable = true)
        {
            GridSettingDataBuilder.Lockable(lockable);

            return this;
        }
    }
}

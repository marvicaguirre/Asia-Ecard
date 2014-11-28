using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal class GridSchemaRegistratorVisitor
    {
        #region Member

        private readonly GridSchemaContainer _schemaContainer;
        private readonly Func<IGridSettingDataBuilderWrapper> _expression;
        private List<Action<GridSchemaContainer, IGridSchemaRegistratorContainer>> ActionsToInvoke { get; set; }
        #endregion

        #region Constructor
        public GridSchemaRegistratorVisitor(GridSchemaContainer schemaContainer, Func<IGridSettingDataBuilderWrapper> expression)
        {
            expression.Guard("Expresion must not be null.");
            schemaContainer.Guard("IGridSchemaContainer must not be null");
            _schemaContainer = schemaContainer;
            _expression = expression;
            ActionsToInvoke = new List<Action<GridSchemaContainer, IGridSchemaRegistratorContainer>>
                {
                    UpdateRegisteredSchema,
                    RegisterNewSchema
                };
        }
        #endregion

        #region Public Functions
        public void Visit(IGridSchemaRegistratorContainer fluentServiceRegistrator)
        {
            fluentServiceRegistrator.Guard("FluentServiceRegistrator must not be null.");
            ActionsToInvoke.ForEach(action => action(_schemaContainer, fluentServiceRegistrator));
        }
        #endregion

        #region Private Functions/Methods
        void UpdateRegisteredSchema(GridSchemaContainer schemaContainer, IGridSchemaRegistratorContainer fluentServiceRegistrator)
        {
            var isNotRegistered =
                fluentServiceRegistrator.Container.All(o => ((GridSchemaContainer) o).UniqueId != schemaContainer.UniqueId);

            if (isNotRegistered)
                return;

            fluentServiceRegistrator.Container.Find(o => ((GridSchemaContainer)o).UniqueId == schemaContainer.UniqueId).GridSchema = _expression;
        }

        void RegisterNewSchema(GridSchemaContainer schemaContainer, IGridSchemaRegistratorContainer fluentServiceRegistrator)
        {
            var isRegistered = fluentServiceRegistrator.Container.Any(o => ((GridSchemaContainer) o).UniqueId == schemaContainer.UniqueId);
            if (isRegistered)
                return;

            fluentServiceRegistrator.Container.Add(new GridSchemaContainer
            {
                GridName = schemaContainer.GridName,
                ModelName = schemaContainer.ModelName,
                ModelNamespace = schemaContainer.ModelNamespace,
                PrimaryKeyName = schemaContainer.PrimaryKeyName,
                Order = schemaContainer.Order,
                ModelPropetyNameToBind = schemaContainer.ModelPropetyNameToBind,
                GridSchema = _expression
            });
        }
        #endregion
    }
}

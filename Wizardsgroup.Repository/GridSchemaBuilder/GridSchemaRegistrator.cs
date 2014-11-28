using System;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal class GridSchemaRegistrator : IGridSchemaRegistrator
    {
        private readonly FluentGridSchemaRegistrator _fluentRegistrator;        

        public GridSchemaRegistrator(FluentGridSchemaRegistrator fluentRegistrator, string modelPropertyToBind, int columnOrder)
        {
            fluentRegistrator.Guard("FluentGridSchemaRegistrator must not be null.");            
            if (string.IsNullOrEmpty(modelPropertyToBind)) throw new ArgumentNullException(modelPropertyToBind);
            if (columnOrder == 0) throw new Exception("columnOrder should be more than 0");
            _fluentRegistrator = fluentRegistrator;
            _fluentRegistrator.GridSchemaContainer.Order = columnOrder;
            _fluentRegistrator.GridSchemaContainer.ModelPropetyNameToBind = modelPropertyToBind;
        }

        public IFluentGridSchemaRegistrator Use(Func<IGridSettingDataBuilderWrapper> schema)
        {
            schema.Guard("Expression must not be null");
            var visitor = new GridSchemaRegistratorVisitor(_fluentRegistrator.GridSchemaContainer,schema);
            visitor.Visit(_fluentRegistrator);
            return _fluentRegistrator;
        }
    }
}

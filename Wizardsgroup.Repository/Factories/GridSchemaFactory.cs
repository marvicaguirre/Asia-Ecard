using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Repository.GridSchemaBuilder;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Interface;

namespace Wizardsgroup.Repository.Factories
{
    public class GridSchemaFactory
    {
        private readonly IReflection _reflectionHelper;
        private readonly string _assemblyName;

        public GridSchemaFactory(IReflection reflectionHelper,string assemblyName)
        {
            reflectionHelper.Guard("IReflection should not be null");
            _reflectionHelper = reflectionHelper;
            _assemblyName = assemblyName;
        }

        public List<IBuildGridSchema> CreateGridDataBuilder()
        {
            var assemblyTypes = _reflectionHelper.GetTypesFromAssembly(_assemblyName);
            var gridDataBuilders = _reflectionHelper.GetTypesWithImplementingInterface<IBuildGridSchema>(assemblyTypes);
            return gridDataBuilders.Select(CreateIntanceOfRuleType).ToList();
        }

        private IBuildGridSchema CreateIntanceOfRuleType(Type gridBuilder)
        {
            var gridInstance = _reflectionHelper.CreateInstanceOfType<IBuildGridSchema>(gridBuilder);
            return gridInstance;
        }

    }
}

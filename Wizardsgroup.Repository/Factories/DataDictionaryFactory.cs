using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Repository.DataDictionaryBuilder;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Interface;

namespace Wizardsgroup.Repository.Factories
{
    public class DataDictionaryFactory
    {
        private readonly IReflection _reflectionHelper;
        private readonly string _assemblyName;

        public DataDictionaryFactory(IReflection reflectionHelper, string assemblyName)
        {
            reflectionHelper.Guard("IReflection should not be null");
            _reflectionHelper = reflectionHelper;
            _assemblyName = assemblyName;
        }

        public List<IDataDictionarySeeder> CreateDataDictionarySeederDataBuilder()
        {
            var assemblyTypes = _reflectionHelper.GetTypesFromAssembly(_assemblyName);
            var seeders = _reflectionHelper.GetTypesWithImplementingInterface<IDataDictionarySeeder>(assemblyTypes);
            return seeders.Select(CreateIntanceOfDataDictionarySeeder).ToList();
        }

        private IDataDictionarySeeder CreateIntanceOfDataDictionarySeeder(Type gridBuilder)
        {
            var seeders = _reflectionHelper.CreateInstanceOfType<IDataDictionarySeeder>(gridBuilder);
            return seeders;
        }
    }
}

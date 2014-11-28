using Wizardsgroup.Repository.Factories;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Interface;

namespace Wizardsgroup.Repository.DataDictionaryBuilder
{
    public class DataDictionarySeederController : IDataSeeder
    {
        private readonly IReflection _reflectionHelper;
        private readonly string _assemblyName;

        public DataDictionarySeederController(IReflection reflectionHelper,string assemblyName)
        {
            assemblyName.Guard("AssemblyName must not be empty or null.");
            reflectionHelper.Guard("ReflectionHelper must not be null.");
            _reflectionHelper = reflectionHelper;
            _assemblyName = assemblyName;
        }

        public void Seed(IContext context)
        {
            var factory = new DataDictionaryFactory(_reflectionHelper,_assemblyName);
            var seeders = factory.CreateDataDictionarySeederDataBuilder();
            seeders.ForEach(o=>o.Seed(context));
        }
    }
}

using Wizardsgroup.Repository;
using Wizardsgroup.Repository.DataDictionaryBuilder;
using Wizardsgroup.Repository.Factories;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Repository.Context
{
    internal class CommonContextInitializer
    {
        public void InitializeDatabase(IContext context)
        {
            GridDataSeed(context);
            SeedData(context);
            DataDictionarySeeder(context);
        }
        
        private void GridDataSeed(IContext context)
        {
            var factory = new GridSchemaFactory(ReflectionHelper.Instance, "Pfizer.Repository");
            var collectionOfGridDataBuilder = factory.CreateGridDataBuilder();
            collectionOfGridDataBuilder.ForEach(buildGridData =>
            {
                buildGridData.DropSchema(context);
                buildGridData.CreateSchema(context);
            });
        }

        private void DataDictionarySeeder(IContext context)
        {
            var seederController = new DataDictionarySeederController(ReflectionHelper.Instance, "Pfizer.Repository");
            seederController.Seed(context);
        }

        private void SeedData(IContext context)
        {
            var seederController = new SeederController();
            seederController.Seed(context);
        }
    }
}

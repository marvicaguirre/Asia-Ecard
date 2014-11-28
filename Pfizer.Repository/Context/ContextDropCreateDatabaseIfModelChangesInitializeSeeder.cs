using System.Data.Entity;
using Wizardsgroup.Repository;

namespace Pfizer.Repository.Context
{
    internal class ContextDropCreateDatabaseIfModelChangesInitializeSeeder :
        DropCreateDatabaseIfModelChanges<MainContext>
    {
        private readonly IExternalSeeder _instance;

        public ContextDropCreateDatabaseIfModelChangesInitializeSeeder() : this(new NullExternalSeeder())
        {
            
        }
        public ContextDropCreateDatabaseIfModelChangesInitializeSeeder(IExternalSeeder instance)
        {
            _instance = instance;
        }

        protected override void Seed(MainContext context)
        {
            new DescriptionUpdater<MainContext>(context).UpdateDescriptions();

            var initializer = new CommonContextInitializer();
            initializer.InitializeDatabase(context);
            _instance.Seed(context);
            base.Seed(context);
            
        }
    }

    internal class NullExternalSeeder : IExternalSeeder
    {
        public  void Seed(IContext context)
        {
            //Do nothing;
        }
    }
}

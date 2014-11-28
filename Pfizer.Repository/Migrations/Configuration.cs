using Pfizer.Repository.Context;
using Wizardsgroup.Repository;

namespace Pfizer.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MainContext context)
        {
            new DescriptionUpdater<MainContext>(context).UpdateDescriptions();
            MainContext.DatabaseInitializerMode = DatabaseInitializerMode.Migration;
            var seeder = new CommonContextInitializer();
            seeder.InitializeDatabase(context);
        }
    }
}

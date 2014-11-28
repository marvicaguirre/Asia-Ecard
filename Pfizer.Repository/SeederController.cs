using System.Collections.Generic;
using Wizardsgroup.Repository;

namespace Pfizer.Repository
{
    internal class SeederController : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var seeders = GetSeeders();
            seeders.ForEach(seeder => seeder.Seed(context));
        }

        private List<IDataSeeder> GetSeeders()
        {
            var seeders = new List<IDataSeeder>();
            seeders.AddRange(new SystemDefaultSeedController().GetSeeders());            
            seeders.AddRange(new ClientSeedController().GetSeeders());
            return seeders;
        }
    }
}
using System.Collections.Generic;
using Wizardsgroup.Repository;

namespace Pfizer.Repository
{
    internal class ClientSeedController
    {
        public List<IDataSeeder> GetSeeders()
        {
            return new List<IDataSeeder>();
        }
    }
}

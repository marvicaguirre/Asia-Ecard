using System.Data.Entity.Migrations;
using Wizardsgroup.Repository;
using Pfizer.Domain.Models;
using Pfizer.Domain.Constants;

namespace Pfizer.Repository.SeedData
{
    internal class UserGroupSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var record = new UserGroup
                {                    
                    Name = "Administrator",
                    Description = "The default System Administrator group. This should not be deleted",
                    CreatedBy = "System",                    
                };
            context.EntitySet<UserGroup>().AddOrUpdate(c => new { c.Name }, record);
          
            context.SaveChanges();
        }
    }
}

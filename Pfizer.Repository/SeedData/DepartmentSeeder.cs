using System.Data.Entity.Migrations;
using Pfizer.Domain.Models;
using Wizardsgroup.Repository;

namespace Pfizer.Repository.SeedData
{
    internal class DepartmentSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var record = new Department
            {
                Name = "IT",
                IsSBD = true,
                CreatedBy = "System"
            };
            context.EntitySet<Department>().AddOrUpdate(c => new { c.Name }, record);

            context.SaveChanges();
        }
    }
}
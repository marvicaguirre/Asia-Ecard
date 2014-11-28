using System.Data.Entity.Migrations;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Repository;

namespace Pfizer.Repository.SeedData
{
    internal class CompanyClassificationSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var record = new CompanyClassification
            {
                Name = "Host",
                CreatedBy = "System"
            };
            context.EntitySet<CompanyClassification>().AddOrUpdate(c => new { c.Name }, record);

            context.SaveChanges();
        }
    }
}

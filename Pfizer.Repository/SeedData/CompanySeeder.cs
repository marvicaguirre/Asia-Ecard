using System.Data.Entity.Migrations;
using System.Linq;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Repository;
using Pfizer.Domain.Models;

namespace Pfizer.Repository.SeedData
{
    internal class CompanySeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var classificationId = context.EntitySet<CompanyClassification>().Single(o => o.Name == "Host").CompanyClassificationId;
            var record = new Company
            {
                Name = "Pfizer",
                CompanyClassificationId = classificationId,
                CreatedBy = "System"
            };
            context.EntitySet<Company>().AddOrUpdate(c => new { c.Name }, record);

            context.SaveChanges();
        }
    }
}

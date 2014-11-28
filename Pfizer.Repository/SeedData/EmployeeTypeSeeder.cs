using System;
using System.Data.Entity.Migrations;
using Pfizer.Domain.Models;
using Wizardsgroup.Repository;

namespace Pfizer.Repository.SeedData
{
    internal class EmployeeTypeSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var record = new EmployeeType
            {
                Name = "Regular",
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<EmployeeType>().AddOrUpdate(c => new { c.Name }, record);            
            context.SaveChanges();
        }
    }
}

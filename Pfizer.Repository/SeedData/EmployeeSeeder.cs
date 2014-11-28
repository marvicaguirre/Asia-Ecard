using System.Data.Entity.Migrations;
using System.Linq;
using Pfizer.Domain.Constants;
using Wizardsgroup.Repository;
using Pfizer.Domain.Models;

namespace Pfizer.Repository.SeedData
{
    internal class EmployeeSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {            
            var deptId = context.EntitySet<Department>().Single(o => o.Name == "IT").DepartmentId;
            var companyId = context.EntitySet<Company>().Single(o => o.Name == "Pfizer").CompanyId;
            var record = new Employee
            {
                FirstName = "Administrator",
                LastName = "Administrator",
                MiddleName = "Administrator",
                DepartmentId = deptId,
                EmployeeTypeId = context.EntitySet<EmployeeType>().Single(o => o.Name == "Regular").EmployeeTypeId,
                IsSupervisor = true,
                CompanyId = companyId,
                CreatedBy = "System"
            };
            context.EntitySet<Employee>().AddOrUpdate(c => new { c.FirstName, c.LastName, c.MiddleName }, record);
            context.SaveChanges();

            var adminUser = context.EntitySet<User>().Single(o => o.UserName == "admin");
            adminUser.EmployeeId = record.EmployeeId;
            context.SetModifiedState(adminUser);
            context.SaveChanges();
            
            context.SaveChanges();
        }
    }
}
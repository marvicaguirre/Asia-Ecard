using System.Data.Entity.Migrations;
using Wizardsgroup.Repository;
using Wizardsgroup.Utilities.Helpers;
using Pfizer.Domain.Models;

namespace Pfizer.Repository.SeedData
{
    internal class UserSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var salt = PasswordHelper.CreateSalt("admin");
            var hashPassword = PasswordHelper.HashPassword(salt, "1234");
            var record = new User
            {
                UserName = "admin",                
                CreatedBy = "System",                
                Password = hashPassword
            };
            context.EntitySet<User>().AddOrUpdate(c => new { c.UserName }, record);

            context.SaveChanges();
        }
    }
}

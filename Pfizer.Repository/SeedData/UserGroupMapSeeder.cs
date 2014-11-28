using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Wizardsgroup.Repository;
using Pfizer.Domain.Models;

namespace Pfizer.Repository.SeedData
{
    internal class UserGroupMapSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var userId = (from cm in context.EntitySet<User>()
                          where cm.UserName == "admin"
                          select cm.UserId).FirstOrDefault();
            var groupId = (from cm in context.EntitySet<UserGroup>()
                           where cm.Name == "Administrator"
                           select cm.UserGroupId).FirstOrDefault();

            var existingId = from cm in context.EntitySet<UserGroupMap>()
                             where cm.UserId.Value == userId
                                   & cm.UserGroupId.Value == groupId
                             select cm.UserId;

            if (!existingId.Any())
            {
                var record = new UserGroupMap
                {                    
                    UserId = userId,
                    UserGroupId = groupId,
                    CreatedBy = "System",
                };
                context.EntitySet<UserGroupMap>().AddOrUpdate(record);                
            }

            context.SaveChanges();
        }
    }
}

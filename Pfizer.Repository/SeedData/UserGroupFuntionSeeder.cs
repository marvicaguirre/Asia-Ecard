using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Repository;
using Wizardsgroup.Utilities.Security;
using Pfizer.Domain.Infrastructure;
using Pfizer.Domain.Models;

namespace Pfizer.Repository.SeedData
{
    internal class UserGroupFuntionSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var dict = new Dictionary<string, List<SecurityFunctionInfo>>();
            var securityInfo = new FunctionHelper(RegisterModuleFunctionContainer.Instance.Container).GetFunctionNames();
            dict.Add("Administrator", securityInfo.ToList());

            foreach (var item in dict)
            {
                var groupName = item.Key;
                var functionList = item.Value;

                string grpName = groupName;
                var groupId = from cm in context.EntitySet<UserGroup>()
                              where cm.Name == grpName
                              select cm.UserGroupId;

                foreach (var function in functionList)
                {
                    var funcName = function.FunctionName;
                    var centralFunctionId = from cm in context.EntitySet <CentralFunction>()
                                            where cm.FunctionName == funcName
                                            select cm.CentralFunctionId;

                    if (centralFunctionId.Any())
                    {
                        var record = new UserGroupFunction
                        {
                            /* CentralFunction =  could be a collection; no need to generate this now;;*/
                            /* UserGroup =  could be a collection; no need to generate this now;;*/                            
                            Note = string.Format("{0}-{1}", funcName, groupId.First()),
                            Description = "",
                            CentralFunctionId = centralFunctionId.First(),
                            UserGroupId = groupId.First(),
                            CreatedBy = "System",
                        };
                        context.EntitySet<UserGroupFunction>().AddOrUpdate(c => new { c.Note } , record);
                    }
                }
            }
           
            context.SaveChanges();
        }        
    }
}

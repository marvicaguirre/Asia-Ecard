using Wizardsgroup.Utilities.Security;

namespace Pfizer.Domain.Infrastructure
{
    internal class System : IRegistrator
    {
        public void Register(ISecurityRegistrator registrator)
        {
            registrator.Register(register => register.ForGroup("System")
                                                     .IncludeModule("Department", func => func.WithBasicFunction())
                                                     .IncludeModule("Employee", o => o.WithBasicFunction())
                                                     .IncludeModule("SystemSetting",func => func
                                                         .WithBasicFunction(o => o.Register(BasicFunction.Edit).Register(BasicFunction.View)))
                                                     .IncludeModule("UserGroup", func => func.WithBasicFunction())
                                                     .IncludeModule("UserGroupFunction",func=>func
                                                        .WithBasicFunction(o=>o.Register(BasicFunction.View).Register(BasicFunction.Delete))
                                                        .WithSpecialFunction(o => o.Register("AssignGroupFunction")))
                                                     .IncludeModule("User", func => func.WithBasicFunction())
                                                     .IncludeModule("UserGroupMap",func=>func
                                                        .WithBasicFunction(o=>o.Register(BasicFunction.View).Register(BasicFunction.Delete))
                                                        .WithSpecialFunction(o => o.Register("AssignUserGroup")))                                                    
                                                     .IncludeModule("Account",func=>func.WithBasicFunction(Function.ViewOnly)
                                                         .WithSpecialFunction(o=>o.Register("Logoff"))));
        }
    }
}

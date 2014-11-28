using Wizardsgroup.Utilities.Security;

namespace Pfizer.Domain.Infrastructure
{
    internal class MaintenanceRegistrator : IRegistrator
    {
        public void Register(ISecurityRegistrator registrator)
        {
            registrator.Register(register => register.ForGroup("Maintenance")
                .IncludeSubgroup("Product", module =>
                {
                    module.IncludeModule("Class", func => func.WithBasicFunction());
                    module.IncludeModule("CardType", func => func.WithBasicFunction());

                    module.IncludeModule("Product", func => func.WithBasicFunction());
                    module.IncludeModule("Dosage", func => func.WithBasicFunction());
                    module.IncludeModule("ConversionFactor", func => func.WithBasicFunction());
                    module.IncludeModule("SalesRetailPrice", func => func.WithBasicFunction());

                    module.IncludeModule("UnitOfMeasure", func => func.WithBasicFunction());

                    module.IncludeModule("ProgramPerCardType", func => func.WithBasicFunction());

                    module.IncludeModule("Program", func => func.WithBasicFunction());
                    module.IncludeModule("ProgramProductMapping", func => 
                        func.WithBasicFunction(o => o.Register(BasicFunction.View).Register(BasicFunction.Delete).Register(BasicFunction.Toggle))
                        .WithSpecialFunction(o=>o.Register("Assign")));
                    module.IncludeModule("CardPrefix", func => func.WithBasicFunction());
                })
                );
        }
    }
}

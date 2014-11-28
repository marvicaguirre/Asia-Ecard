using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wizardsgroup.Utilities.Security;

namespace Wizardsgroup.Utilities.Tests
{
    [TestClass]
    public class SecurityRegistratorShould
    {
        [TestMethod]
        public void RegisterGroupModuleWithModules()
        {
            var securityRegister = new SecurityModuleRegistrator();

            securityRegister.Register(register =>
                {
                    register.ForGroup("Scheduling")
                            .IncludeModule("DeliverySchedule", o => o.WithBasicFunction(reg => reg.Register(BasicFunction.Create).Register(BasicFunction.View))
                                .WithSpecialFunction(reg => reg.Register("GenerateSchedule")))
                            .IncludeModule("TruckSchedule", o => o.WithBasicFunction(Function.All)
                                .WithSpecialFunction(specialFunc => specialFunc.Register("Generate")))
                            .IncludeModule("DriverInformation", o => o.WithBasicFunction(Function.ViewOnly)
                                .WithSpecialFunction(reg => reg.Register("OverrideSomethig"))
                                .WithBasicFunction(Function.All));

                    register.ForGroup("Forecasting")
                            .IncludeModule("Forecasting", functions => functions.WithBasicFunction().WithSpecialFunction(reg => reg.Register("Forecast")));

                    register.ForGroup("Administration")
                            .IncludeSubgroup("Reference", modules => modules.IncludeModule("User", reg => reg.WithBasicFunction(Function.All)))
                            .IncludeModule("UserGroup", reg => reg.WithBasicFunction(Function.All));


                });
            
            Assert.IsTrue(securityRegister.Container.Count == 6);
            securityRegister.Container.ForEach(o => Assert.IsTrue(o.Functions.Count > 0));
        }
    }
}


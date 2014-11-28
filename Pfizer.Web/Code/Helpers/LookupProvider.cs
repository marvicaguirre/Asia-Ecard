using Pfizer.Repository;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Service.Factories;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Web.Code.Helpers
{

    public sealed class LookupProvider : ILookupFactory
    {
        private readonly ILookupFactory _factory;
        
        public static LookupProvider Instance
        {
            get { return Singleton<LookupProvider>.Instance; }
        }

        private LookupProvider()
        {
            _factory  = new LookupFactory(ReflectionHelper.Instance, new UnitOfWorkWrapper(), "Pfizer.Service");
        }

        public ILookupFunction Create<TType>(string entityLookup)
        {
            return _factory.Create<TType>(entityLookup);
        }
    }

}
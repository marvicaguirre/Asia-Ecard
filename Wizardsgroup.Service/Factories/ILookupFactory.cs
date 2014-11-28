using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Service.Factories
{
    public interface ILookupFactory
    {
        ILookupFunction Create<TType>(string entityLookup);
    }
}
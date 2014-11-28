using System.Collections.Generic;

namespace Wizardsgroup.Utilities.Security
{
    public interface IModuleFunctionCollectionContainer
    {
        List<IModuleFunctionContainer> Container { get; }     
    }
}
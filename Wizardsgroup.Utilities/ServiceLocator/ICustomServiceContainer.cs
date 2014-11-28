using System.Collections.Generic;

namespace Wizardsgroup.Utilities.ServiceLocator
{
    internal interface ICustomServiceContainer
    {
        List<IServiceContainer> Container { get; }    
    }
}
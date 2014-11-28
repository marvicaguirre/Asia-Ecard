using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Utilities.ServiceLocator
{
    internal interface IServiceContainer
    {
        Type Key { get; set; }
        Expression Value { get; set; }
    }
}
using System.Linq.Expressions;

namespace RuleEngine.Interfaces
{
    internal interface IRuleBuilder
    {
        Expression Build();
    }
}
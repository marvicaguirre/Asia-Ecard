using RuleEngine.Enums;

namespace RuleEngine.Interfaces
{
    public interface IRule
    {
        object LeftNode { get; set; }        
        object RightNode { get; set; }
        RuleOperator Operator { get; }
        string ValidationMessage { get; set; }     
    }
}
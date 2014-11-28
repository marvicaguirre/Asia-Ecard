using RuleEngine.Enums;
using RuleEngine.Interfaces;

namespace RuleEngine.Rules
{
    public abstract class AbstractRule : IRule
    {        
        public object LeftNode { get; set; }
        public object RightNode { get; set; }
        public abstract RuleOperator Operator { get;}
        public string ValidationMessage { get; set; }
    }
}

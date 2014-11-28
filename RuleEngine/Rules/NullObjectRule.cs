using RuleEngine.Enums;
using RuleEngine.Interfaces;

namespace RuleEngine.Rules
{
    public class NullObjectRule : IRule
    {
        public NullObjectRule()
        {
            LeftNode = new object();
            RightNode = new object();
            ValidationMessage = string.Empty;
            Operator = RuleOperator.None;
        }
        public object LeftNode { get; set; }
        public object RightNode { get; set; }
        public RuleOperator Operator { get; private set; }
        public string ValidationMessage { get; set; }
    }
}

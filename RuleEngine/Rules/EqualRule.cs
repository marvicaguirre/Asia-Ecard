using RuleEngine.Attributes;
using RuleEngine.Enums;

namespace RuleEngine.Rules
{
    [RuleOperator(RuleOperator.Equal)]
    public class EqualRule : AbstractRule
    {
        public override RuleOperator Operator
        {
            get { return RuleOperator.Equal;}
        }
    }
}

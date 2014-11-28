using RuleEngine.Attributes;
using RuleEngine.Enums;

namespace RuleEngine.Rules
{
    [RuleOperator(RuleOperator.GreaterThan)]
    public class GreaterThanRule : AbstractRule
    {
        public override RuleOperator Operator
        {
            get { return RuleOperator.GreaterThan;}
        }
    }
}

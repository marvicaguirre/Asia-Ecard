using RuleEngine.Attributes;
using RuleEngine.Enums;

namespace RuleEngine.Rules
{
    [RuleOperator(RuleOperator.GreaterThanOrEqual)]
    public class GreaterThanOrEqualRule : AbstractRule
    {
        public override RuleOperator Operator
        {
            get { return RuleOperator.GreaterThanOrEqual; }
        }
    }
}

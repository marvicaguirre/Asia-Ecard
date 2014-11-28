using RuleEngine.Attributes;
using RuleEngine.Enums;

namespace RuleEngine.Rules
{
    [RuleOperator(RuleOperator.LessThanOrEqual)]
    public class LessThanOrEqualRule : AbstractRule
    {
        public override RuleOperator Operator
        {
            get { return RuleOperator.LessThanOrEqual; }
        }
    }
}

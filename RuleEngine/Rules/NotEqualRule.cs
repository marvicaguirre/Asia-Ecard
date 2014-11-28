using RuleEngine.Attributes;
using RuleEngine.Enums;

namespace RuleEngine.Rules
{
    [RuleOperator(RuleOperator.NotEqual)]
    public class NotEqualRule : AbstractRule
    {
        public override RuleOperator Operator
        {
            get { return RuleOperator.NotEqual; }
        }
    }
}

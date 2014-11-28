using RuleEngine.Attributes;
using RuleEngine.Enums;

namespace RuleEngine.Rules
{
    [RuleOperator(RuleOperator.LessThan)]
    public class LessThanRule : AbstractRule
    {
        public override RuleOperator Operator
        {
            get { return RuleOperator.LessThan; }
        }
    }
}

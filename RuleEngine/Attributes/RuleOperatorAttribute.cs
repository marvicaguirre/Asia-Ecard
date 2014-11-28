using System;
using RuleEngine.Enums;

namespace RuleEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RuleOperatorAttribute : Attribute
    {
        #region Member
        public RuleOperator RuleOperator { get; private set; }

        #endregion

        #region Constructor

        public RuleOperatorAttribute(RuleOperator ruleOperator)
        {
            RuleOperator = ruleOperator;
        }
        #endregion
    }
}

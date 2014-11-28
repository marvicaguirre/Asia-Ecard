using System;
using System.Collections.Generic;
using System.Linq;
using RuleEngine.Enums;
using RuleEngine.Factories;
using RuleEngine.Interfaces;
using RuleEngine.Rules;

namespace RuleEngine
{
    internal class RuleDataConvert
    {
        #region Member
        private readonly RuleFactory _factory; 
        #endregion

        #region Constructor
        public RuleDataConvert()
        {
            _factory = new RuleFactory();
        } 
        #endregion

        public IRule Translate(RuleData ruleData)
        {
            if (ruleData == null)
                throw new ArgumentNullException("ruleData");

            RuleOperator ruleOperator;
            var rule = Enum.TryParse(ruleData.RuleOperator, false, out ruleOperator) ? _factory.Create(ruleOperator,ruleData.Field,ruleData.Value) : NullRule;
            rule.ValidationMessage = ruleData.ValidationMessage;
            return rule;
        }

        public IEnumerable<IRule> Translate(IEnumerable<RuleData> ruleData)
        {
            return ruleData.Select(Translate);
        }

        internal static NullObjectRule NullRule
        {
            get { return new NullObjectRule();}
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RuleEngine.Builders;
using RuleEngine.Interfaces;

namespace RuleEngine
{
    public class RuleEngine
    {           
        public CompiledRule<T> CompileRules<T>(IRule rule)
        {
            if (rule == null)
                throw new ArgumentNullException("rule");

            var param = Expression.Parameter(typeof(T));
            IRuleBuilder builder = new ConditionalRuleBuilder<T>(rule, param);
            var expression = builder.Build();

            if (expression == null)
                throw new Exception("Cannot construct rule.");
            
            var func = ((Expression<Func<T, bool>>)expression).Compile();

            var compiledRule = new CompiledRule<T> { Rule = func,Field = rule.LeftNode.ToString(), ValidationMessage = rule.ValidationMessage };
            return compiledRule;
        }

        public IEnumerable<CompiledRule<T>> CompileRules<T>(IEnumerable<IRule> rules)
        {
            return rules.Select(CompileRules<T>);
        }

        public ValidationResult Validate<T>(T value, CompiledRule<T> rule)
        {
            return Validate(value, new List<CompiledRule<T>> {rule});
        }

        public ValidationResult Validate<T>(T value, IEnumerable<CompiledRule<T>> rules)
        {
            IRuleValidator validator = new RuleValidator<T>(value, rules);
            return validator.Validate();
        }

        public IRule Convert(RuleData ruleData)
        {
            return Convert(new List<RuleData> {ruleData}).Single();
        }

        public IEnumerable<IRule> Convert(IEnumerable<RuleData> ruleData)
        {
            var ruleDataTranslator = new RuleDataConvert();
            return ruleDataTranslator.Translate(ruleData);
        }
    }
}

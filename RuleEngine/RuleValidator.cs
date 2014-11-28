using System;
using System.Collections.Generic;
using System.Linq;
using RuleEngine.Interfaces;

namespace RuleEngine
{
    internal class RuleValidator<T> : IRuleValidator
    {
        #region Members
        private readonly IEnumerable<CompiledRule<T>> _rules;
        private readonly T _parameterValue;        
        #endregion

        #region Constructor
        public RuleValidator(T parameterValue, CompiledRule<T> rule) : this(parameterValue, new List<CompiledRule<T>>{rule})
        {
            if (rule == null)
                throw new ArgumentNullException("rule");
        }

        public RuleValidator(T parameterValue, IEnumerable<CompiledRule<T>> rules)
        {
            if (parameterValue == null)
                throw new ArgumentNullException("parameterValue");
            if (rules == null)
                throw new ArgumentNullException("rules");
            _parameterValue = parameterValue;
            _rules = rules;
        } 
        #endregion

        public ValidationResult Validate()
        {
            return _ValidateRules();
        }

        private ValidationResult _InvokeRule(CompiledRule<T> rule,ValidationResult result)
        {
            var isPassed = rule.Rule.Invoke(_parameterValue);
            result.Passed = isPassed;
            result.ValidationMessageSummary = isPassed ? string.Empty : rule.ValidationMessage;
            result.ValidationDetails.Add(new ValidationDetail
                {
                    ValidationMessage = rule.ValidationMessage,
                    Field = rule.Field,
                    Passed = isPassed
                });
            return result;
        }

        private ValidationResult _ValidateRules()
        {
            var messages = string.Empty;
            var validationResult = new ValidationResult();
            messages = _rules.Select(o => _InvokeRule(o, validationResult))
                        .Where(resultOfRule => !resultOfRule.Passed)
                        .Aggregate(messages, (current, error) => current + ("\r\n" + error.ValidationMessageSummary));

            var validationSummary = string.Empty;

            if (messages.Length > 0)
                validationSummary = messages.Remove(0, 2);

            validationResult.Passed = messages.Length <= 0;
            validationResult.ValidationMessageSummary = validationSummary;

            return validationResult;
        }
    }
}

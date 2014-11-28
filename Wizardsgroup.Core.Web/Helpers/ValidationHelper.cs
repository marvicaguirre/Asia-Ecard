using System.Collections.Generic;
using System.Linq;
using RuleEngine;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Service;
using ValidationResult = RuleEngine.ValidationResult;

namespace Wizardsgroup.Core.Web.Helpers
{
    public class ValidationHelper<T>
    {
        #region Member
        private readonly RuleEngine.RuleEngine _engine;
        private readonly IEntityService<RuleDatastore> _entityService;
        #endregion

        #region Constructor
        public ValidationHelper(IUnitOfWork unitOfWork)
        {
            _engine = new RuleEngine.RuleEngine();
            _entityService = new RuleService(unitOfWork);
        }
        #endregion

        #region Public Functions/Methods
        public List<CompiledRule<T>> CompileRuleForControllerAndAction(string contoller, string action)
        {
            var rawRulesFromDbStore = _GetRawRulesFilterByControllerAndAction(contoller, action);
            var ruleEngineRuleData = _ConvertToConsumableRuleData(rawRulesFromDbStore);
            var compiledRules = _CompileRuleForControllerWithAction(ruleEngineRuleData);
            return compiledRules;
        }

        public ValidationResult Validate(T entityToValidate, IEnumerable<CompiledRule<T>> compiledRules)
        {
            var result = _engine.Validate(entityToValidate, compiledRules);
            return result;
        } 
        #endregion

        #region Private Functions/Methods
        private List<CompiledRule<T>> _CompileRuleForControllerWithAction(IEnumerable<RuleData> ruleData)
        {
            var rulesToCompile = _engine.Convert(ruleData);
            var compliledRules = _engine.CompileRules<T>(rulesToCompile);
            return compliledRules.ToList();
        }

        private IEnumerable<RuleDatastore> _GetRawRulesFilterByControllerAndAction(string controller, string action)
        {
            return _entityService.Filter(o => o.Controller == controller && o.ControllerAction == action).ToList();
        }

        private IEnumerable<RuleData> _ConvertToConsumableRuleData(IEnumerable<RuleDatastore> rawRulesFromDbStore)
        {
            var listOfRuleData = rawRulesFromDbStore
                .Select(o => new RuleData
                    {
                        Field = o.Field,
                        RuleOperator = o.RuleOperator,
                        Value = o.Value,
                        ValidationMessage = o.ValidationMessage
                    }).ToList();
            return listOfRuleData;
        } 
        #endregion
    }
}
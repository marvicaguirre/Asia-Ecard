using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RuleEngine.Attributes;
using RuleEngine.Enums;
using RuleEngine.Interfaces;
using RuleEngine.Rules;

namespace RuleEngine.Factories
{
    public class RuleFactory
    {
        #region Constants
        private const BindingFlags Bindingflags = BindingFlags.CreateInstance | BindingFlags.Public |
                              BindingFlags.Instance | BindingFlags.NonPublic;
        #endregion

        #region Create Instance

        public IRule Create(RuleOperator ruleOperator,object leftNode = null,object rightNode = null)
        {
            var ruleTypes = _SearchRuleTypes(_GetAssembly());
            var ruleToCreate = _GetRule(ruleOperator, ruleTypes);
            var instanceOfRuleType = _CreateInstanceOfRuleType(ruleToCreate,leftNode,rightNode);                            
            return instanceOfRuleType;
        }

        #endregion

        #region Private Function
        private IEnumerable<Type> _SearchRuleTypes(IEnumerable<Type> assemblyTypes)
        {
            var ruleTypes = (from typesInAssemblies in assemblyTypes
                                   where typesInAssemblies != null
                                   let type = typesInAssemblies
                                   where type.GetInterface("IRule") == typeof(IRule)
                                      && type.IsAbstract == false
                                   select type).ToList();
            return ruleTypes;
        }

        private Type _GetRule(RuleOperator ruleOperator, IEnumerable<Type> ruleTypes)
        {
            var ruleToCreate = (from ruleTypeToCreate in ruleTypes
                               let memberInfo = ruleTypeToCreate
                               let customAttribute =
                                   memberInfo.GetCustomAttributes(typeof (RuleOperatorAttribute), true).FirstOrDefault()
                               let attributeToCheck = customAttribute as RuleOperatorAttribute
                               where attributeToCheck != null && attributeToCheck.RuleOperator.Equals(ruleOperator)
                               select ruleTypeToCreate).FirstOrDefault();

            return ruleToCreate;
        }

        private IEnumerable<Type> _GetAssembly()
        {
            var assemblyName = "RuleEngine".ToLower();
            var assemblyTypes = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                 where assembly.FullName.ToLower().Contains(assemblyName)
                                 let assemblyToCheck = assembly
                                 from types in assembly.GetTypes()
                                 select types).ToList();
            return assemblyTypes;
        }

        private IRule _CreateInstanceOfRuleType(Type rule,object leftNode = null,object rightNode = null)
        {
            var instance = Activator.CreateInstance(rule, Bindingflags, null, null, null) as IRule;
            if (instance != null)
            {
                instance.LeftNode = leftNode;
                instance.RightNode = rightNode;
            }

            return instance;
        }
        #endregion
    }
}

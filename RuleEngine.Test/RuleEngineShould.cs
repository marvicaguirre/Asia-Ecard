using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuleEngine.Enums;
using RuleEngine.Factories;
using RuleEngine.Interfaces;
using RuleEngine.Rules;
using RuleEngine.Test.DummyClass;

namespace RuleEngine.Test
{
    [TestClass]
    public class RuleEngineShould
    {

        [TestMethod]
        public void CompileEqualRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.Equal);
            rule.LeftNode = "Name";
            rule.RightNode = "Faenor";
            rule.ValidationMessage = "Name should be equal to Faenor.";            

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);            

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileEqualRuleNullValue()
        {
            var rule = new RuleFactory().Create(RuleOperator.Equal);
            rule.LeftNode = "Name";
            rule.RightNode = null;
            rule.ValidationMessage = "Name should be equal to Faenor.";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompileEqualRuleNullField()
        {
            var rule = new RuleFactory().Create(RuleOperator.Equal);
            rule.LeftNode = null;
            rule.RightNode = "Faenor";
            rule.ValidationMessage = "Name should be equal to Faenor.";

            var engine = new RuleEngine();
            engine.CompileRules<Person>(rule);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompileEqualRuleNullFieldAndValue()
        {
            var rule = new RuleFactory().Create(RuleOperator.Equal);
            rule.LeftNode = null;
            rule.RightNode = null;
            rule.ValidationMessage = "Name should be equal to Faenor.";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileNotEqualRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.NotEqual);
            rule.LeftNode = "Name";
            rule.RightNode = "Faenor";
            rule.ValidationMessage = "Name should not be equal to Faenor.";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileNotEqualRuleNullValue()
        {
            var rule = new RuleFactory().Create(RuleOperator.NotEqual);
            rule.LeftNode = "Name";
            rule.RightNode = null;
            rule.ValidationMessage = "Name should not be equal to Faenor.";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileNotEqualRuleNullValueForDate()
        {
            var rule = new RuleFactory().Create(RuleOperator.NotEqual);
            rule.LeftNode = "BirthDate";
            rule.RightNode = null;
            rule.ValidationMessage = "Name should not be equal to Faenor.";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompileNotEqualRuleNullField()
        {
            var rule = new RuleFactory().Create(RuleOperator.NotEqual);
            rule.LeftNode = null;
            rule.RightNode = "Faenor";
            rule.ValidationMessage = "Name should not be equal to Faenor.";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileLessThanRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.LessThan);
            rule.LeftNode = "Age";
            rule.RightNode = 17;
            rule.ValidationMessage = "Legal age should be not less than 18";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileLessThanRuleNullValue()
        {
            var rule = new RuleFactory().Create(RuleOperator.LessThan);
            rule.LeftNode = "Age";
            rule.RightNode = null;
            rule.ValidationMessage = "Legal age should be not less than 18";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompileLessThanRuleNullField()
        {
            var rule = new RuleFactory().Create(RuleOperator.LessThan);
            rule.LeftNode = null;
            rule.RightNode = 17;
            rule.ValidationMessage = "Legal age should be not less than 18";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileLessThanOrEqualRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.LessThanOrEqual);
            rule.LeftNode = "Age";
            rule.RightNode = 18;
            rule.ValidationMessage = "Legal age should be not less than 18";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileLessThanOrEqualRuleNullValue()
        {
            var rule = new RuleFactory().Create(RuleOperator.LessThanOrEqual);
            rule.LeftNode = "Age";
            rule.RightNode = null;
            rule.ValidationMessage = "Legal age should be not less than 18";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompileLessThanOrEqualRuleNullField()
        {
            var rule = new RuleFactory().Create(RuleOperator.LessThanOrEqual);
            rule.LeftNode = null;
            rule.RightNode = 18;
            rule.ValidationMessage = "Legal age should be not less than 18";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileGreaterThanRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.GreaterThanOrEqual);
            rule.LeftNode = "Age";
            rule.RightNode = 18;
            rule.ValidationMessage = "Some";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileGreaterThanRuleNullValue()
        {
            var rule = new RuleFactory().Create(RuleOperator.GreaterThanOrEqual);
            rule.LeftNode = "Age";
            rule.RightNode = null;
            rule.ValidationMessage = "Some";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompileGreaterThanRuleNullField()
        {
            var rule = new RuleFactory().Create(RuleOperator.GreaterThanOrEqual);
            rule.LeftNode = null;
            rule.RightNode = 18;
            rule.ValidationMessage = "Some";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileGreaterThanOrEqualRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.GreaterThanOrEqual);
            rule.LeftNode = "Age";
            rule.RightNode = 18;
            rule.ValidationMessage = "Some";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void CompileGreaterThanOrEqualRuleNullValue()
        {
            var rule = new RuleFactory().Create(RuleOperator.GreaterThanOrEqual);
            rule.LeftNode = "Age";
            rule.RightNode = null;
            rule.ValidationMessage = "A message";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompileGreaterThanOrEqualRuleNullField()
        {
            var rule = new RuleFactory().Create(RuleOperator.GreaterThanOrEqual);
            rule.LeftNode = null;
            rule.RightNode = 18;
            rule.ValidationMessage = "Some";

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.IsNotNull(compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void ConvertNullObjectRuleData()
        {
            var ruleData = new RuleData
            {
                Field = "Name",
                Value = "Faenor",
                RuleOperator = "",
                ValidationMessage = "Name should be equal to Faenor."
            };

            var engine = new RuleEngine();
            var translatedRule = engine.Convert(ruleData);

            Assert.AreEqual(RuleOperator.None,translatedRule.Operator);
            Assert.AreEqual("Name should be equal to Faenor.", translatedRule.ValidationMessage);
        }

        [TestMethod]
        public void ConvertNullObjectRuleDataNullValue()
        {
            var ruleData = new RuleData
            {
                Field = "Name",
                Value = null,
                RuleOperator = "",
                ValidationMessage = "Name should be equal to Faenor."
            };

            var engine = new RuleEngine();
            var translatedRule = engine.Convert(ruleData);

            Assert.AreEqual(RuleOperator.None, translatedRule.Operator);
            Assert.AreEqual("Name should be equal to Faenor.", translatedRule.ValidationMessage);
        }

        [TestMethod]
        public void ConvertNullObjectRuleDataNullField()
        {
            var ruleData = new RuleData
            {
                Field = null,
                Value = "Faenor",
                RuleOperator = "",
                ValidationMessage = "Name should be equal to Faenor."
            };

            var engine = new RuleEngine();
            var translatedRule = engine.Convert(ruleData);

            Assert.AreEqual(RuleOperator.None, translatedRule.Operator);
            Assert.AreEqual("Name should be equal to Faenor.", translatedRule.ValidationMessage);
        }

        [TestMethod]
        public void ConvertNullObjectRuleDataNullFieldAndValue()
        {
            var ruleData = new RuleData
            {
                Field = null,
                Value = null,
                RuleOperator = "",
                ValidationMessage = "Name should be equal to Faenor."
            };

            var engine = new RuleEngine();
            var translatedRule = engine.Convert(ruleData);

            Assert.AreEqual(RuleOperator.None, translatedRule.Operator);
            Assert.AreEqual("Name should be equal to Faenor.", translatedRule.ValidationMessage);
        }

        [TestMethod]
        public void ConvertNullObjectRuleDataWithCompile()
        {
            var ruleData = new RuleData
            {
                Field = "Name",
                Value = "Faenor",
                RuleOperator = "",
                ValidationMessage = "Name should be equal to Faenor."
            };

            var engine = new RuleEngine();
            var translatedRule = engine.Convert(ruleData);
            var compiledRule = engine.CompileRules<Person>(translatedRule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.AreEqual("Name should be equal to Faenor.", compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void ExecuteConvertNullObjectRuleData()
        {
            var ruleData = new RuleData
            {
                Field = "Name",
                Value = "Faenor",
                RuleOperator = "",
                ValidationMessage = "Name should be equal to Faenor."
            };

            var person = new Person {Name = "Faenor"};

            var engine = new RuleEngine();
            var translatedRule = engine.Convert(ruleData);
            var compiledRule = engine.CompileRules<Person>(translatedRule);
            var validationResult = engine.Validate(person, compiledRule);

            Assert.IsTrue(validationResult.Passed);
            Assert.AreEqual(string.Empty, validationResult.ValidationMessageSummary);
        }

        [TestMethod]
        public void ConvertEqualRuleData()
        {
            var ruleData = new RuleData
            {
                Field = "Name",
                Value = "Faenor",
                RuleOperator = RuleOperator.Equal.ToString(),
                ValidationMessage = "Name should be equal to Faenor."
            };

            var engine = new RuleEngine();
            var translatedRule = engine.Convert(ruleData);

            Assert.AreEqual(ruleData.Field, translatedRule.LeftNode);
            Assert.AreEqual(ruleData.Value, translatedRule.RightNode);
            Assert.AreEqual(ruleData.ValidationMessage, translatedRule.ValidationMessage);
            Assert.AreEqual(RuleOperator.Equal,translatedRule.Operator);
            Assert.AreEqual(typeof(EqualRule),translatedRule.GetType());
        }

        [TestMethod]
        public void ConvertEqualRuleDataNullValue()
        {
            var ruleData = new RuleData
            {
                Field = "Name",
                Value = null,
                RuleOperator = RuleOperator.Equal.ToString(),
                ValidationMessage = "Name should be equal to Faenor."
            };

            var engine = new RuleEngine();
            var translatedRule = engine.Convert(ruleData);

            Assert.AreEqual(ruleData.Field, translatedRule.LeftNode);
            Assert.AreEqual(ruleData.Value, translatedRule.RightNode);
            Assert.AreEqual(ruleData.ValidationMessage, translatedRule.ValidationMessage);
            Assert.AreEqual(RuleOperator.Equal, translatedRule.Operator);
            Assert.AreEqual(typeof(EqualRule), translatedRule.GetType());
        }

        [TestMethod]
        public void ConvertEqualRuleDataAndCompile()
        {
            var ruleData = new RuleData
            {
                Field = "Name",
                Value = "Faenor",
                RuleOperator = RuleOperator.Equal.ToString(),
                ValidationMessage = "Name should be equal to Faenor."
            };

            var engine = new RuleEngine();
            var translatedRule = engine.Convert(ruleData);
            var compiledRule = engine.CompileRules<Person>(translatedRule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.AreEqual(ruleData.Field,translatedRule.LeftNode);
            Assert.AreEqual(ruleData.Value, translatedRule.RightNode);
            Assert.AreEqual(ruleData.ValidationMessage, translatedRule.ValidationMessage);
            Assert.AreEqual("Name should be equal to Faenor.", compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void ConvertEqualRuleDataAndCompileNullValue()
        {
            var ruleData = new RuleData
            {
                Field = "Name",
                Value = null,
                RuleOperator = RuleOperator.Equal.ToString(),
                ValidationMessage = "Name should be equal to Faenor."
            };

            var engine = new RuleEngine();
            var translatedRule = engine.Convert(ruleData);
            var compiledRule = engine.CompileRules<Person>(translatedRule);

            Assert.IsNotNull(compiledRule.Rule);
            Assert.AreEqual(ruleData.Field, translatedRule.LeftNode);
            Assert.AreEqual(ruleData.Value, translatedRule.RightNode);
            Assert.AreEqual(ruleData.ValidationMessage, translatedRule.ValidationMessage);
            Assert.AreEqual("Name should be equal to Faenor.", compiledRule.ValidationMessage);
        }

        [TestMethod]
        public void ExecuteEqualRuleOnFlatPropertyPassed()
        {
            var rule = new RuleFactory().Create(RuleOperator.Equal);
            rule.LeftNode = "Name";
            rule.RightNode = "Faenor";
            rule.ValidationMessage = "Name should be equal to Faenor.";

            var person = new Person {Name = "Faenor"};

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);
            var result = engine.Validate(person, compiledRule);

            Assert.AreEqual(true,result.Passed);
            Assert.AreEqual(0,result.ValidationMessageSummary.Length);
        }

        [TestMethod]
        public void ExecuteNotEqualRuleOnFlatNullablePropertyPassed()
        {
            var rule = new RuleFactory().Create(RuleOperator.NotEqual);
            rule.LeftNode = "BirthDate";
            rule.RightNode = null;
            rule.ValidationMessage = "Nullable Date must not be empty or null";

            var person = new Person { Name = "Faenor" };

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);
            var result = engine.Validate(person, compiledRule);

            Assert.AreEqual(false, result.Passed);
            Assert.AreEqual(0, result.ValidationMessageSummary.Length);
        }

        [TestMethod]
        public void ExecuteMutipleEqualRuleOnFlatPropertyPassed()
        {
            var rule = new RuleFactory().Create(RuleOperator.Equal);
            rule.LeftNode = "Name";
            rule.RightNode = "Faenor";
            rule.ValidationMessage = "Name should be equal to Faenor.";

            var rule2 = new RuleFactory().Create(RuleOperator.Equal);
            rule2.LeftNode = "Age";
            rule2.RightNode = 3000;
            rule2.ValidationMessage = "Age should be equal to 3000";

            var person = new Person { Name = "Faenor", Age = 3000};
            var rules = new List<IRule> {rule, rule2};
            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rules);
            var result = engine.Validate(person, compiledRule);

            Assert.AreEqual(true, result.Passed);
            Assert.AreEqual(0, result.ValidationMessageSummary.Length);
        }

        [TestMethod]
        public void ExecuteEqualRuleOnFlatPropertyFailed()
        {
            const string message = "Name should be equal to Faenor.";
            var rule = new RuleFactory().Create(RuleOperator.Equal);
            rule.LeftNode = "Name";
            rule.RightNode = "Faenor";
            rule.ValidationMessage = message;

            var person = new Person { Name = "Finwe" };

            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rule);
            var result = engine.Validate(person, compiledRule);

            Assert.AreEqual(false, result.Passed);
            Assert.AreNotEqual(0, result.ValidationMessageSummary.Length);
            Assert.AreEqual(message, result.ValidationMessageSummary);
        }

        [TestMethod]
        public void ExecuteMutipleEqualRuleOnFlatPropertyFailed()
        {
            var rule = new RuleFactory().Create(RuleOperator.Equal);
            rule.LeftNode = "Name";
            rule.RightNode = "Faenor";
            rule.ValidationMessage = "Name should be equal to Faenor.";

            var rule2 = new RuleFactory().Create(RuleOperator.Equal);
            rule2.LeftNode = "Age";
            rule2.RightNode = 3000;
            rule2.ValidationMessage = "Age should be equal to 3000";

            var person = new Person { Name = "Finwe", Age = 3001 };
            var rules = new List<IRule> { rule, rule2 };
            var engine = new RuleEngine();
            var compiledRule = engine.CompileRules<Person>(rules);
            var result = engine.Validate(person, compiledRule);

            Assert.AreEqual(false, result.Passed);
            Assert.AreNotEqual(0, result.ValidationMessageSummary.Length);
        }

    }
}

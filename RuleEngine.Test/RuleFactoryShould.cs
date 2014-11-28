using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuleEngine.Enums;
using RuleEngine.Factories;
using RuleEngine.Rules;

namespace RuleEngine.Test
{
    [TestClass]
    public class RuleFactoryShould
    {
        [TestMethod]
        public void CreateIntanceOfEqualRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.Equal);
            var type = rule as EqualRule;
            Assert.IsNotNull(type);
        }

        [TestMethod]
        public void CreateIntanceOfNotEqualRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.NotEqual);
            var type = rule as NotEqualRule;
            Assert.IsNotNull(type);
        }

        [TestMethod]
        public void CreateIntanceOfGreaterThanRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.GreaterThan);
            var type = rule as GreaterThanRule;
            Assert.IsNotNull(type);
        }

        [TestMethod]
        public void CreateIntanceOfGreaterThanOrEqualRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.GreaterThanOrEqual);
            var type = rule as GreaterThanOrEqualRule;
            Assert.IsNotNull(type);
        }

        [TestMethod]
        public void CreateIntanceOfLessThanRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.LessThan);
            var type = rule as LessThanRule;
            Assert.IsNotNull(type);
        }

        [TestMethod]
        public void CreateIntanceOfLessThanOrEqualRule()
        {
            var rule = new RuleFactory().Create(RuleOperator.LessThanOrEqual);
            var type = rule as LessThanOrEqualRule;
            Assert.IsNotNull(type);
        }
    }
}

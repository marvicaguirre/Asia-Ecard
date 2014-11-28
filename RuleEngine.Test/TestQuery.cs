using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuleEngine.Enums;

namespace RuleEngine.Test
{
    [TestClass]
    public class TestQuery
    {
        [TestMethod]
        public void TestMethod1()
        {
            var list = new List<RuleData>
                {
                    new RuleData
                        {
                            RuleOperator = RuleOperator.Equal.ToString(),
                            Field = "Name"
                        },
                        new RuleData
                            {
                                RuleOperator = RuleOperator.GreaterThan.ToString(),
                                Field = "Age"
                            },
                            new RuleData
                                {
                                    RuleOperator = RuleOperator.LessThan.ToString(),
                                    Field = "Address"
                                }
                };

            var result = list.OrderBy(o=>o);

            Assert.AreNotEqual(null,result);
        }
    }
}

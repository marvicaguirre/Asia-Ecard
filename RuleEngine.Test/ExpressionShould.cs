using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RuleEngine.Test
{
    [TestClass]
    public class ExpressionShould
    {
        [TestMethod]
        public void CanConvert()
        {
            Expression convertExpr = Expression.Convert(Expression.Constant(5.5),typeof (double));

            // The following statement first creates an expression tree, 
            // then compiles it, and then executes it.
            var compiled = Expression.Lambda<Func<double>>(convertExpr).Compile();
            var value = compiled.Invoke();
            Assert.AreEqual(5.5,value);
        }

        [TestMethod]
        public void CanChangeTypeConvert()
        {
            var convertExpr = Convert.ChangeType("5.5", typeof(double));

        }
    }
}

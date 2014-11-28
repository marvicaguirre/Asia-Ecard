using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuleEngine.Test.DummyClass;

namespace RuleEngine.Test
{
    [TestClass]
    public class ReflectionShould
    {
        [TestMethod]
        public void CheckNestedType()
        {
            var propertyToSearch = "Address.Street".Split('.');
            var person = new Person { Name = "Marvic", Address = new Address { Street = "Earth" } };

            string property = string.Empty;
            var types = person.GetType().GetProperties();
            foreach (var prop in propertyToSearch)
            {
                property = prop;
                //var xx = from type in types
                //         where type.type
            }
            PropertyInfo type = null;
            foreach (var type1 in types)
            {
                type = type1;
            }

            Assert.IsNull(null);            
        }


    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public class GuineaPig
        {
            public string Name { get; set; }
            public Guid Id { get; set; }
            public int IntProp { get; set; }
            public DateTime DateTimeProp { get; set; }
            public decimal DecimalProp { get; set; }
            public Guid? NullableId { get; set; }
            public int? NullableIntProp { get; set; }
            public DateTime? NullableDateTimeProp { get; set; }
            public decimal? NullableDecimalProp { get; set; }
        }

        [TestMethod]
        public void TestMethod0()
        {
            var guineaPig = new GuineaPig
            {
                Name = "TEST",Id = Guid.NewGuid(),DateTimeProp = DateTime.Now,IntProp = 1, DecimalProp = 100,
            };

            dynamic pigGuinea = guineaPig.ToDynamic();
            DynamicExtensions.CreateNewProperty(pigGuinea, "Id2", guineaPig.Id);
            Assert.AreEqual(pigGuinea.Id2, guineaPig.Id);
        }      
    }
}

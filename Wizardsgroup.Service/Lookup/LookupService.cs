using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Service.Factories;

namespace Wizardsgroup.Service.Lookup
{
    public class LookupService<T> : ILookupService<T> where T : class 
    {
        private readonly LookupDataFactory _factory;
        private readonly Func<T, int> _value;
        private readonly Func<T, string> _text;

        public LookupService(LookupDataFactory factory,Expression<Func<T, int>> value, Expression<Func<T, string>> text)
        {
            _factory = factory;
            _value = value.Compile();
            _text = text.Compile();
        }

        public IEnumerable<ILookupValueField> ConvertRecordToLookUp<TType>(IEnumerable<T> listOfRecords) where TType : class
        {
            var lookupCollection = listOfRecords.Select(CreateInstance<TType>);                
            return lookupCollection;
        }

        private ILookupValueField CreateInstance<TType>(T rawData) where TType : class 
        {
            var textValue = _text(rawData);
            var valueId = _value(rawData);
            var instanceType = _factory.Create(typeof(TType), textValue, valueId.ToString());            
            return instanceType;
        }
    }
}

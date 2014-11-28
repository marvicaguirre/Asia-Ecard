using System;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Interface;

namespace Wizardsgroup.Service.Factories
{
    public class LookupDataFactory
    {
        #region Members
        private readonly IReflection _helper;        
        #endregion

        #region Constructor
        public LookupDataFactory(IReflection helper)
        {            
            helper.Guard("ReflectionHelper must not be null.");
            _helper = helper;
        }
        #endregion

        public ILookupValueField Create(Type type, string value,string id)
        {
            var lookUp = _helper.CreateInstanceOfType<ILookupValueField>(type,value,id);
            return lookUp;
        }
    }
}

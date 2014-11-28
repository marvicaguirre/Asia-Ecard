using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Service.Attributes;
using Wizardsgroup.Service.Lookup;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Interface;

namespace Wizardsgroup.Service.Factories
{
    public class LookupFactory : ILookupFactory
    {        
        #region Members
        private readonly IReflection _helper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _assemblyName;

        #endregion

        #region Constructor
        public LookupFactory(IReflection helper, IUnitOfWork unitOfWork,string assemblyName)
        {
            assemblyName.Guard("AssemblyName must not be null");
            unitOfWork.Guard("UnitOfWork must not be null.");
            helper.Guard("ReflectionHelper must not be null.");

            _helper = helper;
            _unitOfWork = unitOfWork;
            _assemblyName = assemblyName;
        }
        #endregion

        #region Public Function

        public ILookupFunction Create<TType>(string entityLookup)
        {
            var assemblyTypes = _helper.GetTypesFromAssembly(_assemblyName);
            var types = _helper.GetTypesWithImplementingInterface<TType>(assemblyTypes);
            var typeToCreate = _GetTypeToInstantiate(entityLookup, types);
            var lookUp = _CreateInstanceOfLookUp(typeToCreate);
            return lookUp;
        }

        #endregion

        #region Private Function
        private Type _GetTypeToInstantiate(string entityLookup, IEnumerable<Type> lookUpTypes)
        {
            var typeToCreate = (from type in lookUpTypes
                                let memberInfo = type
                                let customAttribute =
                                    memberInfo.GetCustomAttributes(typeof(EntityLookupAttribute), true).FirstOrDefault()
                                let attributeToCheck = customAttribute as EntityLookupAttribute
                                where attributeToCheck != null && attributeToCheck.Entity.Equals(entityLookup)
                                select type).FirstOrDefault();

            return typeToCreate;
        }

        private ILookupFunction _CreateInstanceOfLookUp(Type type)
        {
            ILookupFunction instance;
            try
            {
                instance = _helper.CreateInstanceOfType<ILookupFunction>(type, _unitOfWork);
            }
            catch (Exception)
            {
                instance = new NullCustomLookup();
            }

            return instance;
        }
        #endregion
    }


}

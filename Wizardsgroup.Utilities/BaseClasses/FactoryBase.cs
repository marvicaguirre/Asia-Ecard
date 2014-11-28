using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wizardsgroup.Utilities.BaseClasses
{
    /// <summary>
    /// Ryan D. Lintag - 2013-08-29
    /// Note: The base constructor should receive 2 string paramaters namely
    /// 1. assemblyName - Full assembly name minus the interface name i.e. MIMS.Repository or MIMS.Utilities
    /// 2. interfaceName - The interface name i.e.  
    /// </summary>
    /// <typeparam name="TBase">This parameter should be an Interface type</typeparam>
    public abstract class FactoryBase<TBase> where TBase : class
    {
        #region constructor
        public FactoryBase(string assemblyName, string interfaceName)
        {
            _assemblyName = assemblyName;
            _interfaceName = interfaceName;
        }
        #endregion
        #region private constants and private properties
        private const BindingFlags Bindingflags = BindingFlags.CreateInstance | BindingFlags.Public |
                              BindingFlags.Instance | BindingFlags.NonPublic;
        private readonly string _assemblyName;
        private readonly string _interfaceName;
        #endregion
        #region implementation of IModelMapperRuleFactory
        public List<TBase> CreateInterfaceObjectList()
        {
            var interfaceObject = _SearchInterfaceObjectTypes();
            return interfaceObject.Select(_CreateIntanceOfRuleType).ToList();
        }
        #endregion
        #region abstract methods
        /// <summary>
        /// Ryan D. Lintag : 2013-08-29
        /// </summary>
        /// Note: This Abstract Method should be implemented and returned with  
        /// the function AppDomain.CurrentDomain.GetAssemblies() only
        /// <returns></returns>
        public abstract Assembly[] GetDomainAssembly();
        #endregion
        #region private functions
        private IEnumerable<Type> _SearchInterfaceObjectTypes()
        {
            var assemblyTypes = _GetInterfaceObjectAssemblyTypes();

            var interfaceObjects = (from typesInAssemblies in assemblyTypes
                             where typesInAssemblies != null
                             let type = typesInAssemblies
                             where type.BaseType == typeof(TBase)
                                && type.IsAbstract == false
                             select type).ToList();

            return interfaceObjects;
        }

        private IEnumerable<Type> _GetInterfaceObjectAssemblyTypes()
        {
            var assemblyTypes = (from assembly in GetDomainAssembly()
                                 where assembly.FullName
                                     .ToLower()
                                     .Contains(_assemblyName.ToLower())
                                 let assemblyToCheck = assembly
                                 from types in assembly.GetTypes()
                                 select types).ToList();
            return assemblyTypes;
        }

        private TBase _CreateIntanceOfRuleType(Type interfaceObject)
        {
            var instance = Activator.CreateInstance(interfaceObject, Bindingflags, null, null, null) as TBase;
            return instance;
        }
        #endregion
    }
    
}

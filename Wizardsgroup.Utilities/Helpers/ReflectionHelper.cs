using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wizardsgroup.Utilities.Interface;

namespace Wizardsgroup.Utilities.Helpers
{
    public sealed class ReflectionHelper : IReflection
    {
        #region Constants

        private const BindingFlags Propertyflags =
            BindingFlags.Default | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic |
            BindingFlags.Public | BindingFlags.SetProperty;

        private const BindingFlags Fieldflags =
            BindingFlags.Default | BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic |
            BindingFlags.Public | BindingFlags.SetField;

        private const BindingFlags ConstructorFlags = BindingFlags.CreateInstance | BindingFlags.Public |
                              BindingFlags.Instance | BindingFlags.NonPublic;
        #endregion

        #region Members
        private static volatile ReflectionHelper _instance = new ReflectionHelper();
        private static readonly object SyncLock = new object();
        #endregion

        #region Constructor
        private ReflectionHelper()
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static ReflectionHelper Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (SyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ReflectionHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Functions

        /// <summary>
        /// Gets the types from name space.
        /// </summary>
        /// <param name="assemblyName">The name space.</param>
        /// <returns></returns>
        public IEnumerable<Type> GetTypesFromAssembly(string assemblyName)
        {
            var name = assemblyName.ToLower();
            var assemblyTypes = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                 where assembly.FullName.ToLower().Contains(name)
                                 let assemblyToCheck = assembly
                                 from types in assembly.GetTypes()
                                 select types).ToList();
            return assemblyTypes;
        }

        public IEnumerable<Type> GetTypesWithImplementingInterface<T>(IEnumerable<Type> types)
        {            
            var typesImplementInterfaceT = (from typesInAssemblies in types
                                      where typesInAssemblies != null
                                      let type = typesInAssemblies
                                      where type.GetInterface(typeof(T).Name) == typeof(T)
                                         && type.IsAbstract == false
                                      select type).ToList();

            return typesImplementInterfaceT;
        }

        public T CreateInstanceOfType<T>(Type typeToCreate,params object[] parameter) where T : class
        {
            var instance = Activator.CreateInstance(typeToCreate, ConstructorFlags, null, parameter, null) as T;
            return instance;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        public Type GetType(IEnumerable<Type> types, string typeName)
        {
            var typeFromCollection = (from typesInAssemblies in types
                         where typesInAssemblies != null
                         let type = typesInAssemblies
                         where type.Name.ToLower().Equals(typeName.ToLower())
                         select type).FirstOrDefault();
            return typeFromCollection;
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="objectToReflect">The object to reflect.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetProperties(object objectToReflect, string propertyName = "")
        {
            return string.IsNullOrEmpty(propertyName)
                       ? objectToReflect.GetType().GetProperties(Propertyflags)
                       : objectToReflect.GetType().GetProperties(Propertyflags)
                             .Where(o => o.Name.ToLower().Equals(propertyName.ToLower()));
        }

        public void SetPropertyValue(object objectToReflect, string propertyName, object value)
        {
            objectToReflect.GetType().GetProperties(Propertyflags).Single(o => o.Name == propertyName).SetValue(objectToReflect, value);
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <param name="objectToReflect">The object to reflect.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public IEnumerable<FieldInfo> GetFields(object objectToReflect, string fieldName = "")
        {
            return string.IsNullOrEmpty(fieldName)
                       ? objectToReflect.GetType().GetFields(Fieldflags)
                       : objectToReflect.GetType().GetFields(Fieldflags)
                             .Where(o => o.Name.ToLower().Equals(fieldName.ToLower()));
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="objectToReflect">The object to reflect.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <returns></returns>
        public IEnumerable<EventInfo> GetEvents(object objectToReflect, string eventName = "")
        {
            return string.IsNullOrEmpty(eventName)
                    ? objectToReflect.GetType().GetEvents()
                    : objectToReflect.GetType().GetEvents()
                    .Where(o => o.Name.ToLower().Equals(eventName.ToLower()));
        }

        /// <summary>
        /// Gets the propety value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToReflect">The object to reflect.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public T GetPropetyValue<T>(object objectToReflect, string propertyName)
        {
            var propertyValue = GetProperties(objectToReflect, propertyName)
                                    .Where(prop => prop != null)
                                    .Select(prop => prop.GetValue(objectToReflect, null))
                                    .SingleOrDefault() ?? new object();

            return (T)propertyValue;
        }

        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public T GetFieldValue<T>(object entity, string fieldName)
        {
            var propertyValue = GetFields(entity, fieldName)
                .Where(prop => prop != null)
                .Select(prop => prop.GetValue(entity))
                .SingleOrDefault() ?? new object();
            return (T)propertyValue;
        }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="instanceOfMethodHolder">The instance of method holder.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="paramTypes">The param types.</param>
        /// <param name="paramValues">The param values.</param>
        /// <returns></returns>
        public object InvokeMethod(object instanceOfMethodHolder, string methodName, Type[] paramTypes = null, object[] paramValues = null)
        {
            Type instanceOfMethodHolderType = instanceOfMethodHolder.GetType();
            MethodInfo methodInfo = paramTypes == null
                                        ? instanceOfMethodHolderType.GetMethod(methodName)
                                        : instanceOfMethodHolderType.GetMethod(methodName, paramTypes);

            return methodInfo.Invoke(instanceOfMethodHolder, paramValues);
        }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="instanceOfMethodHolder">The instance of method holder.</param>
        /// <param name="methodInfo">The method info.</param>
        /// <param name="paramValues">The param values.</param>
        /// <returns></returns>
        public object InvokeMethod(object instanceOfMethodHolder, MethodInfo methodInfo,object[] paramValues = null)
        {
            return methodInfo.Invoke(instanceOfMethodHolder, paramValues);
        }

        /// <summary>
        /// Gets the type of the method from.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        public MethodInfo GetMethod<T>(string methodName)
        {
            return typeof (T).GetMethod(methodName);
        }

        /// <summary>
        /// Makes the generic method.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public MethodInfo MakeGenericMethod(MethodInfo methodInfo, params Type[] type)
        {
            return methodInfo.MakeGenericMethod(type); 
        }

        #endregion
    }
}

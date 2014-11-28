using System;
using System.Collections.Generic;
using System.Reflection;

namespace Wizardsgroup.Utilities.Interface
{
    public interface IReflection
    {
        /// <summary>
        /// Gets the types from name space.
        /// </summary>
        /// <param name="assemblyName">The name space.</param>
        /// <returns></returns>
        IEnumerable<Type> GetTypesFromAssembly(string assemblyName);

        IEnumerable<Type> GetTypesWithImplementingInterface<T>(IEnumerable<Type> types);
        T CreateInstanceOfType<T>(Type typeToCreate,params object[] parameter) where T : class;

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        Type GetType(IEnumerable<Type> types, string typeName);

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="objectToReflect">The object to reflect.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        IEnumerable<PropertyInfo> GetProperties(object objectToReflect, string propertyName = "");

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <param name="objectToReflect">The object to reflect.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        IEnumerable<FieldInfo> GetFields(object objectToReflect, string fieldName = "");

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="objectToReflect">The object to reflect.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <returns></returns>
        IEnumerable<EventInfo> GetEvents(object objectToReflect, string eventName = "");

        /// <summary>
        /// Gets the propety value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToReflect">The object to reflect.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        T GetPropetyValue<T>(object objectToReflect, string propertyName);


        void SetPropertyValue(object objectToReflect, string propertyName, object value);
        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        T GetFieldValue<T>(object entity, string fieldName);

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="instanceOfMethodHolder">The instance of method holder.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="paramTypes">The param types.</param>
        /// <param name="paramValues">The param values.</param>
        /// <returns></returns>
        object InvokeMethod(object instanceOfMethodHolder, string methodName, Type[] paramTypes = null, object[] paramValues = null);

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="instanceOfMethodHolder">The instance of method holder.</param>
        /// <param name="methodInfo">The method info.</param>
        /// <param name="paramValues">The param values.</param>
        /// <returns></returns>
        object InvokeMethod(object instanceOfMethodHolder, MethodInfo methodInfo,object[] paramValues = null);

        /// <summary>
        /// Gets the type of the method from.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        MethodInfo GetMethod<T>(string methodName);

        /// <summary>
        /// Makes the generic method.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        MethodInfo MakeGenericMethod(MethodInfo methodInfo, params Type[] type);
    }
}
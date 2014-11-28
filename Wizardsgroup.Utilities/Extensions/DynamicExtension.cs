using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Wizardsgroup.Utilities.Extensions
{
    public static class DynamicExtensions
    {
        public static dynamic CreateNewProperty(dynamic dynamicOject, string propertyName, object value = null)
        {
            var dataDictionary = (IDictionary<string, object>)dynamicOject;
            dataDictionary.Add(propertyName, value ?? string.Empty);
            return dynamicOject;
        }

        public static dynamic CreateNewProperty(dynamic dynamicOject, KeyValuePair<string, object> pair)
        {
            return CreateNewProperty(dynamicOject, pair.Key, pair.Value);
        }

        public static T FromDynamic<T>(this IDictionary<string, object> dictionary)
        {
            var bindings = new List<MemberBinding>();
            var writableProperties = typeof(T).GetProperties().Where(x => x.CanWrite).ToList();
            writableProperties.ForEach(sourceProperty =>
                {
                    var key = dictionary.Keys.SingleOrDefault(x => x.Equals(sourceProperty.Name, StringComparison.OrdinalIgnoreCase));
                    if (string.IsNullOrEmpty(key)) return;
                    var propertyValue = dictionary[key];
                    bindings.Add(Expression.Bind(sourceProperty, Expression.Constant(propertyValue)));
                });
            Expression memberInit = Expression.MemberInit(Expression.New(typeof(T)), bindings);
            return Expression.Lambda<Func<T>>(memberInit).Compile().Invoke();
        }

        public static dynamic ToDynamic<T>(this T obj, Dictionary<string, object> additionProperties = null)
        {
            var dynamicColumn = new PropertySetting { AdditionPropertyDictionary = additionProperties ?? new Dictionary<string, object>() };
            return ToDynamic(obj, dynamicColumn);
        }

        public static dynamic ToDynamic<T>(this T obj, PropertySetting setting)
        {
            return ConvertToDynamic(obj, setting, typeof(T).GetProperties);
        }

        public static dynamic ToDynamic<T>(this T obj, bool isObjectAnonymous)
        {
            return !isObjectAnonymous ? ToDynamic(obj) : ConvertToDynamic(obj, new PropertySetting(), obj.GetType().GetProperties);
        }
        public static IDictionary<string, object> ToDictionary(this object obj, bool isDynamic = false)
        {
            return isDynamic ? ExpandoToDictionary(obj as ExpandoObject) : ConvertToDictionary(obj, null, obj.GetType().GetProperties);
        }

        private static IDictionary<string, object> ExpandoToDictionary(ExpandoObject obj)
        {
            var keys = ((IDictionary<string, object>)obj).Keys.ToArray();
            var values = ((IDictionary<string, object>)obj).Values.ToArray();
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            for (var i = 0; i < keys.Count(); i++)
            {
                dictionary.Add(new KeyValuePair<string, object>(keys[i], values[i]));
            }
            return dictionary;
        }

        private static dynamic ConvertToDynamic<T>(T obj, PropertySetting setting, Func<PropertyInfo[]> getProperties)
        {
            var dictionary = ConvertToDictionary(obj, setting, getProperties);
            var expando = new ExpandoObject();
            dictionary.ForEach(item =>
            {
                expando = CreateNewProperty(expando as dynamic, item);
            });
            return expando;
        }

        //private static dynamic ConvertToDictionary<T>(T obj, PropertySetting setting, Func<PropertyInfo[]> getProperties)
        private static IDictionary<string, object> ConvertToDictionary<T>(T obj, PropertySetting setting, Func<PropertyInfo[]> getProperties)
        {
            IDictionary<string, object> expando = new Dictionary<string, object>();

            var properties = setting.Properties.Count == 0
                ? getProperties()
                : typeof(T).GetProperties().Where(o => setting.Properties.Contains(o.Name));

            var concurrentDictionary = new ConcurrentDictionary<string, object>();
            Parallel.ForEach(properties, propertyInfo =>
            {
                var propertyExpression = Expression.Property(Expression.Constant(obj), propertyInfo);
                var constant = (ConstantExpression)propertyExpression.Expression;
                var value = ((PropertyInfo)propertyExpression.Member).GetValue(constant.Value);
                concurrentDictionary.TryAdd(propertyInfo.Name, value != null ? value.ToString() : null);
            });
            concurrentDictionary.ToList().ForEach(expando.Add);
            //TODO check null
            setting.AdditionPropertyDictionary.ToList().ForEach(keyValuePair => CreateNewProperty(expando, keyValuePair));
            return expando;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;

namespace Wizardsgroup.Core.Web.Infrastructure
{
    
    public class ExpandoJsonConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var dictionary = (IDictionary<string, object>)obj;
            return dictionary.ToDictionary(item => item.Key, item => item.Value);
        }
        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new ReadOnlyCollection<Type>(new[] { typeof(System.Dynamic.ExpandoObject) });
            }
        }
    }
}
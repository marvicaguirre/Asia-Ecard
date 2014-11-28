using System;
using System.ComponentModel;

namespace Wizardsgroup.Utilities.Extensions
{
    internal class EnumGuard<T> : IEnumGuard<T>
    {
        private readonly T _enumToCheck;

        public EnumGuard(T enumToCheck)
        {            
            _enumToCheck = enumToCheck;
        }

        public IEnumGuard<T> CheckAndThrowNull()
        {
            if (_enumToCheck == null)
            {
                throw new ArgumentNullException(string.Format("value must not be null for {0}", typeof(T).Name));
            }
            return this;
        }

        public IEnumGuard<T> CheckAndThrowInvalidEnumArgument()
        {
            if (!Enum.IsDefined(typeof(T), _enumToCheck))
            {
                throw new InvalidEnumArgumentException(_enumToCheck.ToString(), _enumToCheck.ToInteger(), typeof(T));
            }
            return this;
        }

        public IEnumGuard<T> CheckAndThrowNotEnum()
        {
            if (!(typeof(T).IsEnum))
            {
                throw new ArgumentException(string.Format("{0} is not enum.",typeof(T).Name ));
            }
            return this;
        }        
    }
}

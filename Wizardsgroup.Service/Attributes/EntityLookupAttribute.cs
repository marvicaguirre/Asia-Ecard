using System;

namespace Wizardsgroup.Service.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityLookupAttribute : Attribute
    {
        #region Member
        public string Entity { get; private set; }

        #endregion

        #region Constructor

        public EntityLookupAttribute(string entity)
        {
            Entity = entity;
        }
        #endregion
    }
}

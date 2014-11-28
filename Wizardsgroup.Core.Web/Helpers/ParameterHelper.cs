using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wizardsgroup.Core.Web.Helpers
{
    public sealed class ParameterHelper
    {

        #region Members
        private static volatile ParameterHelper _instance = new ParameterHelper();
        private static readonly object SyncLock = new object();        
        #endregion

        #region Constructor
        private ParameterHelper()
        {            
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static ParameterHelper Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (SyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ParameterHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Public Function/Methods

        public string GetKey(HttpContextBase context,string key)
        {
            var result = GetParameter(context, key);
            return result.FieldValue;
        }

        public string GetParentId(HttpContextBase context)
        {
            var key = GetParameter(context, "ParentId");
            return key.FieldValue;
        }
        #endregion

        #region Private Function/Methods
        private CustomParameter GetParameter(HttpContextBase context, string parameterName)
        {
            var args = context.Request.QueryString["args"];
            var gridParameters = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CustomParameter>>(args);
            var gridParameter = gridParameters
                .FirstOrDefault(a => a.FieldName == parameterName) ??
                new CustomParameter
                {
                    FieldName = string.Empty,
                    FieldValue = string.Empty
                };
            return gridParameter;

        } 
        #endregion        
    }   
}
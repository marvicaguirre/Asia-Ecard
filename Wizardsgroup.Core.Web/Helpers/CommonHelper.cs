using System.Configuration;
using System.Web;

namespace Wizardsgroup.Core.Web.Helpers
{
    public sealed class CommonHelper
    {
        #region Variables
        private static volatile CommonHelper _instance = new CommonHelper();
        private static readonly object SyncLock = new object();
        #endregion

        #region Constructor
        private CommonHelper()
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static CommonHelper Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (SyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CommonHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Funtion

        public string VirtualDirectory()
        {
            return HttpContext.Current.Request.ApplicationPath;
        }

        public string ApplicationPath()
        {
            var request = HttpContext.Current.Request;
            string urlReferer = request.Url.Authority;
            if (request.UrlReferrer != null)
            {
                urlReferer = request.UrlReferrer.Authority;
            }

            var basePath = string.Format("{0}://{1}", request.Url.Scheme, urlReferer);
            //var vdir = VirtualDirectory();
            //return vdir == "/" ? basePath : basePath + vdir;
            return basePath;
        }

        public string GetNormalizedVirtualDir()
        {
            var vDir = VirtualDirectory();
            if (!string.IsNullOrEmpty(vDir) && vDir.Trim() != "/")
            {
                return vDir;
            }
            return "";
        }

        public bool DebugMode()
        {
            var setting = ConfigurationManager.AppSettings["DebugMode"];
            bool result;
            bool.TryParse(setting, out result);
            return result;
        }
        
        #endregion
    }
}
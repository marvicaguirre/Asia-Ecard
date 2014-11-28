using System.Web.Optimization;

namespace Pfizer.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            Wizardsgroup.Core.Web.BundleConfig.RegisterBundles(bundles);
        }
    }
}
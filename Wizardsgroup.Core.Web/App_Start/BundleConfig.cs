using System.Web.Optimization;

namespace Wizardsgroup.Core.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jqueryjs").Include(
            //            "~/Resources/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryjs").Include(
                        "~/Resources/Scripts/jquery-1.10.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymiscjs").Include(
                        "~/Resources/Scripts/jquery.dualListBox-1.3.js",
                        "~/Resources/Scripts/jquery.multiple.select.js"
                        ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryuijs").Include(
            //            "~/Resources/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryuijs").Include(
                       "~/Resources/Scripts/jquery-ui-1.10.3.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryuijs").Include(
            //           "~/Resources/Scripts/jquery-ui-1.9.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Resources/Scripts/jquery.unobtrusive*",
                        "~/Resources/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizrjs").Include(
                        "~/Resources/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/underscorejs").Include(
                        "~/Resources/Scripts/underscore.min*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Resources/bootstrap/js/bootstrap.min.js",
                    "~/Resources/Scripts/respond*"));


            //Kendo = requires jQuery by default so we include it here!
            bundles.Add(new ScriptBundle("~/bundles/kendojs").Include(
                        "~/Resources/Scripts/kendo/2014.1.528/kendo.all.min.js",
                        "~/Resources/Scripts/kendo/2014.1.528/kendo.aspnetmvc.min.js",
                        "~/Resources/Scripts/kendo.modernizr.custom.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/wizardsgroupjs").Include(
                        "~/Resources/Scripts/__wizardsgroup-common.js",
                        "~/Resources/Scripts/__wizardsgroup-confirm-record.js",
                        "~/Resources/Scripts/__wizardsgroup-create-record.js",
                        "~/Resources/Scripts/__wizardsgroup-custom-approval.js",
                        "~/Resources/Scripts/__wizardsgroup-customaction-popup.js",
                        "~/Resources/Scripts/__wizardsgroup-buttonAction.js",
                        "~/Resources/Scripts/__wizardsgroup-customComboBox.js",
                        "~/Resources/Scripts/__wizardsgroup-datepicker.js",
                        "~/Resources/Scripts/__wizardsgroup-delete-record.js",
                        "~/Resources/Scripts/__wizardsgroup-director.js",
                        "~/Resources/Scripts/__wizardsgroup-drop-record.js",
                        "~/Resources/Scripts/__wizardsgroup-dualListbox.js",
                        "~/Resources/Scripts/__wizardsgroup-dynamicTab.js",
                        "~/Resources/Scripts/__wizardsgroup-edit-record.js",
                        "~/Resources/Scripts/__wizardsgroup-error.js",
                        "~/Resources/Scripts/__wizardsgroup-general-controls.js",
                        "~/Resources/Scripts/__wizardsgroup-grid.js",
                         "~/Resources/Scripts/__wizardsgroup-gridAction.js",
                        "~/Resources/Scripts/__wizardsgroup-layout.js",
                        "~/Resources/Scripts/__wizardsgroup-linkedFor.js",
                        "~/Resources/Scripts/__wizardsgroup-module-layout.js",
                        "~/Resources/Scripts/__wizardsgroup-notification.js",
                        "~/Resources/Scripts/__wizardsgroup-popup.js",
                        "~/Resources/Scripts/__wizardsgroup-SingleFieldToMultiselect.js",
                        "~/Resources/Scripts/__wizardsgroup-selectModal.js",                        
                        "~/Resources/Scripts/__wizardsgroup-style.js",
                        "~/Resources/Scripts/__wizardsgroup-toggleStatus.js",
                        "~/Resources/Scripts/__wizardsgroup-view-details.js",
                        "~/Resources/Scripts/__wizardsgroup-entitySearchFilter.js",
                        "~/Resources/Scripts/__wizardsgroup-scroll-div.js",
                        "~/Resources/Scripts/__wizardsgroup-buttonShowAll.js",
                        "~/Resources/Scripts/__wizardsgroup-document.js",
                        "~/Resources/Scripts/__wizardsgroup-modal.js"
                ));




            //STYLES:

            bundles.Add(new StyleBundle("~/bundles/themecss").Include(
                        "~/Resources/Content/themes/base/jquery-ui.css",
                        "~/Resources/Content/themes/base/jquery.ui.core.css",
                        "~/Resources/Content/themes/base/jquery.ui.resizable.css",
                        "~/Resources/Content/themes/base/jquery.ui.selectable.css",
                        "~/Resources/Content/themes/base/jquery.ui.accordion.css",
                        "~/Resources/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Resources/Content/themes/base/jquery.ui.button.css",
                        "~/Resources/Content/themes/base/jquery.ui.dialog.css",
                        "~/Resources/Content/themes/base/jquery.ui.slider.css",
                        "~/Resources/Content/themes/base/jquery.ui.tabs.css",
                        "~/Resources/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Resources/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Resources/Content/themes/base/jquery.ui.theme.css"
                        ));

            bundles.Add(new StyleBundle("~/bundles/kendocss").Include(
                        "~/Resources/Content/kendo/2014.1.528/kendo.common.min.css",
                        "~/Resources/Content/kendo/2014.1.528/kendo.rtl.min.css",
                        "~/Resources/Content/kendo/2014.1.528/kendo.silver.min.css",
                        "~/Resources/Content/kendo/2014.1.528/kendo.silver.mobile.min.css",
                        "~/Resources/Content/kendo/2014.1.528/kendo.dataviz.min.css",
                        "~/Resources/Content/kendo/2014.1.528/kendo.dataviz.silver.min.css"));

            //"~/Resources/Content/kendo/2014.1.528/kendo.silver.min.css",
            //            "~/Resources/Content/kendo/2014.1.528/kendo.dataviz.silver.min.css"));
    //        <link href="http://cdn.kendostatic.com/2014.1.528/styles/kendo.common.min.css" rel="stylesheet" />
    //<link href="http://cdn.kendostatic.com/2014.1.528/styles/kendo.rtl.min.css" rel="stylesheet" />
    //<link href="http://cdn.kendostatic.com/2014.1.528/styles/kendo.silver.min.css" rel="stylesheet" />
    //<link href="http://cdn.kendostatic.com/2014.1.528/styles/kendo.silver.mobile.min.css" rel="stylesheet" />
    //<link href="http://cdn.kendostatic.com/2014.1.528/styles/kendo.dataviz.min.css" rel="stylesheet" />
    //<link href="http://cdn.kendostatic.com/2014.1.528/styles/kendo.dataviz.silver.min.css" rel="stylesheet" />

            bundles.Add(new StyleBundle("~/bundles/kendocommonbootstrapcss").Include(
                "~/Resources/Content/kendo/2014.1.528/kendo.common-bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/bundles/kendobootstrapcss").Include(
                "~/Resources/Content/kendo/2014.1.528/kendo.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapcss").Include(
                       "~/Resources/bootstrap/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/bundles/sitecss").Include("~/Resources/Content/site.css"
                    , "~/Resources/Content/notification.css"
                ));

            // Clear all items from the default ignore list to allow minified CSS and JavaScript files to be included in debug mode
            bundles.IgnoreList.Clear();

            // Add back some of the default ignore list rules
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }
    }
}
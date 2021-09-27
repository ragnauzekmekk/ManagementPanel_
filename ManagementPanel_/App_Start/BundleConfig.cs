using System.Web;
using System.Web.Optimization;

namespace ManagementPanel_
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Scripts/bundles/lib.vendor.bundle.js",
                      "~/Scripts/bundles/apexcharts.bundle.js",
                      "~/Scripts/bundles/counterup.bundle.js",
                      "~/Scripts/bundles/knobjs.bundle.js",
                      "~/Scripts/bundles/c3.bundle.js",
                      "~/Scripts/js/core.js",
                      "~/Scripts/js/page/project-index.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/modal.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/modal.css",
                      "~/Content/css/main.css",
                      "~/Content/css/theme1.css",
                      "~/Content/loading.css"));
        }
    }
}

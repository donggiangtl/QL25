using System.Web;
using System.Web.Optimization;

namespace QL25
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.7.1.js"));
            bundles.Add(new ScriptBundle("~/bundles/gtree").Include(
                        "~/Scripts/gtree/gtree.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
           
            //AjaxAdminLayout
            bundles.Add(new Bundle("~/bundles/AjaxAdminLayout").Include(
                "~/Scripts/MyScripts/AjaxAdminLayout.js"
                      ));
            //UserLayout
            bundles.Add(new Bundle("~/bundles/UserLayout").Include(
                "~/Scripts/MyScripts/UserLayout.js"
                      ));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.min.js"
                      ));

            bundles.Add(new Bundle("~/bundles/jstree").Include(
                      "~/Scripts/jsTree3/jstree.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/mycss/color.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/admin_css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/solid.min.css",
                      "~/Content/mycss/color.css",
                      "~/Content/mycss/admin_page.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/user_css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/solid.min.css",
                      "~/Content/mycss/color.css",
                      "~/Content/mycss/user_page.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jstree_css").Include(
                      "~/Content/jsTree/docs.css",
                      "~/Content/jsTree/themes/default/style.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/gtree").Include(            
                     "~/Content/gtree/gtree.css"));
        }
    }
}

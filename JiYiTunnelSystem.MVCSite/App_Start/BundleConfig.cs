using System.Web;
using System.Web.Optimization;

namespace JiYiTunnelSystem.MVCSite
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js", "~/Scripts/bootstrap-select.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css", "~/Content/bootstrap-select.css",
                      "~/Content/meanmenu.min.css", "~/Content/animate.css",
                      "~/Content/waves.min.css", "~/Content/button.css",
                      "~/Content/notika-custom-icon.css", "~/Content/responsive.css",
                      "~/Content/style.css"));
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/wow.min.js", "~/Scripts/jquery-price-slider.js",
                        "~/Scripts/jquery.scrollUp.min.js", "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/jquery.mCustomScrollbar.concat.min.js",
                        "~/Scripts/jquery.meanmenu.js", "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/dialog").Include("~/Content/sweetalert2.min.css", "~/Content/dialog.css"));
            bundles.Add(new ScriptBundle("~/bundles/dialog").Include(
                        "~/Scripts/core.js", "~/Scripts/sweetalert2.min.js"));

            bundles.Add(new ScriptBundle("~/bundle/highchart").Include(
                "~/Scripts/Highcharts-8.2.0/code/highcharts.js"));

            bundles.Add(new ScriptBundle("~/bundle/sensorpoint").Include("~/Scripts/sensorPoints.js"));

            bundles.Add(new StyleBundle("~/Content/login").Include("~/Content/login.css"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));
        }
    }
}

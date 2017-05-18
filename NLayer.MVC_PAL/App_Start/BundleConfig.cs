using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace NLayer.MVC_PAL
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-filestyle.js",
                      "~/Scripts/respond.js"));

            //Jssor Slider
            bundles.Add(new ScriptBundle("~/bundles/jssorSlider").Include(
                     "~/Scripts/jquery-1.11.3.min.js",
                     "~/Scripts/jssor.slider-23.1.6.mini.js",
                     "~/Scripts/LocalScripts/jssorSlider.js"));

            //fancybox
            bundles.Add(new ScriptBundle("~/bundles/fancybox").Include(
                     "~/fancybox/jquery.fancybox.js",
                     "~/Scripts/jquery-1.11.2.min.js",
                     "~/Scripts/bootstrap_3_3_2.min.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap_3_3_2.min.css",
                      "~/Content/site.css",
                      "~/fancybox/jquery.fancybox.css"
                      ));
        }
    }
}
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
                      "~/Scripts/respond.js"));

            //Galleria lib
            //bundles.Add(new ScriptBundle("~/bundles/galleria").Include(
            //            "~/Scripts/galleria-{version}.js",
            //            "~/galleria/themes/classic/galleria.classic.js",
            //            "~/Scripts/LocalScripts/WorkWithGalleria.js"));

            //Jssor Slider
            bundles.Add(new ScriptBundle("~/bundles/jssorSlider").Include(
                     "~/Scripts/jquery-1.11.3.min.js",
                     "~/Scripts/jssor.slider-23.1.6.mini.js",
                     "~/Scripts/LocalScripts/jssorSlider.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
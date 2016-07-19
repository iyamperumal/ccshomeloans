namespace CcsWeb
{
    using System;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js", new IItemTransform[0]));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*", new IItemTransform[0]));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*", new IItemTransform[0]));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(new string[] { "~/Scripts/bootstrap.js", "~/Scripts/respond.js" }));
            bundles.Add(new StyleBundle("~/Content/css").Include(new string[] { "~/Content/bootstrap.css", "~/Content/site.css" }));
            BundleTable.EnableOptimizations = true;
        }
    }
}


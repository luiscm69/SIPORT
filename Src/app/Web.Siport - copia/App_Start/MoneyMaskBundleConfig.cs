using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Web.Siport.App_Start.MoneyMaskBundleConfig), "RegisterBundles")]

namespace Web.Siport.App_Start
{
	public class MoneyMaskBundleConfig
	{
		public static void RegisterBundles()
		{
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/moneymask").Include("~/Scripts/jquery.moneymask.js"));
		}
	}
}
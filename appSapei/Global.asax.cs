using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace appSapei
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjI1MDUyQDMxMzcyZTM0MmUzMG4wa0wwKzVpekNnaFlWOStQQlUzVHF6eHluNnVLNDNGd1pCUUkvWGt2NGs9");//17.4.0.46
			//Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTczMDU5QDMxMzkyZTM0MmUzMGxSNTM5QlNnUHNZeUJLVC92VU82eVRub0ZBa2QvN2V1U0oxa1QrQ2plR2s9");
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			//SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
		}
		protected void Application_BeginRequest()
		{
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
			Response.Cache.SetNoStore();
		}
	}
}

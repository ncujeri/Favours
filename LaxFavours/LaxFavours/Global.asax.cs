using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LaxFavours {

    public class MvcApplication : HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles);

            // Uncomment to use pre-17.2 routing for .Mvc() and .WebApi() data sources
            // DevExtreme.AspNet.Mvc.Compatibility.DataSource.UseLegacyRouting = true;
            // Uncomment to use pre-17.2 behavior for the "required" validation check
            // DevExtreme.AspNet.Mvc.Compatibility.Validation.IgnoreRequiredForBoolean = false;
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnableCrossDomain();
        }

        static void EnableCrossDomain()
        {
            string origin = HttpContext.Current.Request.Headers["Origin"];
            if (string.IsNullOrEmpty(origin)) return;
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", origin);
            string method = HttpContext.Current.Request.Headers["Access-Control-Request-Method"];
            if (!string.IsNullOrEmpty(method))
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", method);
            string headers = HttpContext.Current.Request.Headers["Access-Control-Request-Headers"];
            if (!string.IsNullOrEmpty(headers))
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", headers);
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.StatusCode = 204;
                HttpContext.Current.Response.End();
            }
        }
    }

}


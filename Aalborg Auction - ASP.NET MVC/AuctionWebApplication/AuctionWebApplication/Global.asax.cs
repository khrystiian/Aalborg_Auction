using AuctionWebApplication.AuctionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AuctionWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        public void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();

            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException))
            {
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                //if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                //    return;

                //Redirect HTTP errors to HttpError page
                Server.Transfer("HttpErrorPage.aspx");
            }

            // For other kinds of errors give the user some information
            // but stay on the default page
            Response.Write("<h2>Error</h2>\n");
            if (exc.Message.Contains("timed"))
            {
                Response.Write("The operation took longer than expected so it was aborted. Please go back");
            }
            if (exc.Message.Contains("endpoint"))
            {
                Response.Write("The service which you are trying to reach is ofline. Try restarting it or contacting your provider");
            } else  {
                Response.Write("You are either not logged in, have no restrictions for viewing the page, or the page does not exist");
            }
            IAuctionProjectService AccService = new AuctionProjectServiceClient("secure");
            AccService.LogError(exc);

            // Clear the error from the server
            Server.ClearError();
        }
    }
}

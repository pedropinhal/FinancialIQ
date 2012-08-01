using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using FinancialIQ.Code;
using FinancialIQ.Controllers;
using FinancialIQ.Infrastructure;

namespace FinancialIQ
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                "MonthlyFilter", // Route name
                "Money/MonthlyTotals", // URL with parameters
                new { controller = "Money", action = "MonthlyTotals" } // Parameter defaults
            );

            routes.MapRoute(
                "MoneyLog", // Route name
                "Money/MoneyLog/{year}/{month}", // URL with parameters
                new { controller = "Money", action = "MoneyLog" } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Money", action = "Home", id = UrlParameter.Optional } // Parameter defaults
            );

            

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie formsCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (formsCookie != null)
            {
                FormsAuthenticationTicket auth = FormsAuthentication.Decrypt(formsCookie.Value);

                var userId = Convert.ToInt32(auth.UserData);

                var principal = new CustomPrincipal(new CustomIdentity(auth.Name), userId);

                //var test = Roles.Provider.Name;
                //var principal = new CustomPrincipal(Roles.Provider.Name, new GenericIdentity(auth.Name), userID);

                Context.User = Thread.CurrentPrincipal = principal;
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }
}
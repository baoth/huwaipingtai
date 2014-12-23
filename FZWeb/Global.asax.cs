using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;

namespace FZ
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
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }
       
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            BusinessTemplate.ConfigBusinessTemplate.SetupResolveRules(builder);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_End(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Session_End");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Session_Start");
        }
    }
}
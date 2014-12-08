using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;

namespace huwaipingtai
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
            Tools.ConfigBusinessTemplate.SetupResolveRules(builder);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //CreateData.CreateTable();
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }

    public class CreateData
    {
        public static void CreateTable()
        {
            try
            {
                //var constring = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
                //var op = new QSmart.Core.DataBase.QSmartMySqlClient(constring);
                //op.CreateTable<ConsumerAddress>();

                //var constring = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
                //var op = new QSmart.Core.DataBase.QSmartMySqlClient(constring);
                //op.CreateTable<DataModel.Order.Cart>();

                //var constring = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
                //var op = new QSmart.Core.DataBase.QSmartMySqlClient(constring);
                //op.CreateTable<DataModel.Goods.GoodsShelves>(); 

                //CartView
               

                
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using BusinessOrder.CustomerAddress;

namespace Tools
{
    public class  ConfigBusinessTemplate
    {
        public static void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<OPCustomerAddress>().As<IOPCustomerAddress>();
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces();
        }
    }
}

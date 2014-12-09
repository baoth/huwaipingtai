using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using BusinessOrder.CAddress;
using BusinessOrder;

namespace BusinessTemplate
{
    public class  ConfigBusinessTemplate
    {
        /*注册业务模版 与接口对应关系*/
        public static void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<OPCustomerAddress>().As<IOPCustomerAddress>();
            builder.RegisterType<BusinessOrder.Cart.OPCart>().As<IBusinessOrder.Cart.IOPCart>();
            builder.RegisterType<BusinessOrder.Order.OPCustomerOrder>().As<IBusinessOrder.Order.IOPCustomerOrder>();
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces();
        }
    }
}

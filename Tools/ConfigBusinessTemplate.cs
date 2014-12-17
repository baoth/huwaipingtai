using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using BusinessOrder.CAddress;
using BusinessOrder;
using IBusinessOrder.Store;
using BusinessOrder.Order;
using BusinessOrder.User;
using IBusinessOrder.Goods;

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
            builder.RegisterType<BusinessOrder.Store.OPStore>().As<IOPStore>();
            builder.RegisterType<BusinessOrder.Goods.OPGoods>().As<IOPGoods>();
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces();
        }
        public static IBusinessOrder.User.ILogon GetILogon()
        {
            return new OPLogon();
        }
    }
}

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
using IBusinessOrder.CMS;

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
            builder.RegisterType<BusinessOrder.OPStore>().As<IOPStore>();
            builder.RegisterType<BusinessOrder.CMS.OPGoods>().As<IOPGoods>();
            builder.RegisterType<BusinessOrder.CMS.Publish>().As<IPublish>();
            builder.RegisterType<BusinessOrder.CMS.OPGoodsCatalog>().As<IBusinessOrder.CMS.IOPGoodsCatalog>();
            
        }
        public static IBusinessOrder.User.ILogon GetILogon()
        {
            return new OPLogon();
        }
    }
}

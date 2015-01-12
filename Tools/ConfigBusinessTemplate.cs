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
            builder.RegisterType<BusinessOrder.GoodsSize.OPGoodsSize>().As<IBusinessOrder.GoodsSize.IOPGoodsSize>();
            builder.RegisterType<BusinessOrder.YanSe.OPYanSe>().As<IBusinessOrder.YanSe.IOPYanSe>();
            builder.RegisterType<BusinessOrder.StorageIn.StorageIn>().As<IBusinessOrder.StorageIn.IStorageIn>();
            builder.RegisterType<BusinessOrder.SizeGroups.OPSizeGroups>().As<IBusinessOrder.SizeGroups.IOPSizeGroups>();
            builder.RegisterType<BusinessOrder.Shelves.OPShelves>().As<IBusinessOrder.Shelves.IOPShelves>();
            builder.RegisterType<BusinessOrder.MenDian.OPMenDian>().As<IBusinessOrder.MenDian.IOPMenDian>();
            
        }
        public static IBusinessOrder.User.ILogon GetILogon()
        {
            return new OPLogon();
        }
    }
}

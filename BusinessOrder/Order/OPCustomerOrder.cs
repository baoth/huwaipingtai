using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Order;
using DataModel.Order;

namespace BusinessOrder.Order
{
    public class OPCustomerOrder : IOPCustomerOrder
    {
        //提交客户订单
        public bool SubmitOrder(CustomerOrder customerOrder)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            //1、调用购物车查询所有这次提交要买的商品
            //2、获得商品库存状况
            //3、检查是否有库存不满足
            //4、保存订单
            var sqlOrder = string.Format("insert into ", "");
            return true;
        }
    }
}

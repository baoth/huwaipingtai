using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Order;
using DataModel.Order;
using BusinessOrder.Enum;
using DataModel.Goods;
using Common.Fun;
using Toolkit.CommonModel;
using IBusinessOrder.Cart;
using IBusinessOrder.Store;
using Toolkit.Fun;
using IBusinessOrder.User;
namespace BusinessOrder.Order
{
    public class OPCustomerOrder : IOPCustomerOrder
    {
        IOPCart iOpCat;
        IOPStore iOpStore;
        public OPCustomerOrder(IOPCart iopcatr,IOPStore iopstore) {
            //导入业务接口
            iOpCat = iopcatr;
            iOpStore = iopstore;
        }
        //提交客户订单
        public CResult SubmitOrder(CustomerOrder customerOrder)
        {

            var CResult = new CResult();
            //1、调用购物车查询所有这次提交要买的商品
            var products = iOpCat.CartActivedList(customerOrder.CustomerId);
            if (products.Count == 0) {
                return FunResult.GetError("亲，购物车中没有商品，不能生成订单呦！");
            }
            //2、获得商品库存状况
            var goodsCount = iOpStore.GetGoodsStore(products.Select(e => e.Sku).ToList());
            //3、检查是否有库存不满足 预留在这里吧  
            foreach (var item in goodsCount)
            {   
                //还有种情况是随然让你下订单，但是我需要通知商家补货。如不能补货 则取消订单。
                var product=products.FirstOrDefault(e => e.Sku==item.SKU);
                if (product != null) { //商品在库存中不存在 几乎不存在这种情况 前期测试为了严谨
                    if (item.StoreCount < product.Quantity) { 
                        //应该不能拆单 直接返回给客服 让客服看是否可以补货  然后产生订单

                    }
                }
            }
            //3.1检查库存不符合的
            //4、自动拆单过程 涉及门店优先级 涉及就近地点 就近仓库 

            //5、保存订单
            var listOrderReSql = new List<string>();
            //生成客户订单表
            var orderCustomerId = Guid.NewGuid();
            var sqlFormatCustomerOrder = @"insert into customerorder (
                    Id,CustomerId,CreateDate,SendDateIndex,
                    PhoneConfirm,AddressId,InvoiceTitleType,
                    InvoiceContentType,InvoiceCompany,PayType,DeliveryType
                ) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
            var sqlCustomerOrder = string.Format(sqlFormatCustomerOrder,
                    orderCustomerId,
                    customerOrder.CustomerId,
                    customerOrder.CreateDate,
                    customerOrder.SendDateIndex , 
                    customerOrder.PhoneConfirm?1:0, 
                    customerOrder.AddressId, 
                    customerOrder.InvoiceTitleType,
                    customerOrder.InvoiceContentType,
                    customerOrder.InvoiceCompany,
                    customerOrder.PayType,
                    customerOrder.DeliveryType);
            listOrderReSql.Add(sqlCustomerOrder);

            //生成订单表
            var orderId = Framework.GetOrderNum();
            var sqlOrderFormat = @"insert into `order`(Id,SubOrder,CustomerOrderId,Status) values ('{0}','{1}','{2}','{3}')";
            var sqlOrder = string.Format(sqlOrderFormat, orderId, 0, customerOrder.Id, (int)OrderStatusEnum.Generate);
            listOrderReSql.Add(sqlOrder);
            //生成订单商品表
            foreach (var goods in products)
            { 
                var sqlOrderGoodsFormat = @"insert into ordergoods(OrderId,Sku,Quantity) values ('{0}','{1}','{2}')";
                var sqlOrderGoods = string.Format(sqlOrderGoodsFormat, orderId, goods.Sku, goods.Quantity);
                listOrderReSql.Add(sqlOrderGoods);
            }
            var dbSession = Common.DbFactory.CreateDbSession();
            dbSession.Context.ExcuteNoQuery(listOrderReSql);
            return FunResult.GetSuccess();
        }
        /// <summary>
        /// 获取购物车中激活的商品
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<DataModel.View.CartView> GetActivedCarts(string customerId)
        {
            return iOpCat.CartActivedList(customerId);
        }


        public CResult VerifyEntity(CustomerOrder customerOrder)
        {
            var msg = "";
            if (customerOrder.AddressId == 0) {
                msg = "收货人地址为空不能提交订单！";
            }
            else if (string.IsNullOrEmpty(customerOrder.CustomerId)) {
                msg = "请重新登录！";
            }
            else if (customerOrder.PayType==0) {
                msg = "请选择付款方式！";
            }
            else if (customerOrder.InvoiceTitleType == 2) {
                if (string.IsNullOrEmpty(customerOrder.InvoiceCompany)) {
                    msg = "请填写发票的抬头";
                }
            }
            
            return string.IsNullOrEmpty(msg)?FunResult.GetSuccess(): FunResult.GetError(msg);
        }
    }

    
}

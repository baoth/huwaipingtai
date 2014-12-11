﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Order;
using DataModel.Order;
using BusinessOrder.Enum;
using DataModel.Goods;
using Common.Fun;
using Toolkit.CommonModel;
namespace BusinessOrder.Order
{
    public class OPCustomerOrder : IOPCustomerOrder
    {
        //提交客户订单
        public CResult SubmitOrder(CustomerOrder customerOrder)
        {

            var CResult = new CResult();
            //1、调用购物车查询所有这次提交要买的商品
            var opCart =new  BusinessOrder.Cart.OPCart();
            var products= opCart.CartActivedList(customerOrder.CustomerId);
            if (products.Count == 0) {
                CResult.IsSuccess = false;
                CResult.Msg = "亲，购物车中没有商品，不能生成订单呦！";
                return CResult;
            }
            //2、获得商品库存状况
            var opStore = new BusinessOrder.Store.OPStore();
            var goodsCount=  opStore.GetGoodsStore(products.Select(e => e.Sku).ToList());
            //3、检查是否有库存不满足 预留在这里吧  
            foreach (var item in goodsCount)
            {   
                //还有种情况是随然让你下订单，但是我需要通知商家补货。如不能补货 则取消订单。
                var product=products.FirstOrDefault(e => e.Sku==item.SKU);
                if (item.StoreCount < product.Quantity) { 
                    //应该不能拆单 直接返回给客服 让客服看是否可以补货  让后产生订单

                }
            }
            //4、自动拆单过程

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
            return CResult;
        }
    }
}

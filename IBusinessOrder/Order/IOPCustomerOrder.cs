using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Order;
using DataModel.View;
using Toolkit.CommonModel;

namespace IBusinessOrder.Order
{
    /// <summary>
    /// 客户订单操作类
    /// </summary>
    public interface IOPCustomerOrder
    {
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <returns></returns>
        CResultCode SubmitOrder(CustomerOrder customerOrder);
        /// <summary>
        /// 验证实体合法
        /// </summary>
        /// <param name="customerOrder"></param>
        /// <returns></returns>
        CResult VerifyEntity(CustomerOrder customerOrder);
        /// <summary>
        /// 获取购物车中激活的商品
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        List<DataModel.View.CartView> GetActivedCarts(string customerId);
        /// <summary>
        /// 获取客户对应的所有订单信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        List<DataModel.Order.OrderGoodsDto> GetUserAllOrderList(string customerId);
        /// <summary>
        /// 获取客户对应的所有订单信息
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="pageSize">分页长度</param>
        /// <param name="pageIndex">页码Index</param>
        /// <returns>OrderGoodsDto List</returns>
        List<DataModel.Order.OrderGoodsDto> GetUserAllOrderList(string customerId,int pageSize,int pageIndex);
        /// <summary>
        /// 获取订单清单列表
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns>List OrderGoodsDto</returns>
        List<DataModel.Order.OrderGoodsDto> GetOrderGoodsList(string customerId);


        /// <summary>
        /// 根据订单号 获取订单 给支付用
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        List<DataModel.Order.OrderGoodsPayDto> GetOrderById(int orderNo);

        /// <summary>
        /// 修改订单支付状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        bool UpdateOrderPayStatus(int orderNo);

    }
}

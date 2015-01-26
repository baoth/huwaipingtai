using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.CMS.Models;
using DataModel.Goods;
using DataModel.Order;
using DataModel.DeliverGoods;
using Toolkit.CommonModel;
namespace IBusinessOrder.DeliverGoods
{
    public interface IDeliverGoods
    {
       /// <summary>
       /// 获取发货的订单列表
       /// </summary>
       /// <returns></returns>
        List<OrderGoodsDto> GetDeliverGoodsList(int pageSize,int pageIndex);

        /// <summary>
        /// 根据订单Id获取收货人信息
        /// </summary>
        /// <returns></returns>
        OrderShiperDto GetShiperByOrderNo(int orderNo);

        /// <summary>
        /// 根据订单Id获取商品信息
        /// </summary>
        /// <returns></returns>
        List<DeliverOrderGoods> GetGoodsByOrderNo(int orderNo);

        CResult SetDeliver(ExpressDto expressDto);
    }
}

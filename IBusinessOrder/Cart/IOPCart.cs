using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Order;

namespace IBusinessOrder.Cart
{
    public interface  IOPCart
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cart">实体类</param>
        /// <param name="type">类型：1表示加入购物车 2表示立即购买</param>
        /// <returns></returns>
        bool Add(DataModel.Order.Cart cart,string type);
       
        bool Delete(int Id);
        List<DataModel.View.CartView> CartList(int customerId);

        /// <summary>
        /// 修改数量
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="productId">商品ID</param>
        /// <param name="quantity">数量</param>
       /// <returns>bool</returns>
       bool UpdateQuantity(string customerId, string productId,int quantity);
       /// <summary>
       /// 修改状态
       /// </summary>
       /// <param name="customerId"></param>
       /// <param name="productIds"></param>
       /// <returns></returns>
       bool UpdateActived(string customerId, List<string> productIds);
    }
}

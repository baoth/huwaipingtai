using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Order;

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
        bool SubmitOrder(CustomerOrder customerOrder);
        
    }
}

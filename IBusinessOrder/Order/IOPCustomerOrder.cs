using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Order;
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
        CResult SubmitOrder(CustomerOrder customerOrder);
        
    }
}

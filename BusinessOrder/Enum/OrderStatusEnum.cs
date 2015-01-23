using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BusinessOrder.Enum
{
    /// <summary>
    /// 单据操作状态
    /// </summary>
    public enum OrderStatusEnum
    {
       [Description("未付款")]
        NoPayment= 10,

       [Description("已取消")]
       Generate = 13,

       [Description("已付款")]
       Payment = 15,
    
       [Description("正在出库")]
       StoreBefore = 20,

       [Description("已发货")]
       DeliverGoods = 25,

       [Description("已完成")]
       Complete = 30
    }
}

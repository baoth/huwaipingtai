using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Order
{
   //客户订单收货人信息
   public class OrderShiperDto
   {
       /// <summary>
       /// 订单Id
       /// </summary>
       public int OrderId { get; set; }
       /// <summary>
       ///收货人姓名 
       /// </summary>
       public string ShiperName { get; set; }
       /// <summary>
       ///收货人电话
       /// </summary>
       public string ShiperPhone { get; set; }

       /// <summary>
       /// 收货人地址
       /// </summary>
       public string Address { get; set; }

       /// <summary>
       /// 收货人日期
       /// </summary>
       public string ShiperDate { get; set; }

   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DeliverGoods
{
    /// <summary>
    /// 快递单信息传输类
    /// </summary>
   public class ExpressDto
    {
       public string ExpressNo { get; set; }
       public string ExpressCompany { get; set; }
       public string Expressor { get; set; }
       public string ExpressPhone { get; set; }
       public string OrderId { get; set; }
    }
}

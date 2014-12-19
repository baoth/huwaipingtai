using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    public class StoreInDocDetail : QSmartEntity
    {
       [PrimaryKey]
       [AutoIncrement]
       public int Id { get; set; }
       /// <summary>
       /// 入库单Id
       /// </summary>
       public string StoreInDocId { get; set; }
       public int GoodsAllocationId { get; set; }
       /// <summary>
       /// 颜色Id
       /// </summary>
       public int ColorId { get; set; }
       /// <summary>
       /// 尺码Id
       /// </summary>
       public int SizeId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
       public int Quantity { get; set; }
       /// <summary>
       /// 价格
       /// </summary>
       public decimal Price { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
       public decimal SumMoney { get; set; }
    }
}

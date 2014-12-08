using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Order
{

    /// <summary>
    /// 商品订单明细
    /// </summary>
    public class OrderDetail : QSmartEntity
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public int Sku { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DeliverGoods
{
     //a.Id,
     //          a.OrderId,
     //          a.Sku,
     //          a.Quantity,
     //          a.Price,
     //          b.name as Color,
     //          c.name as Size,
     //          d.Description 
    /*发货时显示的商品信息*/
    public class DeliverOrderGoods 
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
    }
}

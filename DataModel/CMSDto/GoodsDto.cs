using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataModel.CMS.Models
{
    /// <summary>
    /// 商品传输类  为cms
    /// </summary>
    public class GoodsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JingXiaoShang { get; set; }
        public string MenDianName { get; set; }
        public int ColorId { get; set;}
        public int SizeId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string PrGoodsSKU { get; set; }
        public string DiapalyPrice { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string DonationDesc { get; set; }
        public string Ecoupons { get; set; }
        public string IsDispalyDonationDesc { get; set; }
        public string IsDispalyEcoupons { get; set; }
    }
}
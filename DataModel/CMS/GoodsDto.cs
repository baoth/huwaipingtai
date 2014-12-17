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
        public decimal Price { get; set; }
        public string DiapalyPrice { get; set; }
        public string Desc { get; set; }
        public string Brand { get; set; }
        public string DonationDesc { get; set; }
        public string Ecoupons { get; set; }
        public string IsDispalyDonationDesc { get; set; }
        public string IsDispalyEcoupons { get; set; }
    }
}
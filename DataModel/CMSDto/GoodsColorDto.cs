using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataModel.CMS.Models
{
    /// <summary>
    /// cms 商品颜色
    /// </summary>
    public class GoodsColorDto
    {
        public string Name { get; set; }
        public bool IsDefalut { get; set; }
        public string Id { get; set; }
    }
}
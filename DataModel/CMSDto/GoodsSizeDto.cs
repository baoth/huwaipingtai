using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataModel.CMS.Models
{
    /// <summary>
    /// 商品尺码
    /// </summary>
    public class GoodsSizeDto
    {
        public string Name { get; set; }
        public bool IsDefalut { get; set; }
        public int Id { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Goods
{
    /// <summary>
    /// 商品表(在用)
    /// </summary>
    public class Goods : QSmartEntity
    {
       [PrimaryKey]
       [AutoIncrement]
        public int Id { get; set; }
        public int SizeGroupsId { get; set; }
        public int ColorGroupsId { get; set; }
        /// <summary>
        /// 品牌商
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
      
    }
}

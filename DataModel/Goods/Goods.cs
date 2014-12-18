using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Goods
{
    public class Goods : QSmartEntity
    {
       [PrimaryKey]
       [AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public int GoodsCode { get; set; }
        /// <summary>
        /// 货品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int GoodsCatelogId { get; set; }
        /// <summary>
        /// 货品描述
        /// </summary>
        public string Describe { get; set; }
    }
}

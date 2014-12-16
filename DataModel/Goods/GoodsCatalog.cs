using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Goods
{
    /// <summary>
    /// 商品分类表
    /// </summary>
    public class GoodsCatalog : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>
        [StringMaxLength(2, VarCharType.nvarchar)]
        public string Code{get;set;}
        /// <summary>
        /// 分类层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级分类
        /// </summary>
        [StringMaxLength(2, VarCharType.nvarchar)]
        public string PCode { get; set; }
    }
}

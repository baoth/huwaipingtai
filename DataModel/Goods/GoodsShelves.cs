using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
namespace DataModel
{
    /// <summary>
    /// 商品上架表
    /// </summary>
    public class GoodsShelves : QSmartEntity
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        [PrimaryKey]
        public int Sku { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Description { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal Price { get; set; }
    }
}

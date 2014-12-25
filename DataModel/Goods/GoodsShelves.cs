using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
namespace DataModel
{
    /// <summary>
    /// 上架商品图册集
    /// </summary>
    public class ShangJia_ShangPin_TuCe
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public int ShangPinId { get; set; }
        [StringMaxLength(20, VarCharType.nvarchar)]
        public string ImgName { get; set; }
    }

    /// <summary>
    /// 详情页详情信息
    /// </summary>
    public class ShangJia_Sku_Info
    {
        [PrimaryKey]
        [StringMaxLength(30, VarCharType.nvarchar)]
        public string Sku { get; set; } //fenlei-shangpinid-yanse-chima
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ShangPinId { get; set; }
        public bool IsShangJia { get; set; }
    }

    /// <summary>
    /// 详情页图头
    /// </summary>
    public class ShangJia_Sku_TuTou
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [StringMaxLength(30, VarCharType.nvarchar)]
        public string Sku { get; set; }
        [StringMaxLength(20, VarCharType.nvarchar)]
        public string ImgName { get; set; }
    }
}

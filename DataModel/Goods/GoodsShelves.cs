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
    public class ShangJia_ShangPin_TuCe : QSmartEntity
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public int FeiLeiId { get; set; }
        public int ShangPinId { get; set; }
        public int MenDianId { get; set; }
        [StringMaxLength(20, VarCharType.nvarchar)]
        public string ImgName { get; set; }
    }

    /// <summary>
    /// 上架商品详情图片集
    /// </summary>
    public class ShangJia_ShangPin_DetailInfo : QSmartEntity
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public int FeiLeiId { get; set; }
        public int ShangPinId { get; set; }
        public int MenDianId { get; set; }
        [StringMaxLength(20, VarCharType.nvarchar)]
        //建议为GUID.Tostring()
        public string ImgName { get; set; }
        public int SortId { get; set; }
    }


    /// <summary>
    /// 详情页详情信息
    /// </summary>
    public class ShangJia_Sku_Info : QSmartEntity
    {
        [PrimaryKey]
        [StringMaxLength(30, VarCharType.nvarchar)]
        public string Sku { get; set; } //mendianId-fenleiId-shangpinid-yanse-chima
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ShangPinId { get; set; }
        public int FeiLeiId { get; set; }
        public int MenDianId { get; set; }
        public bool IsShangJia { get; set; }
    }

    /// <summary>
    /// 详情页图头
    /// </summary>
    public class ShangJia_Sku_TuTou : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [StringMaxLength(30, VarCharType.nvarchar)]
        public string ImgKey { get; set; } //mendianId-fenleiId-shangpinid-yanse
        [StringMaxLength(20, VarCharType.nvarchar)]
        public string ImgName { get; set; }
        /// <summary>
        /// 商品图册Id
        /// </summary>
        public int ShangJia_ShangPin_TuCeId { get; set; }
        /// <summary>
        /// 显示为默认值
        /// </summary>
        public bool IsDefault { set; get; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { set; get; }
    }
    /// <summary>
    /// 自定义模型（无表）
    /// </summary>
    public class ShangJia_ShangPin_Sku_TuTouDto
    {
        public int Id { get; set; }
        public int ShangPinId { get; set; }       
        public string ImgName { get; set; }
        public string ImgKey { get; set; }
        public int ShangJia_ShangPin_TuCeId { set; get; }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
namespace DataModel
{
    /// <summary>
    /// 商品分类表(在用)
    /// </summary>
    public class ShangPinFenLei : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>
        [StringMaxLength(2, VarCharType.nvarchar)]
        public string BianMa{get;set;}
        /// <summary>
        /// 分类层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
       [StringMaxLength(50, VarCharType.nvarchar)]
        public string Name { get; set; }
        /// <summary>
        /// 父级分类
        /// </summary>
        [StringMaxLength(2, VarCharType.nvarchar)]
        public string PBianMa { get; set; }
        /// <summary>
        /// 停用 启用
        /// </summary>
        public bool IsStop { get; set; }
    }
}

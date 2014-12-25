using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    /// <summary>
    /// 颜色组 品牌商维护（在用）
    /// </summary>
    public class YanSeZu : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int PinPaiId { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Name { get; set; }
    }
    /// <summary>
    /// 临时自定义模型
    /// </summary>
    public class SizeGroupsMainModel : QSmartEntity
    {
        public int Id { get; set; }
        public int PinPaiId { get; set; }
        public string Name { get; set; }
        public List<SizeGroupsDetailModel> detail { set; get; }
    }
    /// <summary>
    /// 临时自定义模型
    /// </summary>
    public class SizeGroupsDetailModel : QSmartEntity
    {
        public int Id { get; set; }
        public int ChiMaZuId { get; set; }
        public int ChiMaId { get; set; }
        public string SizeName { set; get; }
    }
}

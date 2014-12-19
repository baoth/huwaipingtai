using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.BaseInfo
{
    /// <summary>
    /// 尺码组（在用）
    /// </summary>
    public class SizeGroups : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int BrandId { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Name { get; set; }
    }
}

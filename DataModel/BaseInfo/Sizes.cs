using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    /// <summary>
    /// 尺码（在用）
    /// </summary>
    public class ChiMa : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
       public int Id { get; set; }
       [StringMaxLength(50, VarCharType.nvarchar)]
       public string Name { get; set; }
    }
}

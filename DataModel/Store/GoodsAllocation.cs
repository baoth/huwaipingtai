using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
  /// <summary>
  /// 货位 (在用)
  /// </summary>
    public class HuoWei : QSmartEntity
    {
      [PrimaryKey]
      [AutoIncrement]
      public int Id { get; set; }
      public int CangKuId { get; set; }
      public int PHuoWeiId { get; set; }
      [StringMaxLength(50,VarCharType.varchar)]
      public string Name { get; set; }
    }
}

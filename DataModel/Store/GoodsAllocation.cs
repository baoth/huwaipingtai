using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
  /// <summary>
  /// 商品货位 (在用)
  /// </summary>
    public class GoodsAllocation : QSmartEntity
    {
      [PrimaryKey]
      [AutoIncrement]
      public int Id { get; set; }
      public int StockId { get; set; }
      public int ParentId { get; set; }
      [StringMaxLength(50,VarCharType.varchar)]
      public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Order
{
    /// <summary>
    /// 客户地址表 
    /// </summary>
      public class CostomerAddress : QSmartEntity  
      {
          [AutoIncrement]
          [PrimaryKey]
          public int Id { get; set; }
          public int CustomerId { get; set; }
          [StringMaxLength(200, VarCharType.nvarchar)]
          public string DetailAddress { get; set; }
          [StringMaxLength(20, VarCharType.nvarchar)]
          public string City { get; set; }
          [StringMaxLength(50, VarCharType.nvarchar)]
          public string County { get; set; }
          [StringMaxLength(20, VarCharType.nvarchar)]
          public string Province { get; set; }
          [StringMaxLength(50, VarCharType.nvarchar)]
          public string Shipper { get; set; }
          [StringMaxLength(20, VarCharType.nvarchar)]
          public string ShipperPhone { get; set; }
          public bool Default { get; set; }
  }
}

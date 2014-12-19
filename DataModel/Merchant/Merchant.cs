using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Section
{
    /// <summary>
    /// 商家信息  （在用）
    /// </summary>
    public class Merchant : QSmartEntity
    {
       [PrimaryKey]
       [AutoIncrement]
       public int Id { get; set; }
       [StringMaxLength(50,VarCharType.nvarchar)]
       public string Name { get; set; }
       [StringMaxLength(150, VarCharType.nvarchar)]
       public string Address { get; set; }
       [StringMaxLength(50, VarCharType.nvarchar)]
       public string Personor { get; set; }
    }
}

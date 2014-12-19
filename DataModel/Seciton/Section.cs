using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    /// <summary>
    /// 门店表(在用)
    /// </summary>
   public class MenDian: QSmartEntity
    {
       [PrimaryKey]
       [AutoIncrement]
       public int Id { get; set; }
       /// <summary>
       /// 经销商表
       /// </summary>
       public int JingXiaoShangId { get; set; }
       /// <summary>
       /// 门店名称
       /// </summary>
       [StringMaxLength(50,VarCharType.varchar)]
       public string Name { get; set; }
       /// <summary>
       /// 地址
       /// </summary>
       [StringMaxLength(150, VarCharType.varchar)]
       public string Address { get; set; }
       /// <summary>
       /// 手机
       /// </summary>
       [StringMaxLength(20, VarCharType.varchar)]
       public string Phone { get; set; }
   }
}

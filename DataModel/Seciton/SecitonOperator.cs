using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    /// <summary>
    /// 操作员  （在用）
    /// </summary>
    public class CaoZuoYuan : QSmartEntity
    {

        [PrimaryKey]
        [AutoIncrement]
      public int Id { get; set; }
     [StringMaxLength(50, VarCharType.nvarchar)]
      public string LoginName { get; set; }
     [StringMaxLength(50, VarCharType.nvarchar)]
      public string Password { get; set; }
        [StringMaxLength(20, VarCharType.nvarchar)]
     public string Logo { get; set; }
    }
    /// <summary>
    /// 门店--操作员关联表
    /// </summary>
    public class MenDian_CanZuoYuan_GuanLian : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int CaoZuoYuanId { get; set; }
        public int MenDianId { get; set; }
    }

}

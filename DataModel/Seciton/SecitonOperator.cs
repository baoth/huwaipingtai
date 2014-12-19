using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    /// <summary>
    /// 门店操作表  （在用）
    /// </summary>
    public class SecitonOperator : QSmartEntity
    {

        [PrimaryKey]
        [AutoIncrement]
      public int Id { get; set; }

      public int SectionId { get; set; }
     [StringMaxLength(50, VarCharType.nvarchar)]
      public string LoginName { get; set; }
     [StringMaxLength(50, VarCharType.nvarchar)]
      public string Password { get; set; }

    }
}

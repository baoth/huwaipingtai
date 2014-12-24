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
        [StringMaxLength(20, VarCharType.nvarchar)]
        public string LoginName { get; set; }
        [StringMaxLength(20, VarCharType.nvarchar)]
        public string Password { get; set; }
        [StringMaxLength(20, VarCharType.nvarchar)]
        public string Logo { get; set; }
        [StringMaxLength(20, VarCharType.nvarchar)]
        public string NickName { get; set; }
    }

}

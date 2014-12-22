using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    /// <summary>
    /// 品牌表在用
    /// </summary>
    public class PinPai : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [StringMaxLength(50,VarCharType.nvarchar)]
        public string Name { get; set; }
        [StringMaxLength(150, VarCharType.nvarchar)]
        public string Logo { get; set;}
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string PinPaiShangId { get; set; }
    }
}

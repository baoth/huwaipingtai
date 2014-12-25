using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    public class RuKuMingXi : QSmartEntity
    {
       [PrimaryKey]
       [AutoIncrement]
       public int Id { get; set; }
       /// <summary>
       /// 入库单Id
       /// </summary>
       [StringMaxLength(50,VarCharType.varchar)]
       public string RuKuId { get; set; }
       public int HuoWeiId { get; set; }
       /// <summary>
       /// 颜色Id
       /// </summary>
       public int YanSeId { get; set; }
       /// <summary>
       /// 尺码Id
       /// </summary>
       public int ChiMaId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
       public int ShuLiang { get; set; }
       /// <summary>
       /// 价格
       /// </summary>
       public decimal DanJia { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
       public decimal JinEr { get; set; }
    }
}

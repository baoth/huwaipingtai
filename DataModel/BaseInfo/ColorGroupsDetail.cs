using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
   /// <summary>
   /// 颜色组明细（在用）
   /// </summary>
    public class YanSeZuMingXi : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
       public int Id { get; set; }
        public int YanSeZuId { get; set; }
       public int YanSeId { get; set; }
    }
}

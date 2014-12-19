using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Store
{
    public class StoreInDocDetail : QSmartEntity
    {
       [PrimaryKey]
       [AutoIncrement]
       public int Id { get; set; }
       /// <summary>
       /// 入库单Id
       /// </summary>
       public string StoreInDocId { get; set; }
       /// <summary>
       /// 颜色Id
       /// </summary>
       public int ColorId { get; set; }
       /// <summary>
       /// 尺码Id
       /// </summary>
       public int SizeId { get; set; }
       public int Count { get; set; }
    }
}

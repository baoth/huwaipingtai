using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{

    /// <summary>
    /// 尺码明细表 由品牌商维护（在用）
    /// </summary>
    public class SizeGroupsDetail : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
       public int Id { get; set; }
       public int SizesGroupsId { get; set; }
       public int SizesId { get; set; }
    }
}

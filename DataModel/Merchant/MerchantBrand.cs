using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Section
{
    /// <summary>
    /// 商家品牌关系表（在用）
    /// </summary>
    public class MerchantBrand : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
       public int Id { get; set; }
       public int BrandId { get; set; }
       public int MerchantId { get; set; }
    }
}

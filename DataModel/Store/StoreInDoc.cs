using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Store
{
    /// <summary>
    /// 入库单表
    /// </summary>
    public class StoreInDoc : QSmartEntity
    {
        /// <summary>
        ///主键 string 有子表方便存储关联
        /// </summary>
        [PrimaryKey]
        [StringMaxLength(32,VarCharType.varchar)] 
       public string Id { get; set; }
       /// <summary>
       /// 商家Id
       /// </summary>
       public int MerchantId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
       public int GoodsId { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
       public DateTime InDate { get; set; }
        /// <summary>
        /// 入库人
        /// </summary>
       public string InPersonor { get; set; }
      
    }
}

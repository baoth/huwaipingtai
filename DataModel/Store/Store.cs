using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{  
    /// <summary>
    /// 仓库 
    /// </summary>
    public class CangKu : QSmartEntity
    {
       [PrimaryKey]
       [AutoIncrement]
       public int Id { get; set; }
       /// <summary>
       /// 门店或者经销商Id
       /// </summary>
       public int MenDian { get; set; }
       /// <summary>
       /// 表示是门店还是经销商 
       /// </summary>
       public int LeiXing { get; set; }
       /// <summary>
       /// 仓库名称
       /// </summary>
       [StringMaxLength(50,VarCharType.varchar)]
       public string Name { get; set; }
        /// <summary>
        /// 仓库地址
        /// </summary>
       [StringMaxLength(150, VarCharType.varchar)]
       public string Address { get; set; }
        /// <summary>
        ///仓库管理员 
        /// </summary>
       [StringMaxLength(50, VarCharType.varchar)]
       public string GuanLiYuan { get; set; }
     
      

    }
}

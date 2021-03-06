﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    /// <summary>
    /// 入库单表
    /// </summary>
    public class RuKu : QSmartEntity
    {
        /// <summary>
        ///主键 string 有子表方便存储关联
        /// </summary>
        [PrimaryKey]
        [StringMaxLength(32,VarCharType.varchar)] 
       public string Id { get; set; }

       public int CangKuId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
       public int ShangPinId { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
       public DateTime RiQi { get; set; }
        /// <summary>
        /// 入库人
        /// </summary>
       public string RuKuRen { get; set; }
      
    }
}

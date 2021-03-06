﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
namespace DataModel.Order
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class Cart:QSmartEntity
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        [StringMaxLength(30,VarCharType.varchar)]
        public string Sku { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        [StringMaxLength(32, VarCharType.nvarchar)]
        public string CustomerId { get; set; }
        /// <summary>
        /// 是否被激活
        /// </summary>
        public bool Actived { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Quantity { get; set; }
    }
}

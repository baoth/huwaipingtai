﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.View
{
    public class CartView:QSmartEntity
    {        
        [PrimaryKey]
        public int Id { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public int Sku { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 是否被激活
        /// </summary>
        public bool Actived { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Quantity { get; set; }       
        /// <summary>
        /// 商品描述
        /// </summary>
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Description { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal Price { get; set; }

    }
}

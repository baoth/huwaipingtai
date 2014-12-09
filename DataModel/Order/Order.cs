﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
namespace DataModel.Order
{
    public class CustomerOrder : QSmartEntity
    {
        [PrimaryKey]
        public int Id { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 发货日期
        /// 0:1-7天
        /// 1:多1天9:00-15:00 2:多1天15:00-19:00 3:多1天19:00-22:00
        /// 4:多2天..........一次类推
        /// </summary>
        public short SendDateIndex { get; set; }
        /// <summary>
        /// 是否电话确认
        /// </summary>
        public bool PhoneConfirm { get; set; }
        /// <summary>
        /// 收货人信息（地址 电话 姓名)
        /// </summary>
        public int AddressId { get; set; }
        /// <summary>
        /// 发票抬头类型
        /// </summary>
        public short InvoiceTitleType { get; set; }
        /// <summary>
        /// 发票内容类型
        /// </summary>
        public short InvoiceContentType { get; set; }
        /// <summary>
        /// 发票抬头单位名称
        /// </summary>
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string InvoiceCompany { get; set; }
        /// <summary>
        /// 支付方式 
        /// </summary>
        public short PayType { get; set; }
        /// <summary>
        /// 送货方式
        /// </summary>
        public short DeliveryType { get; set; }
    }
    /// <summary>
    /// 商品订单
    /// </summary>
    public class Order:QSmartEntity
    {
        public int Id { get; set; }
        public bool SubOrder { get; set; }
        public int CustomerOrderId { get; set; }
        public short Status { get; set; }
    }

    public class OrderGoods : QSmartEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Sku { get; set; }
        public int Quantity { get; set; }
    }

}

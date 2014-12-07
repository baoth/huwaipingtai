using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
namespace DataModel.Order
{
    /// <summary>
    /// 商品订单
    /// </summary>
    public class Order:QSmartEntity
    {
        /// <summary>
        /// 订单号
        /// </summary>
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
        /// </summary>
        public string SendDate { get; set; }
        /// <summary>
        /// 是否电话确认
        /// </summary>
        public bool PhoneConfirm { get; set; }
        /// <summary>
        /// 收货人信息（地址 电话 姓名)
        /// </summary>
        public string CustInfo { get; set; }
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

}

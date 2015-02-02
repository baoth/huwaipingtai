using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
using System.ComponentModel;
namespace DataModel.Order
{
    public class CustomerOrder : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        [StringMaxLength(32, VarCharType.nvarchar)]
        public string CustomerId { get; set; }
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
        [PrimaryKey]
        [AutoIncrement]
        public Int64 Id { get; set; }
        public bool SubOrder       { get; set; }
        public int CustomerOrderId { get; set; }
        public short Status        { get; set; }
        /*为发货*/
        public string ExpressCompany{get;set;}
        public string Expresser    {get;set;}
        public string ExpressPhone {get;set;}
        /*快递公司  快递员 快递电话 快递单号 发货时间  */
        public string ExpressNum   {get;set;}
        public DateTime ExpressDateTime{get;set;}
      
    }

    public class OrderGoods : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public Int64 OrderId { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal Price { set; get; }
    }
    /// <summary>
    /// 临时实体
    /// </summary>
    public class OrderGoodsDto
    {       
        public Int64 Id { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>        
        public string Sku { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>        
        public string CustomerId { get; set; }       
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>        
        public string Description { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ShangPinId { set; get; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string ImgName { set; get; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgUrl { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { set; get; }
        /// <summary>
        /// 快递公司
        /// </summary>
        public string ExpressCompany { get; set; }
        /// <summary>
        /// 快递号
        /// </summary>
        public string ExpressNum { get; set; }

        public DateTime ExpressDateTime { get; set; }
    }

    ///订单下商品的实体类 为显示支付的body用
    public class OrderGoodsPayDto 
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
    /// <summary>
    /// 单据操作状态
    /// </summary>
    public enum OrderStatusEnum
    {
        [Description("未付款")]
        NoPayment = 10,

        [Description("已取消")]
        Generate = 13,

        [Description("已付款")]
        Payment = 15,

        [Description("正在出库")]
        StoreBefore = 20,

        [Description("已发货")]
        DeliverGoods = 25,

        [Description("已完成")]
        Complete = 30
    }
    
}

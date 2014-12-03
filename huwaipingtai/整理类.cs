using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace huwaipingtai
{
    public class 讨论过真实存在的类
    {
        public class Dealer//经销商
        {
            public int Id { get; set; }
            public string DealerCode { get; set; }
            public string DealerName { get; set; }
            public string DealerAddress { get; set; }
            public string Phone { get; set; }
            /// <summary>
            /// 传真
            /// </summary>
            public string Fax { get; set; }
            public string PostMail { get; set; }
            /// <summary>
            /// 执照信息
            /// </summary>
            public string License { get; set; }
            public string LegalPerson { get; set; }
        }
        public class Seciton//门店信息
        {
            public int Id { get; set; }
            public int DealerId { get; set; }
            public string SecitonCode { get; set; }
            public string SecitonName { get; set; }
            public string Address { get; set; }
            public string LinkMan { get; set; }
            public string Phone { get; set; }
        }
        public class GoodsUpDownMain//上架下架 
        {
            public int Id { get; set; }
            public int GoodsId { get; set; }
            public string  GoodsDescribe { get; set; }
            public string UpDate { get; set; }
            public string DownDate { get; set; }
            public int SecitonId { get; set; }
            /**/
        }
        public class GoodsUpDownDetail //上架下架详细信息
        {
            public int Id { get; set; }
            public int GoodsUpDownMainId { get; set; }
            public int SizeId{get;set;}
            public int Color{get;set;}
            public string CustomColor { get; set; }
            public string CustomSize { get; set; }
            /// <summary>
            ///上架数量
            /// </summary>
            public int Count { get; set; }
            /// <summary>
            /// 现价
            /// </summary>
            public decimal CurrentPrice { get; set; }
            /// <summary>
            /// 原价
            /// </summary>
            public decimal Price { get; set; }
            /// <summary>
            /// 会员价
            /// </summary>
            public decimal MemberPrice { get; set; }
            /// <summary>
            /// 唯一标识码 //同一商品，价格，尺码
            /// </summary>
            public string SKU { get; set; }
        }
        public class Account//账户
        {
            public int Id { get; set; }//Consumer
            public string LoginName { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class Consignee//收货人信息 
        {
            public int Id { get; set; }
            public int AccountId { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string County { get; set; }
            public string Mobile { get; set; }
            public string Tel { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public bool Default { get; set; }
        }
        public class GoodsColor// 颜色
        {
            public int Id { get; set; }
            /// <summary>
            /// 颜色编码
            /// </summary>
            public string ColorCode { get; set; }
            /// <summary>
            /// 颜色名称
            /// </summary>
            public string ColorName { get; set; }
            /// <summary>
            /// 颜色描述
            /// </summary>
            public string ColorDescribe { get; set; }
            public int StoreId { get; set; }
        }
        public class GoodsSize// 尺码
        {
            public int Id { get; set; }
            /// <summary>
            /// 尺码编码
            /// </summary>
            public string SizeCode { get; set; }
            /// <summary>
            /// 尺码名称
            /// </summary>
            public string SizeName { get; set; }
            /// <summary>
            /// 尺码描述
            /// </summary>
            public string SizeDescribe { get; set; }
            /// <summary>
            /// 经销商Id
            /// </summary>
            public int DealerId { get; set; }
        }
        public class GoodsSizeDetail// 货品尺码明细表 
        {
            public int Id { get; set; }
            /// <summary>
            /// 货品Id
            /// </summary>
            public int GoodsId { get; set; }
            /// <summary>
            /// 尺码Id
            /// </summary>
            public int SizeId { get; set; }
            /// <summary>
            /// 是否停用
            /// </summary>
            public bool IsStop { get; set; }
            /// <summary>
            /// 自定义颜色
            /// </summary>
            public string CustomSize { get; set; }
        }
        public class GoodsColorDetail// 货品颜色明细表 
        {
            public int Id { get; set; }
            /// <summary>
            /// 货品Id
            /// </summary>
            public int GoodsId { get; set; }
            /// <summary>
            ///颜色Id
            /// </summary>
            public int ColorId { get; set; }
            /// <summary>
            /// 是否停用
            /// </summary>
            public bool IsStop { get; set; }
            /// <summary>
            /// 自定义颜色
            /// </summary>
            public string CustomColor { get; set; }
        }
        public class GoodsType//货品分类
        {   //ProductType
            public int Id { get; set; }
            public int ParentId { get; set; }
            public string TypeCode { get; set; }
            public string TypeName { get; set; }
        }
        public class Goods//货品
        {
            public int Id { get; set; }
            /// <summary>
            ///经销商Id
            /// </summary>
            public string DealerId { get; set; }
            /// <summary>
            /// 货品代码
            /// </summary>
            public int GoodsCode { get; set; }
            /// <summary>
            /// 货品名称
            /// </summary>
            public string GoodsName { get; set; }
            /// <summary>
            /// 货品分类
            /// </summary>
            public int GoodsTypeId { get; set; }
            /// <summary>
            /// 货品描述
            /// </summary>
            public string ProductDescribe { get; set; }
         

        }
        public class Dic_FinalType// 结算方式 
        {
            public int Id { get; set; }
            public string ItemCode { get; set; }
            public string ItemValue { get; set; }
        }
        public class Dic_DespatchType//发货方式
        {
            public int Id { get; set; }
            public string ItemCode { get; set; }
            public string ItemValue { get; set; }
        }
        public class OrderBillDetail//订单子单
        {
            public int Id { get; set; }
            /// <summary>
            /// 商品Id
            /// </summary>
            public int GoodsId { get; set; }
            /// <summary>
            /// 商品数量
            /// </summary>
            public int GoodsCount { get; set; }
            /// <summary>
            /// 颜色Id
            /// </summary>
            public int ColorId { get; set; }
            /// <summary>
            /// 尺码Id
            /// </summary>
            public int SizeId { get; set; }

            /// <summary>
            /// 订单号
            /// </summary>
            public string OrderNum { get; set; }

            ///<summary>
            /// 销售价
            /// </summary>
            public decimal SellPrice { get; set; }

            ///<summary>
            /// 成交价
            /// </summary>
            public decimal Price { get; set; }

            /// <summary>
            /// 运费
            /// </summary>
            public decimal TransFee { get; set; }

            /// <summary>
            /// 确认金额
            /// </summary>
            public decimal SumbitPrice { get; set; }

            /// <summary>
            /// 消费者
            /// </summary>
            public int ConsumerId { get; set; }

            /// <summary>
            /// 消费者地址
            /// </summary>
            public string ConsumerAddress { get; set; }

            /// <summary>
            /// 消费者电话
            /// </summary>
            public string ConsumerPhone { get; set; }

            /// <summary>
            /// 联系人
            /// </summary>
            public string Linkman { get; set; }

            /// <summary>
            /// 邮编
            /// </summary>
            public int PostCode { get; set; }

            /// <summary>
            /// 城市
            /// </summary>
            public string City { get; set; }

            /// <summary>
            /// 区县
            /// </summary>
            public string County { get; set; }
            /// <summary>
            /// 操作员Id
            /// </summary>
            public int OperatorId { get; set; }

            /// <summary>
            /// 门店Id
            /// </summary>
            public int SectionId { get; set; }
            /// <summary>
            /// 自定义颜色
            /// </summary>
            public string CustomColor { get; set; }
            /// <summary>
            /// 自定义尺码
            /// </summary>
            public string CustomSize { get; set; }
        }
        public class OrderBill//订单主表
        {
            /// <summary>
            ///订单Id
            /// </summary>
            public int OrderId { get; set; }
            /// <summary>
            /// 销售单号
            /// </summary>
            public string OrderNum { get; set; }
            /// <summary>
            /// 订单日期
            /// </summary>
            public string OrderDate { get; set; }
            /// <summary>
            /// 结算状态
            /// </summary>
            public int FinalState { get; set; }
            /// <summary>
            /// 发货状态
            /// </summary>
            public int ShipperState { get; set; }
            /// <summary>
            ///结算方式 
            /// </summary>
            public int FinalType { get; set; }
            /// <summary>
            /// 发货方式
            /// </summary>
            /// </summary>
            public int ShipperType { get; set; }
            /// <summary>
            /// 订单状态
            /// </summary>
            public int OrderState { get; set; }
            /// <summary>
            /// 消费者Id
            /// </summary>
            public int ConsumerId { get; set; }
            /// <summary>
            /// 门店Id
            /// </summary>
            public int SecitonId { get; set; }

        }
        public class ShoppingCart//购物车 
        {
            public int Id { get; set; }
            /// <summary>
            /// 用户Id
            /// </summary>
            public int CustomerId { get; set; }
            /// <summary>
            /// 商品Id
            /// </summary>
            public int GoodsId { get; set; }
            /// <summary>
            /// 商品数量
            /// </summary>
            public int GoodsCount { get; set; }
            /// <summary>
            /// 颜色Id
            /// </summary>
            public int ColorId { get; set; }
            /// <summary>
            /// 尺码Id
            /// </summary>
            public int SizeId { get; set; }

            ///<summary>
            /// 销售价
            /// </summary>
            public decimal SellPrice { get; set; }

            ///<summary>
            /// 成交价
            /// </summary>
            public decimal Price { get; set; }
        }
    }
}
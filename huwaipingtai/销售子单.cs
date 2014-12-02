using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace huwaipingtai
{
    //价格 销售价 吊牌价 进货价 成交价
    public class 订单子单
    {
        public int Id { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public int ProductId{get;set;}
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductCount{get;set;}
        /// <summary>
        /// 颜色Id
        /// </summary>
        public int ColorId{get;set;}

        /// <summary>
        /// 
        /// </summary>
        public int SizeId{get;set;}

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
        //public decimal SellPrice { get; set; }

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
        public int StoreId { get; set; }
        /// <summary>
        /// 自定义颜色
        /// </summary>
        public string CustomColor { get; set; }
        /// <summary>
        /// 自定义尺码
        /// </summary>
        public string CustomSize { get; set; }
    }
    public class 订单主表 
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
        public int ConsumerId{get;set;}
        /// <summary>
        /// 门店Id
        /// </summary>
        public int StoreId { get; set; }
       
    }


    public class 货品
    {
        public int Id{get;set;}
        /// <summary>
        /// 货品代码
        /// </summary>
        public int ProductCode{get;set;}
        /// <summary>
        /// 货品名称
        /// </summary>
        public string ProductName{get;set;}
        /// <summary>
        /// 货品分类
        /// </summary>
        public int ProductType{get;set;}
        /// <summary>
        /// 货品描述
        /// </summary>
        public string ProductDescribe { get; set; }
        public string StoreId{get;set;}

    }
    public class 货品分类
    {   //ProductType
        public int Id { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductName { get; set; }
    }
    public class 颜色 
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
    public class 尺码
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
        public int StoreId { get; set; }
    }
    public class 货品颜色子表 
    {
        public int Id { get; set; }
        /// <summary>
        /// 货品Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 颜色Id
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
    public class 货品尺码名称 
    {
        public int Id { get; set; }
        /// <summary>
        /// 货品Id
        /// </summary>
        public int ProductId { get; set; }
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
    public class 网店//net
    { 
        
    }
    public class 门店信息 //(Store)
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
    }
    public class 经销商// （Dealer）
    {
        public int Id { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string DealerAddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PostMail { get; set; }
        public string License { get; set; }
        public string LegalPerson { get; set; }


    }
    public class 消费者 
    {
        public int Id{get;set;}//Consumer
        public string ConsumerCode { get; set; }
        public string ConsumerName { get; set; }
        public string Phone { get; set; }
        public string Phone1 { get; set; }
        public string DetailAddress { get; set; }
        public string City { get; set; }
        public string County { get; set; }

    }
    public class 操作员 
    {

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
namespace DataModel.Order
{
    /// <summary>
    /// 客户
    /// </summary>
    public class Customer : QSmartEntity          
    {
        [PrimaryKey]
        public string Id { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string RealName { get; set; }
        [StringMaxLength(20, VarCharType.nvarchar)]
        public string Phone { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string LoginName { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string NikeName { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Password { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Mail { get; set; }
    }

    /// <summary>
    /// 客户订单默认选项
    /// </summary>
    public class CustomerOrderSettings : QSmartEntity
    {
        [PrimaryKey]
        public int CustomerId { get; set; }
        
        private int _AddressId = 0;
        /// <summary>
        /// 地址
        /// </summary>
        public int AddressId
        {
            get { return _AddressId; }
            set { _AddressId = value; }
        }

        private int _PayType = 1;
        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayType
        {
            get { return _PayType; }
            set { _PayType = value; }
        }

        private int _DeliveryType = 1;
        /// <summary>
        /// 发货方式
        /// </summary>
        public int DeliveryType
        {
            get { return _DeliveryType; }
            set { _DeliveryType = value; }
        }

        private int _InvoiceTitleType = 1;
        /// <summary>
        /// 发票抬头类型
        /// </summary>
        public int InvoiceTitleType
        {
            get { return _InvoiceTitleType; }
            set { _InvoiceTitleType = value; }
        }

        private int _InvoiceContentType = 1;
        /// <summary>
        /// 发票内容类型
        /// </summary>
        public int InvoiceContentType
        {
            get { return _InvoiceContentType; }
            set { _InvoiceContentType = value; }
        }

        private string _InvoiceCompany = string.Empty;
        /// <summary>
        /// 发票单位
        /// </summary>
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string InvoiceCompany
        {
            get { return _InvoiceCompany; }
            set { _InvoiceCompany = value; }
        }
    }
}

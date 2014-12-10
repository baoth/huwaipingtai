using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Order;
namespace BusinessOrder
{
    /// <summary>
    /// 发货人地址
    /// </summary>
    public  interface  IOPCustomerAddress
    {
        /// <summary>
        /// 增加发货地址
        /// </summary>
        /// <param name="cusomerAddress"></param>
        /// <returns></returns>
        bool Add(CustomerAddress cusomerAddress);
        /// <summary>
        /// 修改发货地址
        /// </summary>
        /// <param name="cusomerAddress"></param>
        /// <returns></returns>
        bool Update(CustomerAddress cusomerAddress);
        /// <summary>
        /// 删除发货地址
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool Delete(int Id);
        /// <summary>
        /// 根据Id选择发货地址
        /// </summary>
        /// <param name="Id">地址Id</param>
        /// <returns></returns>
        CustomerAddress Select(int Id);
        /// <summary>
        /// 根据Id设置默认地址
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool SetDefault(int Id);
        /// <summary>
        /// 选择某一个客户所有发货地址
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        List<CustomerAddress> GetAll(string customerId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBusinessOrder.User
{
  public  interface ILogon
    {
       DataModel.Order.Customer Logon(string userName, string password);
       DataModel.Order.Customer RegUser(string validateCode, string phoneCode,string phone);
      /// <summary>
      /// 添加客户信息
      /// </summary>
      /// <param name="customer"></param>
      /// <returns></returns>
       bool Add(DataModel.Order.Customer customer);
      /// <summary>
      /// 根据微信ID 判断是否存在客户信息
      /// </summary>
      /// <param name="wxid"></param>
      /// <returns></returns>
       bool IsExist(string wxid);
      /// <summary>
      /// 根据微信ID获取客户信息
      /// </summary>
      /// <param name="wxid"></param>
      /// <returns></returns>
       DataModel.Order.Customer GetCustomerByWXID(string wxid);
    }
}

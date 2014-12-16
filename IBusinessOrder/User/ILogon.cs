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

    }
}

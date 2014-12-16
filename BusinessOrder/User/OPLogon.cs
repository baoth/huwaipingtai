using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.User;
namespace BusinessOrder.User
{
    public class OPLogon : ILogon
    {
        public DataModel.Order.Customer Logon(string userName, string password)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            string command = string.Format("select * from customer where LoginName='{0}' and password='{1}'",
               userName, password);
           var dt= dbSession.Context.QueryTable(command);
           var result = dbSession.Context.ConversionEntity<DataModel.Order.Customer>(dt);
           return result.Count == 0 ? null : result[0];
        }


        public DataModel.Order.Customer RegUser(string validateCode, string phoneCode, string phone)
        {
            throw new NotImplementedException();
        }
    }
}

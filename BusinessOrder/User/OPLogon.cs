using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.User;
using QSmart.Core.Object;
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


        public bool Add(DataModel.Order.Customer customer)
        {
            try
            {
                var dbSession = Common.DbFactory.CreateDbSession();
                dbSession.Context.InsertEntity(customer.CreateQSmartObject());
                dbSession.Context.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsExist(string wxid)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id from customer where WXId='{0}' ",wxid);
            var dt = dbSession.Context.QueryTable(sql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public DataModel.Order.Customer GetCustomerByWXID(string wxid)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select * from customer where WXId='{0}'",wxid);
            var dt = dbSession.Context.QueryTable(sql);
            if (dt.Rows.Count > 0)
            {
                var list = dbSession.Context.ConversionEntity<DataModel.Order.Customer>(dt);
                if (list != null && list.Count > 0)
                {
                    return list[0];
                }
                return null;
            }
            return null;
        }
    }
}

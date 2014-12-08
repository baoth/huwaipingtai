using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using QSmart.Core.Object;
using DataModel.Order;
namespace BusinessOrder.CAddress
{
   public  class OPCustomerAddress:IOPCustomerAddress
    {

        public bool Add(CustomerAddress cusomerAddress)
        {
            cusomerAddress.CreateDate = DateTime.Now;
            var dbSession = Common.DbFactory.CreateDbSession();
            dbSession.Context.InsertEntity(cusomerAddress.CreateQSmartObject());
            dbSession.Context.SaveChange();
            //实现业务逻辑和展现层 业务分离
            return true;
        }//18000 10000 28000+4000 18000+14000 36000

        public bool Update(CustomerAddress cusomerAddress)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            dbSession.Context.ModifyEntity(cusomerAddress.CreateQSmartObject());
            dbSession.Context.SaveChange();
            return true;
        }

        public bool Delete(int Id)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            dbSession.Context.DeleteEntity(dbSession.CreateDeleteCommand<CustomerAddress>(Id));
            dbSession.Context.SaveChange();
            return true;
        }


        public DataModel.Order.CustomerAddress Select(int Id)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            return dbSession.Retrieve<CustomerAddress>("Id", Id);
        }


        public List<CustomerAddress> GetAll()
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var dt = dbSession.Context.QueryTable("select * from customeraddress order by CreateDate"); ;
            return dbSession.Context.ConversionEntity<CustomerAddress>(dt);
        }
    }
}

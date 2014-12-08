using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using QSmart.Core.Object;
using DataModel.Order;
namespace BusinessOrder.CustomerAddress
{
   public  class OPCustomerAddress:IOPCustomerAddress
    {

        public bool Add(CostomerAddress cusomerAddress)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            dbSession.Context.InsertEntity(cusomerAddress.CreateQSmartObject());
            dbSession.Context.SaveChange();
            //实现业务逻辑和展现层 业务分离
            return true;
        }

        public bool Update(CostomerAddress cusomerAddress)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            dbSession.Context.ModifyEntity(cusomerAddress.CreateQSmartObject());
            dbSession.Context.SaveChange();
            return true;
        }

        public bool Delete(int Id)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            dbSession.Context.DeleteEntity(dbSession.CreateDeleteCommand<CostomerAddress>(Id));
            dbSession.Context.SaveChange();
            return true;
        }


        public DataModel.Order.CostomerAddress Select(int Id)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            return dbSession.Retrieve<CostomerAddress>("Id", Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessOrder.CustomerAddress
{
   public  class OPCustomerAddress:IOPCustomerAddress
    {

        public bool Add(DataModel.Order.ConsumerAddress cusomerAddress)
        {
            //实现业务逻辑和展现层 业务分离
            return false;
        }

        public bool Update(DataModel.Order.ConsumerAddress cusomerAddress)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }


        public DataModel.Order.ConsumerAddress Select(int Id)
        {
            throw new NotImplementedException();
        }
    }
}

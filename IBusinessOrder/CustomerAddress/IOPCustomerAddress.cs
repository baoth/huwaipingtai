using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Order;
namespace BusinessOrder.CustomerAddress
{
    public  interface  IOPCustomerAddress
    {
        bool Add(ConsumerAddress cusomerAddress);
        bool Update(ConsumerAddress cusomerAddress);
        bool Delete(int Id);
    }
}

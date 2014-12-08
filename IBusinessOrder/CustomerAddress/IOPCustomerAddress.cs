using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Order;
namespace BusinessOrder.CustomerAddress
{
    public  interface  IOPCustomerAddress
    {
        bool Add(CostomerAddress cusomerAddress);
        bool Update(CostomerAddress cusomerAddress);
        bool Delete(int Id);
        CostomerAddress Select(int Id);
    }
}

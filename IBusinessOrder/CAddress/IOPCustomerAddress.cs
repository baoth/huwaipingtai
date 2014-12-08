using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Order;
namespace BusinessOrder
{
    public  interface  IOPCustomerAddress
    {
        bool Add(CustomerAddress cusomerAddress);
        bool Update(CustomerAddress cusomerAddress);
        bool Delete(int Id);
        CustomerAddress Select(int Id);
        List<CustomerAddress> GetAll();
    }
}

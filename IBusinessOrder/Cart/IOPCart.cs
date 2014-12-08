using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Order;

namespace IBusinessOrder.Cart
{
    public interface  IOPCart
    {
        bool Add(DataModel.Order.Cart cart);
        bool Update(DataModel.Order.Cart cart);
        bool Delete(int Id);
        List<DataModel.View.CartView> CartList(int customerId);
    }
}

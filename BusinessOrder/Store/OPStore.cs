using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Store;
using DataModel;
namespace BusinessOrder
{
    public class OPStore : IOPStore
    {
        public List<SKUStore> GetGoodsStore(List<string> listSKU)
        {
           return new List<SKUStore>(){
                new SKUStore(){SKU="1-1-3-4-1",StoreCount=100},
                new SKUStore(){SKU="1000000012",StoreCount=100}
           };
        }
    }
}

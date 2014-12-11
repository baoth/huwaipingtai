using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Store;
using DataModel.Store;
namespace BusinessOrder.Store
{
    public class OPStore : IOPStore
    {
        public List<SKUStore> GetGoodsStore(List<int> listSKU)
        {
           return new List<SKUStore>(){
                new SKUStore(){SKU=1000000011,StoreCount=100},
                new SKUStore(){SKU=1000000012,StoreCount=100}
           };
        }
    }
}

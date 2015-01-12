using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;

namespace IBusinessOrder.Store
{
    public interface IOPStore
    {
        /// <summary>
        ///  根据SKU的集合 返回商品的库存
        /// </summary>
        /// <param name="listSKU">sku集合</param>
        /// <returns></returns>
        List<SKUStore> GetGoodsStore(List<string> listSKU);
    }
}

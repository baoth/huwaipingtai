using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;

namespace IBusinessOrder.Shelves
{
    public interface IOPShelves
    {
        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <returns></returns>
       List<PinPai> GetBrandList();
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        List<ShangPin> GetBrandList(int pingpaiid);
    }
}

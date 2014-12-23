using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;

namespace IBusinessOrder.StorageIn
{
   public interface IStorageIn
    {
       /// <summary>
       /// 获得商品的尺码
       /// </summary>
       /// <returns></returns>
       IList<ChiMa> GetSizeByGoodsId(string gid);

       /// <summary>
       /// 获得商品的颜色
       /// </summary>
       /// <returns></returns>
       IList<DataModel.YanSe> GetColorByGoodsId(string gid);

    }
}

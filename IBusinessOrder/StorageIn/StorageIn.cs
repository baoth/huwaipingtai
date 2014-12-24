using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;
using Toolkit.CommonModel;

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
       /// <summary>
       /// 获得货位
       /// </summary>
       /// <returns></returns>
       IList<DataModel.HuoWei> GetHuoWei();

       /// <summary>
       /// 保存入库单
       /// </summary>
       /// <param name="canKuId">仓库Id</param>
       /// <param name="mainId">主单Id</param>
       /// <param name="userName">用户名</param>
       /// <param name="listRuKuMingXiDto"></param>
       /// <returns></returns>
       CResult SaveStorageIn(string canKuId, string mainId, string userName, List<RuKuMingXiDto> listRuKuMingXiDto);
    }
}

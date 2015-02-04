using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.CMS.Models;
using DataModel.Goods;
using DataModel;

namespace IBusinessOrder.Goods
{
   public interface IOPGoods
    {
       /// <summary>
       /// 获取当前商品的模版
       /// </summary>
       /// <returns></returns>
       string GetGoodsCurTemplate(string goodsSKU);
       /// <summary>
       /// 根据sku获取商品尺码
       /// </summary>
       /// <param name="goodsSKU"></param>
       /// <returns></returns>
       IList<GoodsSizeDto> GetGoodsSize(string goodsSKU);
       /// <summary>
       /// 根据sku获取商品颜色
       /// </summary>
       /// <param name="goodsSKU"></param>
       /// <returns></returns>
       IList<GoodsColorDto> GetGoodsColor(string goodsSKU);
       /// <summary>
       /// 根据SKU获取商品信息
       /// </summary>
       /// <param name="goodsSKU"></param>
       /// <returns></returns>
      GoodsDto GetGoods(string goodsSKU);
      IList<ShangJia_ShangPin_DetailInfo> GetDetailImg(string goodsSKU);
    }
}

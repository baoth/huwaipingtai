using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.CMS.Models;

namespace IBusinessOrder.Goods
{
   public interface IOPGoods
    {
       /// <summary>
       /// 获取当前的模版
       /// </summary>
       /// <returns></returns>
       string GetGoodsCurTemplate();

       List<GoodsSizeDto> GetGoodsSize(string goodsSKU);
       List<GoodsColorDto> GetGoodsColor(string goodsSKU);
       List<GoodsDto> GetGoods(string goodsSKU);

    }
}

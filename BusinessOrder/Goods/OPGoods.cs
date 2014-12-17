using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Goods;

namespace BusinessOrder.Goods
{
    public class OPGoods : IOPGoods
    {
        public string GetGoodsCurTemplate()
        {
            throw new NotImplementedException();
        }

        public List<DataModel.CMS.Models.GoodsSizeDto> GetGoodsSize(string goodsSKU)
        {
            throw new NotImplementedException();
        }

        public List<DataModel.CMS.Models.GoodsColorDto> GetGoodsColor(string goodsSKU)
        {
            throw new NotImplementedException();
        }

        public List<DataModel.CMS.Models.GoodsDto> GetGoods(string goodsSKU)
        {
            throw new NotImplementedException();
        }
    }
}

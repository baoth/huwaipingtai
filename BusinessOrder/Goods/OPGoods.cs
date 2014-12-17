using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Goods;
using DataModel.CMS.Models;
using Toolkit.Ext;
namespace BusinessOrder.Goods
{
    public class OPGoods : IOPGoods
    {
        public string GetGoodsCurTemplate(string goodsSKU)
        {
            var path= Toolkit.Path.PathConfig.GetTemplatePath();
            var strTemplate = "mGoodsDetail.htm";
            return path.CombinePath(strTemplate);
        }
        public List<GoodsSizeDto> GetGoodsSize(string goodsSKU)
        {
            return new List<GoodsSizeDto> { 
                new GoodsSizeDto() {Id="",Name= "藏青色" },
                new GoodsSizeDto() {Id="",Name= "卡其色",IsDefalut=true },
                new GoodsSizeDto() {Id="",Name= "浅灰色" },
                new GoodsSizeDto() {Id="",Name= "深灰色" },
            };
        }

        public List<GoodsColorDto> GetGoodsColor(string goodsSKU)
        {
            return new List<GoodsColorDto> { 
                new GoodsColorDto() {Id="",Name= "M" },
                new GoodsColorDto() {Id="",Name= "L" },
                new GoodsColorDto() {Id="",Name= "XL" },
                new GoodsColorDto() {Id="",Name= "XXL" },
                new GoodsColorDto() {Id="",Name= "XXXL" },
                new GoodsColorDto() {Id="",Name= "4XL" },
                new GoodsColorDto() {Id="",Name= "5XL" } 
            };
        }

        public GoodsDto GetGoods(string goodsSKU)
        {
            var o = new GoodsDto()
            {
                DiapalyPrice = "¥3699.00",
                Price = 3699,
                Id = 1,
                Desc = "TZ.mall 2014 男装修身  格子 条纹 加绒 加厚 免烫保暖长袖衬衫 男 MS01藏青色 L",
                Brand = "TZ.mall",
                DonationDesc = "TZ.mall 秋冬保暖纯棉袜子 秋冬保暖必需品 赠品拍下不发货 随机发放颜色 袜子 均码 X  1",
                Ecoupons = ""

            };
            o.IsDispalyDonationDesc = string.IsNullOrEmpty(o.DonationDesc) ? "none" : "block";
            o.IsDispalyEcoupons = string.IsNullOrEmpty(o.Ecoupons) ? "none" : "block";
            return o;
        }
    }
}

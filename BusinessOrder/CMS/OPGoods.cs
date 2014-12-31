using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Goods;
using DataModel.CMS.Models;
using Toolkit.Ext;
namespace BusinessOrder.CMS
{
    public class OPGoods : IOPGoods
    {
        public string GetGoodsCurTemplate(string goodsSKU)
        {
            var path= Toolkit.Path.PathConfig.GetTemplatePath();
            var strTemplate = "mGoodsDetail.htm";
            return path.CombinePath(strTemplate);
        }
        public string GetGoodsGenerateFullPath(string fileName) 
        {
            var path = Toolkit.Path.PathConfig.GetGeneratePath();

            return path.CombinePath(fileName);
        }
        public IList<GoodsSizeDto> GetGoodsSize(string goodsSKU)
        {
            var arrIds = goodsSKU.Split('-');
            var colorId = arrIds[2];
            var sql =string.Format(@"select * from  yanse where Id in (
select SizesId from  sizegroupsdetail where SizesGroupsId in (
select ChiMaZuId from  shangpin where Id='{0}'))", colorId);
            var db  = Common.DbFactory.CreateDbSession();
            var dt  = db.Context.QueryTable(sql);
            return dt.ToList<GoodsSizeDto>();
        }

        public IList<GoodsColorDto> GetGoodsColor(string goodsSKU)
        {
            var arrIds = goodsSKU.Split('-');
            var colorId = arrIds[2];
            var sql = string.Format(@"select * from  yanse where Id in (
select YanSeId from  yansezumingxi where YanSeZuId in (
select YanSeZuId from  shangpin where Id='{0}'))", colorId);
            var db = Common.DbFactory.CreateDbSession();
            var dt = db.Context.QueryTable(sql);
            return dt.ToList<GoodsColorDto>();
        }

        public GoodsDto GetGoods(string goodsSKU)
        {
            var sql = @"
            select
                a.Name,
                b.Sku,
                b.Description,
                b.Price
                from  shangpin a
                left join shangjia_sku_info b on a.Id=b.ShangPinId
                where Sku='{0}'            
            ";
            var db = Common.DbFactory.CreateDbSession();
            var dt = db.Context.QueryTable(sql);
            var o1 = dt.ToList<GoodsDto>()[0]; 
            var o = new GoodsDto()
            {
                DiapalyPrice = "¥" + o1.Price,
                Price = o1.Price,
                Id = o1.Id,
                Desc =o1.Desc,//"TZ.mall 2014 男装修身  格子 条纹 加绒 加厚 免烫保暖长袖衬衫 男 MS01藏青色 L",
                Brand = o1.DiapalyPrice,//"TZ.mall",
                DonationDesc = o1.DonationDesc,//"TZ.mall 秋冬保暖纯棉袜子 秋冬保暖必需品 赠品拍下不发货 随机发放颜色 袜子 均码 X  1",
                Ecoupons = ""

            };
            o.IsDispalyDonationDesc = string.IsNullOrEmpty(o.DonationDesc) ? "none" : "block";
            o.IsDispalyEcoupons = string.IsNullOrEmpty(o.Ecoupons) ? "none" : "block";
            return o;
        }
    }
}

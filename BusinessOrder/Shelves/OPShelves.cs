using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Shelves;
using Toolkit.Ext;
using DataModel.Goods;
using System.Data;
namespace BusinessOrder.Shelves
{
    public class OPShelves:IOPShelves
    {
        public List<DataModel.PinPai> GetBrandList()
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Name,Logo,PinPaiShangId from PinPai ");
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<DataModel.PinPai>(dt);
        }
        public List<DataModel.ShangPin> GetProductList(int pingpaiid)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Name,ShangPinFenLeiId,ChiMaZuId,YanSeZuId,PinPaiId from ShangPin where pinpaiid='{0}'", pingpaiid);
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<DataModel.ShangPin>(dt);
        }
        public List<DataModel.ShangJia_ShangPin_Sku_TuTouDto> GetProductPhotoList(int shangpinid, string imgKey)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select a.Id,a.ShangPinId,a.ImgName,a.Id as ShangJia_ShangPin_TuCeId,b.ImgKey from ShangJia_ShangPin_TuCe as a left join (select c.Id,c.ShangJia_ShangPin_TuCeId, c.ImgKey,c.ImgName from ShangJia_Sku_TuTou as c where c.ImgKey='{1}')as b on a.id=b.ShangJia_ShangPin_TuCeId where a.ShangPinId='{0}' ", shangpinid, imgKey);
          
            var dt = dbSession.Context.QueryTable(sql);
            var list = dt.ToList<DataModel.ShangJia_ShangPin_Sku_TuTouDto>() as List<DataModel.ShangJia_ShangPin_Sku_TuTouDto>;
            return list;
        }
        public bool SaveShangJia_Sku_TuTou(List<DataModel.ShangJia_Sku_TuTou> list)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            List<string> sqlList = new List<string>();
            var sku = list[0].ImgKey;
            sqlList.Add("delete from ShangJia_Sku_TuTou where ImgKey='" + sku + "'");
            foreach(DataModel.ShangJia_Sku_TuTou item in list )
            {
                sqlList.Add(string.Format(" insert into ShangJia_Sku_TuTou(ShangJia_ShangPin_TuCeId,ImgKey,ImgName) values('{0}','{1}','{2}')", item.ShangJia_ShangPin_TuCeId, item.ImgKey, item.ImgName));
            }
            dbSession.Context.ExcuteNoQuery(sqlList);
            return true;
        }
        public bool DeleteShangJia_Sku_TuTou(string imgKey)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = "detete from ShangJia_Sku_TuTou where ImgKey='" + imgKey + "'";
            dbSession.Context.ExcuteNoQuery(sql);
            return true;
        }

        /// <summary>
        /// 获取商品信息为上架
        /// </summary>
        /// <param name="mendian">为拼接SKU</param>
        /// <returns></returns>
        public IList<GoodsShelvesDto> GetGoodsShelvesDto(string mendian,int goodsId)
        {
            string sql =string.Format( @"select 
                        a.ShangPinFenLeiId,
                        a.Id,
                        c.YanseId,
                        b.ChiMaId,
                        b.ChiMa,
                        c.Yanse,
                        a.Name,
                        a.PinPaiId,
                        d.name as PinPaiName
                        from shangpin a
                        left  join(select h.*,k.Name as chiMa from  ChiMazumingxi h left join ChiMa k on h.ChiMaId=k.id)   b on a.chimazuId=b.chimazuId  
                        left  join(select m.*,n.Name as yanSe from  yansezumingxi m left join Yanse n on m.YanseId=n.id)c on a.YanseZuId=c.yansezuId 
                        left  join pinPai d on d.Id=a.PinPaiId
                        where a.Id='{0}'
                        order by Id,yanseId,ChimaId",goodsId);
            var db = Common.DbFactory.CreateDbSession();
            DataTable dt= db.Context.QueryTable(sql);
            var ents=dt.ToList<GoodsShelvesDto>();
            foreach (var item in ents)
	        {
                item.SKU = mendian + "-" + item.ShangPinFenLeiId + "-" + item.Id + "-" + item.YanseId + "-" + item.ChiMaId;
	        }
            return ents;
        }


        public IList<GoodsShelvesDto> GetGoodsShelvesColor(IList<GoodsShelvesDto> goodsShelvesDto)
        {
           
            List<GoodsShelvesDto> listColor = new List<GoodsShelvesDto>();
            var colorSKU = "";
            foreach (var item in goodsShelvesDto)
            {
                var skuColor = item.SKU.Substring(0,item.SKU.LastIndexOf('-'));
             
                if (colorSKU != skuColor) {
                    colorSKU = skuColor;
                    var goods = new GoodsShelvesDto()
                    {
                        SKU=skuColor,
                        Id=item.Id,
                        PinPaiId=item.PinPaiId,
                        PinPaiName=item.PinPaiName,
                        YanseId=item.YanseId,
                        Yanse=item.Yanse,
                        ShangPinFenLeiId=item.ShangPinFenLeiId,
                        Name=item.Name
                    };
                    listColor.Add(goods);
                }
            }
            return listColor;
        }
    }
}

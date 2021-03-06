﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Shelves;
using Toolkit.Ext;
using DataModel.Goods;
using System.Data;
using Toolkit.CommonModel;
using Toolkit.Fun;
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
        public List<DataModel.ShangJia_ShangPin_Sku_TuTouDto> GetProductPhotoList(int shangpinid)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select distinct a.Id,a.ShangPinId,a.ImgName,a.Id as ShangJia_ShangPin_TuCeId,case when b.ImgKey is null then b.ImgKey else 1 end as ImgKey  from ShangJia_ShangPin_TuCe as a left join  ShangJia_Sku_TuTou as b on a.id=b.ShangJia_ShangPin_TuCeId  where a.ShangPinId='{0}' ", shangpinid);

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
                sqlList.Add(string.Format(" insert into ShangJia_Sku_TuTou(ShangJia_ShangPin_TuCeId,ImgKey,ImgName,Sort) values('{0}','{1}','{2}',{3})", item.ShangJia_ShangPin_TuCeId, item.ImgKey, item.ImgName,item.Sort));
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


        public bool SaveShangJia_ShangPin_TuCe(List<DataModel.ShangJia_ShangPin_TuCe> list)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            List<string> sqlList = new List<string>();            
            foreach (DataModel.ShangJia_ShangPin_TuCe item in list)
            {
                sqlList.Add(string.Format(" insert into ShangJia_ShangPin_TuCe(ShangPinId,ImgName) values('{0}','{1}')", item.ShangPinId, item.ImgName));
            }
            dbSession.Context.ExcuteNoQuery(sqlList);
            return true;
        }

        public List<string> GetSelectImgByImgkey(string imgKey)
        {
           //缩放图
            var imgPath = System.Web.Configuration.WebConfigurationManager.AppSettings["WebImgPath"];
            List<string> list = new List<string>();
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select a.Id,a.ShangPinId,a.ImgName,a.Id as ShangJia_ShangPin_TuCeId,b.ImgKey from ShangJia_Sku_TuTou as b  left join ShangJia_ShangPin_TuCe as a  on a.id=b.ShangJia_ShangPin_TuCeId where b.ImgKey='{0}' ", imgKey);

            var dt = dbSession.Context.QueryTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    var path =imgPath + rows["ShangPinId"] + "/" + rows["ImgName"];
                    path = path.Replace("\\","/");
                    list.Add(path);
                }
            }
            return list;
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
            var strArrSku= "";
            foreach (var item in ents)
	        {
                item.SKU = mendian + "-" + item.ShangPinFenLeiId + "-" + item.Id + "-" + item.YanseId + "-" + item.ChiMaId;
                strArrSku +=strArrSku==""?("'"+item.SKU+"'"):",'"+item.SKU+"'";
	        }
            /*由于有编码匹配 考虑关联查询（没有解决mysql中int变字符叠加 拼串的问题 ）
             *查询数据库一次，将sku的价格拿到  在反补到商品上架传输类中
            */
            var skuListStr =string.Format("select * from shangjia_sku_info where sku in({0})",strArrSku);
            var dtSangJia = db.Context.QueryTable(skuListStr);
            var dicSKU2Price = new Dictionary<string, string>();
            var dicSKU2ShangJia = new Dictionary<string, int>();
            foreach (DataRow dr in dtSangJia.Rows)
            {
                var price = dr["Price"].ToString();
                var isShangJia=dr["IsShangJia"].ToString().ToLower()=="true"?1:0;
                dicSKU2Price.Add(dr["Sku"].ToString(), price);
                dicSKU2ShangJia.Add(dr["Sku"].ToString(), isShangJia);
                
            }
            foreach (var item in ents)
            {
                if (dicSKU2Price.ContainsKey(item.SKU))
                {
                    item.Price = dicSKU2Price[item.SKU];
                }
                if (dicSKU2ShangJia.ContainsKey(item.SKU))
                {
                    item.IsShangJia = dicSKU2ShangJia[item.SKU];
                }
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

        public CResult PutawayGoods(string sku)
        {

            return FunResult.GetSuccess();
        }


        public CResult SetUpShelves(List<string> skus, string desc,string price)
        {
            /*1、插入上架表*/
            var insertFormatSql = @" INSERT INTO shangjia_sku_info
                            ( sku ,
                              Description ,
                              Price ,
                              ShangPinId ,
                              IsShangJia,
                              FenLeiId
                            ) 
                            values('{0}','{1}','{2}','{3}','{4}','{5}')";
            List<string> listStr = new List<string>();
            foreach (var item in skus)
            {
                //mendianId-fenleiId-shangpinid-yanse-chima
                var arrIds = item.Split('-');
                listStr.Add(string.Format("delete from shangjia_sku_info where sku='{0}'",item));
                listStr.Add(string.Format(insertFormatSql,item,desc,price,arrIds[2],1,arrIds[1]));
            }
            var db = Common.DbFactory.CreateDbSession();
            db.Context.ExcuteNoQuery(listStr);
            return FunResult.GetSuccess();

        }





        public CResult SetAllShelves(IList<GoodsShelvesParamsDto> goodsShelvesParamsDtos)
        {
            /*1、插入上架表*/
            var insertFormatSql = @" INSERT INTO shangjia_sku_info
                            ( sku ,
                              Description ,
                              Price ,
                              ShangPinId ,
                              IsShangJia,
                              FenLeiId
                            ) 
                            values('{0}','{1}','{2}','{3}','{4}','{5}')";
            var listStr = new List<string>();
            foreach (var item in goodsShelvesParamsDtos)
            {
                listStr.Add(string.Format(" delete from shangjia_sku_info where sku='{0}'", item.sku));
                //mendianId-fenleiId-shangpinid-yanse-chima
                var arrIds = item.sku.Split('-');
                listStr.Add(string.Format(insertFormatSql, item.sku,item.desc, item.price, arrIds[2], 1, arrIds[1]));
            }
            var db = Common.DbFactory.CreateDbSession();
            db.Context.ExcuteNoQuery(listStr);
            return FunResult.GetSuccess();
        }


        public CResult SetDownShelves(string skus)
        {
            var sqlFormat = "update shangjia_sku_info set IsShangJia=0 where  sku in('{0}')";
            var db = Common.DbFactory.CreateDbSession();
            db.Context.ExcuteNoQuery(string.Format(sqlFormat,skus.Replace(",","','")));
            return FunResult.GetSuccess();
        }
        public string GetGoodsShelevsDesc(int googdId)
        {
            var sql = string.Format("select  * from shangjia_sku_info where shangpinId='{0}'  limit 1", googdId);
            var db = Common.DbFactory.CreateDbSession();
            var dt= db.Context.QueryTable(sql);
            if (dt != null && dt.Rows.Count > 0) {
                return dt.Rows[0]["Description"].ToString();
            }
            return "";
        }

        public bool SaveShangJia_ShangPin_DetailInfo(List<DataModel.ShangJia_ShangPin_DetailInfo> list)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            List<string> sqlList = new List<string>();
            //foreach (DataModel.ShangJia_ShangPin_DetailInfo item in list)
                for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                sqlList.Add(string.Format(" insert into shangjia_shangpin_detailinfo(ShangPinId,MenDianId,ImgName,SortId) values('{0}','{1}','{2}','{3}')",item.ShangPinId,item.MenDianId, item.ImgName,item.SortId));
            }
            dbSession.Context.ExcuteNoQuery(sqlList);
            return true;
        }


        public List<DataModel.ShangJia_ShangPin_DetailInfo> GetProductDetailInfoList(int shangpinid)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select * from  shangjia_shangpin_detailinfo where shangpinId='{0}'  order by SortId ", shangpinid);
            var dt = dbSession.Context.QueryTable(sql);
            var list = dt.ToList<DataModel.ShangJia_ShangPin_DetailInfo>() as List<DataModel.ShangJia_ShangPin_DetailInfo>;
            return list;
        }
    }
}

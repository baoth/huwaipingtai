using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Shelves;
using Toolkit.Ext;
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
        public List<DataModel.ShangJia_ShangPin_Sku_TuTouDto> GetProductPhotoList(int shangpinid)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select a.Id,a.ShangPinId,a.ImgName,a.Id as ShangJia_ShangPin_TuCeId,b.ImgKey from ShangJia_ShangPin_TuCe as a left join  ShangJia_Sku_TuTou as b on a.id=b.ShangJia_ShangPin_TuCeId where a.ShangPinId='{0}' ", shangpinid);

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
            //
            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            rootPath = rootPath.Substring(0,rootPath.LastIndexOf('\\'));
            var imgPath = System.Web.Configuration.WebConfigurationManager.AppSettings["WebImgRealPath"];
            List<string> list = new List<string>();
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select a.Id,a.ShangPinId,a.ImgName,a.Id as ShangJia_ShangPin_TuCeId,b.ImgKey from ShangJia_Sku_TuTou as b  left join ShangJia_ShangPin_TuCe as a  on a.id=b.ShangJia_ShangPin_TuCeId where b.ImgKey='{0}' ", imgKey);

            var dt = dbSession.Context.QueryTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    var path = rootPath + imgPath+"/" + rows["ShangPinId"] + "/" + rows["ImgName"];
                    path = path.Replace("\\","/");
                    list.Add(path);
                }
            }
            return list;
        }




       
    }
}

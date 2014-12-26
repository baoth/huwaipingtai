using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Shelves;
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

       

        public List<DataModel.ShangJia_ShangPin_Sku_TuTouModel> GetProductPhotoList(int shangpinid, string sku)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select a.Id,a.ShangPinId,a.ImgName,b.Sku from ShangJia_ShangPin_TuCe as a left join (select c.Sku,c.ImgName from ShangJia_Sku_TuTou as c where c.Sku='{1}')as b on a.ImgName=b.ImgName where a.ShangPinId='{0}' ",shangpinid,sku);
          
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<DataModel.ShangJia_ShangPin_Sku_TuTouModel>(dt);
        }

        public bool SaveShangJia_Sku_TuTou(List<DataModel.ShangJia_Sku_TuTou> list)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            List<string> sqlList = new List<string>();
            var sku = list[0].Sku;
            sqlList.Add("detete from ShangJia_Sku_TuTou where sku='"+sku+"'");
            foreach(DataModel.ShangJia_Sku_TuTou item in list )
            {
              sqlList.Add( string.Format(" insert into ShangJia_Sku_TuTou(Sku,ImgName) values('{0}','{1}')",item.Sku,item.ImgName));
            }
            dbSession.Context.ExcuteNoQuery(sqlList);
            return true;
        }

        public bool DeleteShangJia_Sku_TuTou(string sku)
        {
            var dbSession = Common.DbFactory.CreateDbSession();           
            var sql="detete from ShangJia_Sku_TuTou where sku='" + sku + "'";
            dbSession.Context.ExcuteNoQuery(sql);
            return true;
        }
    }
}

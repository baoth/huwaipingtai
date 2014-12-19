using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using QSmart.Core.Object;
using DataModel.Order;
using IBusinessOrder.CMS;
using DataModel;
namespace BusinessOrder.CMS
{
    public class OPGoodsCatalog : IOPGoodsCatalog
    {
        
        public bool Add(GoodsCatalog catalog)
        {
            try
            {
                var dbSession = Common.DbFactory.CreateDbSession();
                dbSession.Context.InsertEntity(catalog.CreateQSmartObject());
                dbSession.Context.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(GoodsCatalog catalog)
        {
            try
            {
                var dbSession = Common.DbFactory.CreateDbSession();
                dbSession.Context.ModifyEntity(catalog.CreateQSmartObject());
                dbSession.Context.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var dbSession = Common.DbFactory.CreateDbSession();
                dbSession.Context.DeleteEntity(dbSession.CreateDeleteCommand<GoodsCatalog>(Id));
                dbSession.Context.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
               
        public List<GoodsCatalog> GetGoodsCatalogChild(string code)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Code,Level,Name,Pcode,IsStop from goodsCatalog where code='{0}' or pcode='{0}'", code);
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<GoodsCatalog>(dt);
        }


        public List<GoodsCatalog> GetGoodsCatalogAllList()
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Code,Level,Name,Pcode,IsStop from goodsCatalog ");
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<GoodsCatalog>(dt);
        }


        public List<DataModel.Goods.GoodsCatalog> GetGoodsCatalogNotIsStopList()
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Code,Level,Name,Pcode,IsStop from goodsCatalog where isStop=0 or isStop is null ");
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<DataModel.Goods.GoodsCatalog>(dt);
        }
       
    }
}

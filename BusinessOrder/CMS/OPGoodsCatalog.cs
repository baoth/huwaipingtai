using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using QSmart.Core.Object;
using DataModel.Order;
using IBusinessOrder.CMS;
namespace BusinessOrder.CMS
{
    public class OPGoodsCatalog : IOPGoodsCatalog
    {
        
        public bool Add(DataModel.Goods.GoodsCatalog catalog)
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

        public bool Update(DataModel.Goods.GoodsCatalog catalog)
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
                dbSession.Context.DeleteEntity(dbSession.CreateDeleteCommand<DataModel.Goods.GoodsCatalog>(Id));
                dbSession.Context.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
               
        public List<DataModel.Goods.GoodsCatalog> GetGoodsCatalogChild(string code)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Code,Level,Name,Pcode,IsStop from goodsCatalog where code='{0}' or pcode='{0}'", code);
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<DataModel.Goods.GoodsCatalog>(dt);
        }


        public List<DataModel.Goods.GoodsCatalog> GetGoodsCatalogAllList()
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Code,Level,Name,Pcode,IsStop from goodsCatalog ");
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<DataModel.Goods.GoodsCatalog>(dt);
        }
    }
}

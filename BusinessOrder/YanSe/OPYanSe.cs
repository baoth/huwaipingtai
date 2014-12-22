using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
using IBusinessOrder.YanSe;
namespace BusinessOrder.YanSe
{
    
    public class OPYanSe:IOPYanSe
    {
        public bool Add(DataModel.YanSe yanSe)
        {
            try
            {
                var dbSession = Common.DbFactory.CreateDbSession();
                dbSession.Context.InsertEntity(yanSe.CreateQSmartObject());
                dbSession.Context.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(DataModel.YanSe yanSe)
        {
            try
            {
                var dbSession = Common.DbFactory.CreateDbSession();
                dbSession.Context.ModifyEntity(yanSe.CreateQSmartObject());
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
                dbSession.Context.DeleteEntity(dbSession.CreateDeleteCommand<DataModel.YanSe>(Id));
                dbSession.Context.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataModel.YanSe> GetYanSeList()
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Name from YanSe ");
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<DataModel.YanSe>(dt);
        }


        public DataModel.YanSe GetYanSeModel(string id)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Name from YanSe where Id='" + id + "' ");
            var dt = dbSession.Context.QueryTable(sql);
            if (dt.Rows.Count > 0)
            {
                var list = dbSession.Context.ConversionEntity<DataModel.YanSe>(dt);
                if (list != null && list.Count > 0)
                {
                    return list[0];
                }
                return null;
            }
            return null;
        }


      
    }
}

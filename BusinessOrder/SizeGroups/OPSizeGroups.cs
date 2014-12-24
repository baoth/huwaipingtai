using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.SizeGroups;
using QSmart.Core.Object;
namespace BusinessOrder.SizeGroups
{   
    public class OPSizeGroups:IOPSizeGroups
    {
        public bool Add(DataModel.ChiMaZu sizeGroup)
        {
            try
            {
                var dbSession = Common.DbFactory.CreateDbSession();
                dbSession.Context.InsertEntity(sizeGroup.CreateQSmartObject());
                dbSession.Context.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(DataModel.ChiMaZu sizeGroup)
        {
            try
            {
                var dbSession = Common.DbFactory.CreateDbSession();
                dbSession.Context.ModifyEntity(sizeGroup.CreateQSmartObject());
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
                dbSession.Context.DeleteEntity(dbSession.CreateDeleteCommand<DataModel.ChiMaZu>(Id));
                dbSession.Context.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataModel.ChiMaZu> GetSizeGroupsList()
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Name from ChiMaZu ");
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<DataModel.ChiMaZu>(dt);
        }

        public DataModel.ChiMaZu GetSizeGroupsModel(string id)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Name from ChiMaZu where Id='" + id + "' ");
            var dt = dbSession.Context.QueryTable(sql);
            if (dt.Rows.Count > 0)
            {
                var list = dbSession.Context.ConversionEntity<DataModel.ChiMaZu>(dt);
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

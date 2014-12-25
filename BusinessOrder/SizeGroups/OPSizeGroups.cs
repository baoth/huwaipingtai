using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.SizeGroups;
using QSmart.Core.Object;
using DataModel;
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
            var sql = string.Format("select Id,PinPaiId,Name from ChiMaZu where Id='" + id + "' ");
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

        public List<SizeGroupsMainModel> GetSizeGroupsModeListByPPId(int pinPaiId)
        {
            List<SizeGroupsMainModel> list = new List<SizeGroupsMainModel>();
            var mainList = GetSizeGroupsMainList(pinPaiId);
            var detailList = GetSizeGroupsDetailList(pinPaiId);
            foreach (DataModel.ChiMaZu item in mainList)
            {
                SizeGroupsMainModel mainModel = new SizeGroupsMainModel();
                mainModel.Id = item.Id;
                mainModel.Name = item.Name;
                mainModel.PinPaiId = item.PinPaiId;
                var details = detailList.FindAll(e=>e.ChiMaZuId==item.Id);
                if (details != null && details.Count > 0)
                {
                    mainModel.detail = details;
                }
                list.Add(mainModel);                
            }
            return list;
        }
        /// <summary>
        /// 尺码主表List
        /// </summary>
        /// <returns></returns>
        private List<DataModel.ChiMaZu> GetSizeGroupsMainList(int pinPaiId)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,PinPaiId,Name from ChiMaZu where pinpaiid='{0}'",pinPaiId);
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<DataModel.ChiMaZu>(dt);
        }
        /// <summary>
        /// 组明细
        /// </summary>
        /// <param name="pinPaiId"></param>
        /// <returns></returns>
        private List<SizeGroupsDetailModel> GetSizeGroupsDetailList(int pinPaiId)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql ="select   a.Id,a.ChiMaId,a.ChiMaZuId,c.Name as SizeName from ChiMaZuMingxi as a "
                       +" left join chimazu as b on a.chimazuid=b.id "
                       +" left join chima as c on a.chimaid=c.id"
                       +" where b.pinpaiid='"+pinPaiId+"'";
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<SizeGroupsDetailModel>(dt);
        }
    }

    
   
}

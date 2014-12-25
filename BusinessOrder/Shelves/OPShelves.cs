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
    }
}

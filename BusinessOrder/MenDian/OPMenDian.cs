using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.MenDian;

namespace BusinessOrder.MenDian
{
    public class OPMenDian:IOPMenDian
    {

        public List<DataModel.MenDian> GetMenDianList()
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,JingXiaoShangId,Name,Address,Phone from MenDian ");
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<DataModel.MenDian>(dt);
        }
        
    }
}

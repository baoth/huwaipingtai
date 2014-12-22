using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using IBusinessOrder.GoodsSize;
using QSmart.Core.Object;
namespace BusinessOrder.GoodsSize
{
   //public class GoodsSize:IOPGoodsSize
   // {
   //    public bool Add(DataModel.ChiMa goodsSize)
   //     {
   //         try
   //         {
   //             var dbSession = Common.DbFactory.CreateDbSession();
   //             dbSession.Context.InsertEntity(goodsSize.CreateQSmartObject());
   //             dbSession.Context.SaveChange();
   //             return true;
   //         }
   //         catch (Exception ex)
   //         {
   //             throw ex;
   //         }
   //     }

   //    public bool Update(DataModel.ChiMa goodsSize)
   //     {
   //         try
   //         {
   //             var dbSession = Common.DbFactory.CreateDbSession();
   //             dbSession.Context.ModifyEntity(goodsSize.CreateQSmartObject());
   //             dbSession.Context.SaveChange();
   //             return true;
   //         }
   //         catch (Exception ex)
   //         {
   //             throw ex;
   //         }
   //     }

   //     public bool Delete(int Id)
   //     {
   //         try
   //         {
   //             var dbSession = Common.DbFactory.CreateDbSession();
   //             dbSession.Context.DeleteEntity(dbSession.CreateDeleteCommand<DataModel.ChiMa>(Id));
   //             dbSession.Context.SaveChange();
   //             return true;
   //         }
   //         catch (Exception ex)
   //         {
   //             throw ex;
   //         }
   //     }

   //     public List<DataModel.ChiMa> GetGoodsSizeList()
   //     {
   //         var dbSession = Common.DbFactory.CreateDbSession();
   //         var sql = string.Format("select Id,GoodsId,Size from goodsSize ");
   //         var dt = dbSession.Context.QueryTable(sql); ;
   //         return dbSession.Context.ConversionEntity<DataModel.ChiMa>(dt);
   //     }


   //     public DataModel.GoodsSize GetGoodsSizeModel(string id)
   //     {
   //         var dbSession = Common.DbFactory.CreateDbSession();
   //         var sql = string.Format("select Id,GoodsId,Size from goodsSize where Id='"+id+"' ");
   //         var dt = dbSession.Context.QueryTable(sql);
   //         if (dt.Rows.Count > 0)
   //         { 
   //             var list=dbSession.Context.ConversionEntity<DataModel.GoodsSize>(dt);
   //             if (list != null && list.Count > 0)
   //             { 
   //                 return list[0];
   //             }
   //             return null;
   //         }
   //         return null;
   //     }
   // }
}

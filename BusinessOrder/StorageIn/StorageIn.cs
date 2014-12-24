using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.StorageIn;
using DataModel;
using Toolkit.CommonModel;
using Toolkit.Ext;
namespace BusinessOrder.StorageIn
{
    public class StorageIn : IStorageIn
    {

        public IList<DataModel.YanSe> GetColorByGoodsId(string gid)
        {
            return new List<DataModel.YanSe>()
            {
              new DataModel.YanSe(){Name="深灰色",Id=1},
              new DataModel.YanSe(){Name="藏青色",Id=2},
              new DataModel.YanSe(){Name="天蓝色",Id=3},
              new DataModel.YanSe(){Name="宝石绿",Id=4},
              new DataModel.YanSe(){Name="葡萄紫",Id=5}
            };
        }

        public IList<ChiMa> GetSizeByGoodsId(string gid)
        {
            return new List<ChiMa>()
            {
              new ChiMa(){Name="M",Id=1},
              new ChiMa(){Name="L",Id=2},
              new ChiMa(){Name="XL",Id=3},
              new ChiMa(){Name="XXL",Id=4},
              new ChiMa(){Name="4XL",Id=5}
            };
        }


        public IList<HuoWei> GetHuoWei()
        {
            return new List<HuoWei>() { 
                new HuoWei(){Id=1,Name="货位1"},
                new HuoWei(){Id=1,Name="货位2"},
                new HuoWei(){Id=1,Name="货位3"},
                new HuoWei(){Id=1,Name="货位4"}

            };
        }
        /// <summary>
        /// 传到后台的类
        /// </summary>
        /// <param name="listRuKuMingXiDto"></param>
        /// <returns></returns>
        public CResult SaveStorageIn(string canKuId, string mainId, string userName, string rukuDate, string shangPinId, List<RuKuMingXiDto> listRuKuMingXiDto)
        {
            var strFormatMainSql = "INSERT INTO ruku(Id,CangkuId,ShangPinId,RiQi,RukuRen) values ('{0}','{1}','{2}','{3}','{4}')";
            var strFormatDetailSql ="insert into rukumingxi(RuKuId,HuoWeiId,YanSeId,ChiMaId,ShuLiang,DanJia,JinEr) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
            try
            {
                var mainGuid =string.IsNullOrEmpty(mainId)? CommonFun.GetNewGuid():mainId;
                var jiage = 0;
                List<string> listDetailStr = new List<string>();
                listDetailStr.Add(string.Format(strFormatMainSql,mainGuid,canKuId, shangPinId,rukuDate, userName));
                foreach (var item in listRuKuMingXiDto)
                {
                    listDetailStr.Add(string.Format(strFormatDetailSql, mainGuid, item.HWId, item.ColorId, item.SizeId, item.Num, 0, item.Num * jiage));
                }
                var dbSession = Common.DbFactory.CreateDbSession();
                dbSession.Context.ExcuteNoQuery(listDetailStr);
            }
            catch (Exception ex)
            {
                Toolkit.Fun.FunResult.GetError(ex.Message.ToString());
            }
            return Toolkit.Fun.FunResult.GetSuccess();
        }
    }
}

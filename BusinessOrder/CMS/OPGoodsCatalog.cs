using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using QSmart.Core.Object;
using DataModel.Order;
using IBusinessOrder.CMS;
using DataModel;
using Toolkit.Ext;
namespace BusinessOrder.CMS
{
    public class OPGoodsCatalog : IOPGoodsCatalog
    {

        public bool Add(ShangPinFenLei catalog)
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

        public bool Update(ShangPinFenLei catalog)
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
                dbSession.Context.DeleteEntity(dbSession.CreateDeleteCommand<ShangPinFenLei>(Id));
                dbSession.Context.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ShangPinFenLei> GetGoodsCatalogChild(string code)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Code,Level,Name,Pcode,IsStop from goodsCatalog where code='{0}' or pcode='{0}'", code);
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<ShangPinFenLei>(dt);
        }


        public List<ShangPinFenLei> GetGoodsCatalogAllList()
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Code,Level,Name,Pcode,IsStop from goodsCatalog ");
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<ShangPinFenLei>(dt);
        }


        public List<ShangPinFenLei> GetGoodsCatalogNotIsStopList()
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql = string.Format("select Id,Code,Level,Name,Pcode,IsStop from goodsCatalog where isStop=0 or isStop is null ");
            var dt = dbSession.Context.QueryTable(sql); ;
            return dbSession.Context.ConversionEntity<ShangPinFenLei>(dt);
        }



        /// <summary>
        /// 获得商品分类的级数据
        /// </summary>
        /// <returns></returns>
        public List<ShangPinFenLei> GetGoodsCataLog(List<int> levels)
        {
            var listCataLog = new List<ShangPinFenLei>();
            string sWhere = " 1<>1 ";
            for (int i = 0; i < levels.Count; i++)
			{
                sWhere += " or Level=" + levels[i] ;
            }
            string sql = "select * from shangpinfenlei where "+sWhere;
            var dbSession = Common.DbFactory.CreateDbSession();
            var dt = dbSession.Context.QueryTable(sql);
            return dbSession.Context.ConversionEntity<ShangPinFenLei>(dt);
        }
        /// <summary>
        /// 商品目录模版路径
        /// </summary>
        /// <param name="goodsSKU"></param>
        /// <returns></returns>
        public string GetGoodsCatalogTemplate(string templateName)
        {
            var path = Toolkit.Path.PathConfig.GetTemplatePath();
            //var strTemplate = "TCatelogAll.htm";
            return path.CombinePath(templateName);
        }
    }
}

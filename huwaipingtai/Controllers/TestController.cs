using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSmart.Core.DataBase;
using DataModel;

namespace huwaipingtai.Controllers
{
    public class TestController : Controller
    {
        // GET: /Test/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateTable() 
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            //db.CreateTable<ColorGroups>(QSmartTableType.InnoDB);
            //db.CreateTable<ColorGroupsDetail>(QSmartTableType.InnoDB);
            //db.CreateTable<Colors>(QSmartTableType.InnoDB);
            //db.CreateTable<SizeGroups>(QSmartTableType.InnoDB);
            //db.CreateTable<SizeGroupsDetail>(QSmartTableType.InnoDB);
            //db.CreateTable<Sizes>(QSmartTableType.InnoDB);

            //db.CreateTable<Brand>(QSmartTableType.InnoDB);
            //db.CreateTable<Goods>(QSmartTableType.InnoDB);
            //db.CreateTable<GoodsCatalog>(QSmartTableType.InnoDB);

            //db.CreateTable<Merchant>(QSmartTableType.InnoDB);
            //db.CreateTable<MerchantBrand>(QSmartTableType.InnoDB);

            db.CreateTable<SecitonOperator>(QSmartTableType.InnoDB);
            db.CreateTable<Section>(QSmartTableType.InnoDB);

            db.CreateTable<GoodsAllocation>(QSmartTableType.InnoDB);
            db.CreateTable<Store>(QSmartTableType.InnoDB);
            db.CreateTable<StoreInDoc>(QSmartTableType.InnoDB);
            db.CreateTable<StoreInDocDetail>(QSmartTableType.InnoDB);
            return Content("ok");
        }

    }
}

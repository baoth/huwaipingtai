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
            db.CreateTable<YanSeZu>(QSmartTableType.InnoDB);
            db.CreateTable<YanSeZuMingXi>(QSmartTableType.InnoDB);
            db.CreateTable<YanSe>(QSmartTableType.InnoDB);
            db.CreateTable<ChiMaZu>(QSmartTableType.InnoDB);
            db.CreateTable<ChiMaZuMingXi>(QSmartTableType.InnoDB);
            db.CreateTable<ChiMa>(QSmartTableType.InnoDB);

            db.CreateTable<PinPai>(QSmartTableType.InnoDB);
            db.CreateTable<ShangPin>(QSmartTableType.InnoDB);
            db.CreateTable<ShangPinFenLei>(QSmartTableType.InnoDB);

            db.CreateTable<JingXiaoShang>(QSmartTableType.InnoDB);
            

            db.CreateTable<CaoZuoYuan>(QSmartTableType.InnoDB);
            db.CreateTable<MenDian>(QSmartTableType.InnoDB);
            db.CreateTable<MenDian_CanZuoYuan_GuanLian>(QSmartTableType.InnoDB);

            db.CreateTable<CangKu>(QSmartTableType.InnoDB);
            db.CreateTable<HuoWei>(QSmartTableType.InnoDB);
            
            db.CreateTable<RuKu>(QSmartTableType.InnoDB);
            db.CreateTable<RuKuMingXi>(QSmartTableType.InnoDB);
            return Content("ok");
        }

    }
}

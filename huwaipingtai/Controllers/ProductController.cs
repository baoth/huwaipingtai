using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSmart.Core.DataBase;
using QSmart.Core.Object;
using DataModel.Order;
using DataModel.Goods;

namespace huwaipingtai.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View("Detail");
        }
        public ActionResult List()
        {
            return View("List");
        }
        public ActionResult Test()
        {
            return View("Test");
        }

        public ContentResult CreateTable()
        {
            QSmartDatabaseClient db=DataBaseProvider.Create("db");
            db.CreateTable<Customer>(QSmartTableType.InnoDB);
            db.CreateTable<GoodsShelves>(QSmartTableType.InnoDB);
            db.CreateTable<Cart>(QSmartTableType.InnoDB);
            db.CreateTable<CustomerAddress>(QSmartTableType.InnoDB);
            db.CreateTable<Order>(QSmartTableType.InnoDB);
            
            return Content("ok");
        }
    }

}

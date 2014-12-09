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
            db.CreateTable<CustomerOrder>(QSmartTableType.InnoDB);

            db.CreateTable<Order>(QSmartTableType.InnoDB);
            db.CreateTable<OrderGoods>(QSmartTableType.InnoDB);
            GoodsShelves item1 = new GoodsShelves
            {
                Description = "加棉衣服 卡其色 XL",
                 Price = 88.88M,
                 Sku = 1000000011
            };
            GoodsShelves item2 = new GoodsShelves
            {
                Description = "加棉衣服 卡其色 XXL",
                Price = 89.88M,
                Sku = 1000000012
            };
            GoodsShelves item3 = new GoodsShelves
            {
                Description = "加棉衣服 咖啡色 XL",
                Price = 88.99M,
                Sku = 1000000021
            };
            GoodsShelves item4 = new GoodsShelves
            {
                Description = "加棉衣服 咖啡色 XXL",
                Price = 99.88M,
                Sku = 1000000022
            };
            db.InsertEntity(item1.CreateQSmartObject());
            db.InsertEntity(item2.CreateQSmartObject());
            db.InsertEntity(item3.CreateQSmartObject());
            db.InsertEntity(item4.CreateQSmartObject());
            db.SaveChange();
            /*
             CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` 
SQL SECURITY DEFINER 
VIEW `cartview` AS 
select `cart`.`Sku` AS `Sku`,`cart`.`CustomerId` AS `CustomerId`,`cart`.`Actived` AS `Actived`,`cart`.`Quantity` AS `Quantity`,`goodsshelves`.`Description` AS `description`,`goodsshelves`.`Price` AS `price` 
from (`cart` left join `goodsshelves` on((`cart`.`Sku` = `goodsshelves`.`Sku`)));
             * */
            return Content("ok");
        }
    }

}

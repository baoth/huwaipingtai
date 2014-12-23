﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSmart.Core.DataBase;
using QSmart.Core.Object;

using DataModel;
using DataModel.Order;

namespace FZ.Controllers
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
            ViewData["Money"] = "188.00";
            ViewData["Fee"] = "188.00";
            ViewData["Money"] = "188.00";
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
        public ContentResult userinfo()
        {
            return Content(Request["callback"].ToString() + "({'provinceId':'1','countryId':'2799'})");
        }
        public ContentResult CreateTable()
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.CreateTable<YanSe>(QSmartTableType.InnoDB);
            return Content("ok");

            db.CreateTable<ChiMa>(QSmartTableType.InnoDB); 
            //db.CreateTable<GoodsCatalog>(QSmartTableType.InnoDB);         
            db.CreateTable<Customer>(QSmartTableType.InnoDB);
            db.CreateTable<GoodsShelves>(QSmartTableType.InnoDB);
            db.CreateTable<Cart>(QSmartTableType.InnoDB);
            db.CreateTable<CustomerAddress>(QSmartTableType.InnoDB);
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
            Customer ct = new Customer
            {
                Id = Guid.NewGuid().ToString().Replace("-", ""),
                LoginName = "admin",
                NikeName = "管理员",
                Password = "123456",
                Phone = "123456789"
            };
            db.InsertEntity(ct.CreateQSmartObject());
            db.SaveChange();
            /*
CREATE  OR REPLACE VIEW `cartview` AS
select `cart`.`Id` AS `Id`,`cart`.`Sku` AS `Sku`,`cart`.`CustomerId` AS `CustomerId`,`cart`.`Actived` AS `Actived`,`cart`.`Quantity` AS `Quantity`,`goodsshelves`.`Description` AS `Description`,`goodsshelves`.`Price` AS `Price` from (`cart` left join `goodsshelves` on((`cart`.`Sku` = `goodsshelves`.`Sku`)));
;
             * */
            return Content("ok");
        }
    }

}

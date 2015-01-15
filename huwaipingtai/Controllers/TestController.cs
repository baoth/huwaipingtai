using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSmart.Core.DataBase;
using DataModel;
using DataModel.Order;
using QSmart.Core.Object;

namespace huwaipingtai.Controllers
{
    public class TestController : Controller
    {
        // GET: /Test/
   
        public JsonResult Index()
        {
            return Json(new {UserName="123123",Org="test"},JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateTable() 
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");

            db.CreateTable<Customer>(QSmartTableType.InnoDB);
            db.CreateTable<Cart>(QSmartTableType.InnoDB);
            db.CreateTable<CustomerAddress>(QSmartTableType.InnoDB);
            db.CreateTable<CustomerOrder>(QSmartTableType.InnoDB);

            db.CreateTable<Order>(QSmartTableType.InnoDB);
            db.CreateTable<OrderGoods>(QSmartTableType.InnoDB);

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
            //db.CreateTable<MenDian_CanZuoYuan_GuanLian>(QSmartTableType.InnoDB);

            db.CreateTable<CangKu>(QSmartTableType.InnoDB);
            db.CreateTable<HuoWei>(QSmartTableType.InnoDB);
            
            db.CreateTable<RuKu>(QSmartTableType.InnoDB);
            db.CreateTable<RuKuMingXi>(QSmartTableType.InnoDB);
            return Content("ok");
            /*
CREATE  OR REPLACE VIEW `cartview` AS
select `cart`.`Id` AS `Id`,`cart`.`Sku` AS `Sku`,`cart`.`CustomerId` AS `CustomerId`,`cart`.`Actived` AS `Actived`,`cart`.`Quantity` AS `Quantity`,`goodsshelves`.`Description` AS `Description`,`goodsshelves`.`Price` AS `Price` from (`cart` left join `goodsshelves` on((`cart`.`Sku` = `goodsshelves`.`Sku`)));
;
 * */
        }

    }
}

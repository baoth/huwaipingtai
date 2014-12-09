using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using DataModel.Order;
using BusinessOrder;
using QSmart.Core.DataBase;
using QSmart.Core.Object;

namespace huwaipingtai.Controllers
{
    public class OrderController : BasicController
    {
         //业务接口导入
        BusinessOrder.IOPCustomerAddress iopcustomeraddress;
        public OrderController(IOPCustomerAddress iopcustomeraddress)
         {
             this.iopcustomeraddress = iopcustomeraddress;
         }
        /// <summary>
        /// 立刻购买
        /// </summary>
        /// <returns></returns>
        public void BuyDirect()
        {
            string sku = Request["sku"];
            string quantity = Request["quantity"];
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            Cart entity = new Cart
            {
                 Actived=true,
                 CustomerId=this.CurrentUserInfo.Id,
                 Quantity=int.Parse(quantity),
                 Sku=int.Parse(sku)
            };
            //删除在购物车中相同的商品项(如果有)
            db.ExcuteNoQuery(string.Format("DELETE FROM `cart` WHERE Sku={0} and CustomerId={1}",
                             entity.Sku,entity.CustomerId));
            //修改所有商品的Actived状态为false
            db.ExcuteNoQuery(string.Format("UPDATE `cart` SET `Actived` = 0 WHERE `CustomerId` ={0}",
                             entity.CustomerId));
            //把当前商品插入到购物车中
            db.InsertEntity(entity.CreateQSmartObject());
            //跳转到order/index
        }
        // GET: /Order/
        public ActionResult Index()
        {
            //QSmartDatabaseClient db = DataBaseProvider.Create("db");
            //DataTable dt = db.QueryTable("select Quantity,price from cartview where customerid=" + this.CurrentUserInfo.Id +
            //                             " and actived=1");
            //decimal Money = 0;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Money += (int)dr[0] * (decimal)dr[1];
            //}
            ViewData["Money"] = "0.00";// Money.ToString("C");
            ViewData["Fee"] = "0.00";
            ViewData["Total"] = "0.00";// Money.ToString("C");
            //读取当前客户所有在购物车里状态actived为true的商品
            //计算金额合计
            return View();
            
        }
        public ActionResult pay()
        {
            return View("pay");
        }
        public ActionResult delivery()
        {
            return View("delivery");
        }
        public ActionResult invoice()
        {
            return View("invoice");
        }
        public ActionResult courier()
        {
            return View("courier");
        }
    }

}

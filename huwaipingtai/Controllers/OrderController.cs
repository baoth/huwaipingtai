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
using IBusinessOrder.Order;
using BusinessOrder;
using DataModel.Order;
using Toolkit.Ext;
using QSmart.Core.DataBase;
using System.Data;
using Toolkit.CommonModel;
namespace huwaipingtai.Controllers
{
    public class OrderController : BasicController
    {
         //业务接口导入
        IOPCustomerOrder customerOrder;
        public OrderController(IOPCustomerOrder customerOrder)
        {
            this.customerOrder = customerOrder;
        }
        /*订单提交*/
        [HttpPost]
        public JsonResult SubmitOrder() 
        {
            try
            {
               var entity = Request.CreateInstance<CustomerOrder>();
               entity.CreateDate = DateTime.Now;
               entity.CustomerId = this.CurrentUserInfo.Id;
               var result= customerOrder.SubmitOrder(entity);
               return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new CResult() { IsSuccess=false,Msg=ex.ToString()});
            }
          
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

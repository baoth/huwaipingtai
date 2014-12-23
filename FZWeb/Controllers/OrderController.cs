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
using Toolkit.Fun;
namespace FZ.Controllers
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
               //绑定订单数据
               var entity = Request.CreateInstance<CustomerOrder>();
               entity.CreateDate = DateTime.Now;
               entity.CustomerId = this.CurrentUserInfo.Id;
               //验证订单信息
               var checkResult=customerOrder.VerifyEntity(entity);
               if(!checkResult.IsSuccess){
                   return Json(checkResult); 
               }
               //提交订单
               var result= customerOrder.SubmitOrder(entity);
               return Json(result);
            }
            catch (Exception ex)
            {
                return Json(FunResult.GetError(ex.Message));
            }
          
        }

        

        // GET: /Order/
        public ActionResult Index()
        {

            List<DataModel.View.CartView> cvs = customerOrder.GetActivedCarts(this.CurrentUserInfo.Id);
            decimal totalmoney=0;
            if (cvs != null && cvs.Count > 0)
            {
                foreach (DataModel.View.CartView cv in cvs)
                {
                    totalmoney += cv.Price * cv.Quantity;
                }
            }

            ViewData["Money"] = totalmoney.ToString("C");
            ViewData["Fee"] = ((decimal)0).ToString("C");
            ViewData["Total"] = totalmoney.ToString("C");
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

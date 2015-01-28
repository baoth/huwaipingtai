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
using Toolkit.Ext;
using Toolkit.CommonModel;
using Toolkit.Fun;
using Toolkit.Path;
using Common.Weixin;
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
               var result = customerOrder.SubmitOrder(entity);
               return Json(result);
            }
            catch (Exception ex)
            {
                Log.Logger.Write("ordersubmit:error->" + ex.Message);
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
        public ActionResult successSubmit()
        {
            ViewData["OrderId"] = Request["orderId"];
            return View("successSubmit");
        }
        /*支付失败*/
        public ActionResult failureSubmit()
        {
            ViewData["OrderId"] = Request["orderId"];
            return View("failureSubmit");
        }
        public ActionResult userAllOrderList()
        {
           var imagePath= PathConfig.GetWebSmallImagPath();
            if(!string.IsNullOrEmpty(imagePath))
            {
                ViewData["imagepath"]=imagePath;
            }
            var htmlPath=PathConfig.GetWebGenerateHtmlPath();
           if(!string.IsNullOrEmpty(htmlPath))
           {
              ViewData["htmlpath"]=htmlPath;        
           }
            
            return View("userAllOrderList");
        }
        public ActionResult GetUserAllOrderList()
        {
            if (this.CurrentUserInfo == null) return null;
           var customerid = this.CurrentUserInfo.Id;           
           int index = 1 ;
            var pageIndex=Request["pageindex"];
            if (!int.TryParse(pageIndex, out index))
            {
                index = 1;
            }
            var list = customerOrder.GetUserAllOrderList(customerid, 5, index);
           var json = JsonHelp.objectToJson(list);
           return Content(json);
           
        }
        public ActionResult goodlisting()
        {
            var imagePath= PathConfig.GetWebSmallImagPath();
            if(!string.IsNullOrEmpty(imagePath))
            {
                ViewData["ImgPath"]=imagePath;
            }
            var htmlPath=PathConfig.GetWebGenerateHtmlPath();
           if(!string.IsNullOrEmpty(htmlPath))
           {
              ViewData["HtmlPath"]=htmlPath;        
           }           
           return View("goodlisting");
        }
        public ActionResult GetOrderGoodsList()
        {
            if (this.CurrentUserInfo== null) return null;
            var list = customerOrder.GetOrderGoodsList(this.CurrentUserInfo.Id);
            var json = JsonHelp.objectToJson(list);
            return Content(json);
        }

    }

}

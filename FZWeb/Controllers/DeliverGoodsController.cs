using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toolkit.Path;
using DataModel.DeliverGoods;
using Toolkit.Fun;

namespace FZ.Controllers
{
    /// <summary>
    /// 发货业务
    /// </summary>
    public class DeliverGoodsController : Controller
    {
        //
        // GET: /Shipper/
        IBusinessOrder.DeliverGoods.IDeliverGoods idelivergoods;
        public DeliverGoodsController(IBusinessOrder.DeliverGoods.IDeliverGoods idelivergoods1) 
        {
            this.idelivergoods = idelivergoods1;
        }
        public ActionResult Index()
        {
            var imagePath = PathConfig.GetWebSmallImagPath();
            if (!string.IsNullOrEmpty(imagePath))
            {
                ViewData["imagepath"] = imagePath;
            }
            var htmlPath = PathConfig.GetWebGenerateHtmlPath();
            if (!string.IsNullOrEmpty(htmlPath))
            {
                ViewData["htmlpath"] = htmlPath;
            }
            return View();
        }

        public JsonResult GetDeliverGoods()
        {
            int index = 1;
            var pageIndex = Request["pageindex"];
            if (!int.TryParse(pageIndex, out index))
            {
                index = 1;
            }
            var list = idelivergoods.GetDeliverGoodsList(5, index);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeliverGoods() {
            int id;
            var orderId = Request["OrderId"];
            int.TryParse(orderId, out id);
            var shiperInfo = idelivergoods.GetShiperByOrderNo(id);
            var goods = idelivergoods.GetGoodsByOrderNo(id);
            ViewData["ShiperInfo"] = shiperInfo;
            ViewData["OrderId"] = orderId;
            ViewData["DeliverGoods"] = goods;
           return View("DeliverGoods");
        }

        /// <summary>
        /// 设置发货完成
        /// </summary>
        /// <returns></returns>
        public JsonResult SetDeliver() 
        {
            var data = Request["data"];
            try
            {
                var expressDto = Toolkit.JsonHelp.JsonHelp.josnToObject<ExpressDto>(data);
                var cresult = idelivergoods.SetDeliver(expressDto);
                return Json(cresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(FunResult.GetError(ex.Message));
            }
           
           
        }
    }
}

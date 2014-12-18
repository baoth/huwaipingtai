using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBusinessOrder.Goods;
using IBusinessOrder.CMS;
using Toolkit.Fun;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        IPublish iPublish;
        // GET: /Home/
        public HomeController(IPublish ipublish)
        {
            this.iPublish = ipublish;
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CreateTemplate() 
        {
            try
            {
                var b = iPublish.PublishGoods("sku");
                return Json(b);
            }
            catch (Exception ex)
            {
                return Json(FunResult.GetError(ex.Message.ToString()));
            }
        
        }
    }
}

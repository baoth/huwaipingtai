using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBusinessOrder.Goods;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public IOPGoods opGoods = null;
        public HomeController(IOPGoods opGoods) {
            this.opGoods = opGoods;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateTemplate() 
        {
            var p = new CMS.Common.Pub(opGoods);
            p.Publish();
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huwaipingtai.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
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
        public ActionResult Address()
        {
            return View("address");
        }
    }
}

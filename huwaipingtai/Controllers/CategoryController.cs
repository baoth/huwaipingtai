using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huwaipingtai.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View("Products_000");
        }

    }
}

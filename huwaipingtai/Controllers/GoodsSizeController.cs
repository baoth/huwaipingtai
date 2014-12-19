using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huwaipingtai.Controllers
{
    public class GoodsSizeController : Controller
    {
        //
        // GET: /GoodsSize/

        public ActionResult Index()
        {
            return View("index");
        }
        public ActionResult editSize()
        {
            return View("editSize");
        }

    }
}

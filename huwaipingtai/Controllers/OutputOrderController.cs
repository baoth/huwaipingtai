using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huwaipingtai.Controllers
{
    public class OutputOrderController : Controller
    {
        //
        // GET: /OutputOrder/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail()
        { 
            return View("Detail");
        }

    }
}

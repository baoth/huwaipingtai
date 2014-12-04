using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huwaipingtai.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View("Detail");
        }
        public ActionResult List()
        {
            return View("List");
        }
        public ActionResult Test()
        {
            return View("Test");
        }


    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FZ.Controllers
{
    public class ShelvesController : Controller
    {
        //
        // GET: /Brand/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Brand()
        {
            return View("Brand");
        }

    }
}

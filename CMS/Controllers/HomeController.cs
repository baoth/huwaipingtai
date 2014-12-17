using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateTemplate() 
        {
            var p = new CMS.Common.Pub();
            p.Publish();
            return null;
        }
    }
}

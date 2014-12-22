using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huwaipingtai.Controllers
{
    public class StorageInController :BasicController
    {
        //
        // GET: /StorageIn/

        public ActionResult Index()
        {
            return View("/Storage/StorageIn");
        }

    }
}

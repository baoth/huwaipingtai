﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FZ.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult All()
        {
            return View("All");
        }
        public ActionResult Products()
        {
            return View("Products");
        }

    }
}

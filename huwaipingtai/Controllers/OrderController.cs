﻿using System;
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
            return View("orderpay");
        }
        public ActionResult Address()
        {
            return View("address");
        }
        public ActionResult EditAddress()
        {
            return View("editAddress");
        }
    }
}

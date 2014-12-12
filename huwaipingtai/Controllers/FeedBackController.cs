using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huwaipingtai.Controllers
{
    public class FeedBackController : BasicController
    {
        //
        // GET: /FeedBack/

        public ActionResult Advice()
        {
            return View("Advice");
        }

        public ActionResult SubmitAdvice()
        {
            return View("AdviceSubmited");
        }
    }
}

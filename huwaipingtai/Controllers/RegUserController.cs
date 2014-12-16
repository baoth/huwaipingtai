using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huwaipingtai.Controllers
{
    public class RegUserController : Controller
    {
        /// <summary>
        /// 图片验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ImageCode()
        {
            var codeNum = Toolkit.Fun.VerifyCode.GetTxt(5);
            Session["imagecodeuser"] = codeNum;
            var bFile = Toolkit.Fun.VerifyCode.CreateImage(codeNum);
            return File(bFile, "image/Jpeg");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("mobileReg");
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public RedirectResult UserReg()
        {
            var session = Session["imagecodeuser"];
            var verifyCode = Request["verifyCode"];
            var mobile = Request["mobile"];
            return Redirect("/Product/1000000011/index.html");
        }

    }
}

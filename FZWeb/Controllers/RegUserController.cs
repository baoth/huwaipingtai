using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FZ.Controllers
{
    public class RegUserController : Controller
    {

        public RegUserController() 
        {

        }
        /// <summary>
        /// 图片验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ImageCode()
        {
            var codeNum = Toolkit.Fun.VerifyCode.GetTxt(5);
            var bFile = Toolkit.Fun.VerifyCode.CreateImage(codeNum);
            Session["imagecodeuser"] = codeNum.ToLower();
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
        public RedirectResult Register() 
        {
            var vcode = Request["validateCode"];
            var mobile = Request["mobile"];
            var password = Request["password"];
            /*插入数据库*/
            //比对图片code
            if (vcode.ToLower() != Session["imagecodeuser"].ToString()) { 
            
            }
            //比对手机码
            if (password.ToLower() != Session["usermobilecode"].ToString())
            {

            }
            return Redirect("/Product/1000000011/index.html"); ;//临时返回这个
        } 
    }
}

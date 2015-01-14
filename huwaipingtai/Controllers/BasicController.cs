using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huwaipingtai.Controllers
{
    public class BasicController : Controller
    {
        protected UserInfo CurrentUserInfo = null;
        protected IBusinessOrder.User.ILogon iLogon = BusinessTemplate.ConfigBusinessTemplate.GetILogon();
        //
        // GET: /Basic/
        public BasicController() 
        {
            
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var idtO=Request.Cookies["idt"];
            var idt = string.IsNullOrEmpty(idtO + "") ? "" : idtO.Value;
            if (idt == "wx")
            {
                WXOnActionExecuting(filterContext);
            }
            else {
                WebOnActionExecuting(filterContext);
            }
        }

        private void SetFooter()
        {
            if (this.CurrentUserInfo != null)
            {
                ViewData["NameL"] = this.CurrentUserInfo.NickName; ViewData["ActionL"] = "/User/Home";
                ViewData["NameR"] = "退出"; ViewData["ActionR"] = "/User/LogOut";
            }
            else
            {
                ViewData["NameL"] = "登陆"; ViewData["ActionL"] = "/User/logon?t=direct_l";
                ViewData["NameR"] = "注册"; ViewData["ActionR"] = "#";
            }
        }
        private void WebOnActionExecuting(ActionExecutingContext filterContext)
        {
            this.CurrentUserInfo = Session[RequestCommand.SESSION_USERINFO] as UserInfo;
            if (!RequestCommand.Intercepts.Contains(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower() +
                filterContext.ActionDescriptor.ActionName.ToLower()))
            {
                if (CurrentUserInfo == null)
                {
                    var userName = Request.Cookies[RequestCommand.COOKIE_LOGONNAME];

                    if (userName == null)
                    {
                        Session[RequestCommand.LOGON_JUMP_URL] = this.Request.RawUrl;
                        //重定向
                        filterContext.Result = new RedirectResult("/User/logon");
                        return;
                    }
                    else
                    {
                        var password = Request.Cookies[RequestCommand.COOKIE_LOGONPASSWORD];
                        var user = iLogon.Logon(userName.Value, password.Value);
                        if (user == null)
                        {
                            Session[RequestCommand.LOGON_JUMP_URL] = this.Request.RawUrl;
                            //重定向
                            filterContext.Result = new RedirectResult("/User/logon");
                            return;
                        }
                        else
                        {
                            Session[RequestCommand.SESSION_USERINFO] = new UserInfo { Id = user.Id, NickName = user.NikeName };
                            filterContext.Result = new RedirectResult(this.Request.RawUrl);
                        }
                    }
                }
            }
            SetFooter();
        }
        private void WXOnActionExecuting(ActionExecutingContext filterContext)
        {
            //this.CurrentUserInfo = Session[RequestCommand.SESSION_USERINFO] as UserInfo;
            //if (this.CurrentUserInfo == null)
            //{
            //    var sid = Request.Cookies["sid"];
            //    //存
            //}
            this.CurrentUserInfo = new UserInfo { Id = Request.Cookies["sid"].Value, NickName = "游客" };
        }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string NickName { get; set; }
    }

    public static class RequestCommand
    {
        public static readonly string SESSION_USERINFO = "uinfo";
        public static readonly string LOGON_JUMP_URL = "lju";
        public static readonly string COOKIE_LOGONNAME = "baoname";
        public static readonly string COOKIE_LOGONPASSWORD = "baopsw";
        public static List<string> Intercepts = new List<string> { "userlogon", "userdologon", "userlogout",
                                                "feedbackadvice","feedbacksubmitadvice"};
    }
}

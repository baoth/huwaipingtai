using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.Order;

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
            SetCustomerInfo(Request.Cookies["sid"].Value);
        }
        /// <summary>
        /// 设置用户信息
        /// </summary>
        public void SetCustomerInfo(string weixinId)
        {
            if (string.IsNullOrEmpty(weixinId)) return;
            var obj = iLogon;
            if (!obj.IsExist(weixinId))
            {
                var model = new Customer();
                model.Id = Guid.NewGuid().ToString().Replace("-","");
                model.WXID = weixinId;
                model.NikeName = "游客";
                obj.Add(model);
            }
            var user = obj.GetCustomerByWXID(weixinId);
            if (user != null)
            {

                var ncookie = new HttpCookie(RequestCommand.COOKIE_LOGONNAME, user.LoginName);
                ncookie.Expires = DateTime.Now.AddMonths(1);
                var pcookie = new HttpCookie(RequestCommand.COOKIE_LOGONPASSWORD, user.Password);
                pcookie.Expires = DateTime.Now.AddMonths(1);
                Response.SetCookie(ncookie);
                Response.SetCookie(pcookie);

                Session[RequestCommand.SESSION_USERINFO] = new UserInfo { Id = user.Id, NickName = user.NikeName };
                var jumpurl = Session[RequestCommand.LOGON_JUMP_URL] as string;
                Session[RequestCommand.LOGON_JUMP_URL] = null;

                this.CurrentUserInfo = Session[RequestCommand.SESSION_USERINFO] as UserInfo;
            }
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

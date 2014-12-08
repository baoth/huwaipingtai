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
        //
        // GET: /Basic/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (RequestCommand.InterceptAction(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName))
            {
                this.CurrentUserInfo = Session[RequestCommand.SESSION_USERINFO] as UserInfo;
                if (this.CurrentUserInfo == null)
                {
                    ViewData["Name"] = "登陆"; ViewData["Action"] = "/User/logon";
                    Session[RequestCommand.DIRECT_PATH] = this.Request.Path;
                    Response.Redirect("/User/logon");
                    return;
                }
                ViewData["Name"] = this.CurrentUserInfo.NickName;
                ViewData["Action"] = "#";
            }
            base.OnActionExecuting(filterContext);
        }

    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string NickName { get; set; }
    }

    public static class RequestCommand
    {
        public static readonly string SESSION_USERINFO = "uinfo";
        public static readonly string DIRECT_PATH = "dpath";
        public static bool InterceptAction(string ControllerName, string ActionName)
        {
            ControllerName = ControllerName.ToLower();
            ActionName = ActionName.ToLower();
            if (ControllerName == "user" && ActionName == "logon") return false;
            if (ControllerName == "user" && ActionName == "dologon") return false;
            if (ControllerName == "user" && ActionName == "logout") return false;
            return true;
        }
    }
}

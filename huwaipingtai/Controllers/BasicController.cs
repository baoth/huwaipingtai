﻿using System;
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
            base.OnActionExecuting(filterContext);
            if (!RequestCommand.Intercepts.Contains(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower() +
                filterContext.ActionDescriptor.ActionName.ToLower()))
            {
                this.CurrentUserInfo = Session[RequestCommand.SESSION_USERINFO] as UserInfo;
                if (CurrentUserInfo == null)
                {
                    ViewData["Name"] = "登陆"; ViewData["Action"] = "/User/logon";
                    Session[RequestCommand.LOGON_JUMP_URL] = this.Request.Path;
                    //重定向
                    filterContext.Result = new RedirectResult("/User/logon");
                    //Response.Redirect("/User/logon");
                    //加了这句就不再走后面的Action
                    //filterContext.Result = new HttpNotFoundResult();
                    return;
                }
                else
                {
                    ViewData["Name"] = this.CurrentUserInfo.NickName;
                    ViewData["Action"] = "#";
                }
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
        public static List<string> Intercepts = new List<string> { "userlogon", "userdologon", "userlogout"};
    }
}

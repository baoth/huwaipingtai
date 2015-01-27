using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSmart.Weixin.Core;
using Toolkit;
using System.Xml.Linq;
using Log;
using System.Text;
using System.Net;
using System.IO;

namespace huwaipingtai.Controllers
{
    public class WXApiController : Controller
    {
        //
        [HttpGet]
        [ActionName("Index")]
        public ActionResult GetIndex(string signature, string timestamp, string nonce, string echostr)
        {
          
            WeixinCore wc = WeixinAdaptor.CreateWeixinCore();

            if (wc.Check(signature, timestamp, nonce, wc.Token))
            {
                return Content(echostr);//返回随机字符串则表示验证通过
            }
            else
            {
                return Content("");
            }
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult PostIndex(string signature, string timestamp, string nonce, string echostr)
        {
            Logger.Write("进入方法");
            WeixinCore wc =WeixinAdaptor.CreateWeixinCore();
            if (!wc.Check(signature, timestamp, nonce, WeixinAdaptor.Token))
            {
                Logger.Write("参数错误");
                return Content("参数错误！");
            }
            Logger.Write("进入方法通过");
            if (Request.InputStream != null)
            {
                XDocument doc = XDocument.Load(Request.InputStream);
                WeixinMessageBaseInfo baseinfo = WeixinMessageBaseInfo.Create(doc);
                if (baseinfo != null && baseinfo.IsEvent)
                {
                    var context = WeixinAdaptor.GetWxMessage(baseinfo.ToUserName, baseinfo.FromUserName, "");
                    switch (baseinfo.EventType)
                    {
                        case EventType.subscribe:
                            Logger.Write("关注");
                            return Content(context);
                        case EventType.click:
                            Logger.Write("点击首页");
                            return Content(context);
                            // Logger.Write("关注");
                            //var context = WeixinAdaptor.GetWxMessage(baseinfo.ToUserName,baseinfo.FromUserName,"");
                            //return Content(context);
                    }
                }
                else if (baseinfo != null && !baseinfo.IsEvent)
                {
                    Logger.Write("xiaoxi");
                    switch (baseinfo.MsgType)
                    {
                        case MsgType.text:
                            var tm = (AcceptWeixinTextMessage)baseinfo.CreateAcceptMessage();
                            if (string.IsNullOrEmpty(tm.Content)) return Content("");
                            var context = WeixinAdaptor.GetWxMessage(tm.FromUserName, tm.ToUserName, tm.Content);
                            return Content(context);
                        case MsgType.voice:
                            return Content("");

                    }
                }
            }
            return Content("");
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="menuJson">菜单对象</param>
        /// <returns></returns>
        public ActionResult CreateMenu()
        {
            var jsonMenu = "{\"button\":[{\"name\":\"首页\", \"type\":\"click\", \"key\":\"V_HOME_INDEX\"}, {"+
             
           " \"name\": \"发送位置\", "+
         "   \"type\": \"location_select\", "+
            "\"key\": \"V_USER_POSTION\"" +
        "}]}";
            var context = Common.Weixin.WeixinApi.CreateMenuByStream(jsonMenu);
            return Content(context);
        }
        public ActionResult DeleteMenu() {
            var b = Common.Weixin.WeixinApi.DeleteMenu();
            return Content("Ok");
        }

        ///// <summary>
        ///// 删除菜单
        ///// </summary>
        ///// <param name="accessToken">调用接口凭证</param>
        ///// <returns></returns>
        //public CommonResult DeleteMenu(string accessToken)
        //{
        //    var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", accessToken);

        //    return Helper.GetExecuteResult(url);
        //}
    }
}

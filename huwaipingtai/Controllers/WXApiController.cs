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

            WeixinCore wc =WeixinAdaptor.CreateWeixinCore();
            if (!wc.Check(signature, timestamp, nonce, WeixinAdaptor.Token))
            {

                return Content("参数错误！");
            }
            if (Request.InputStream != null)
            {
                XDocument doc = XDocument.Load(Request.InputStream);
                WeixinMessageBaseInfo baseinfo = WeixinMessageBaseInfo.Create(doc);
                if (baseinfo != null && baseinfo.IsEvent)
                {
                    var context = WeixinAdaptor.GetWxMessage(baseinfo.ToUserName, baseinfo.FromUserName, "");
                    baseinfo.EventKey=doc.Element("xml").Element("EventKey").Value;
                    switch (baseinfo.EventType)
                    {
                        case EventType.subscribe:
                            return Content(context);
                        case EventType.click:
                            if (baseinfo.EventKey == "home")
                            {
                                return Content(context);
                            }
                            else
                            {
                                ReplyWeixinNewsMessage rnm = new ReplyWeixinNewsMessage();
                                rnm.FromUserName = baseinfo.ToUserName;
                                rnm.ToUserName = baseinfo.FromUserName;
                                rnm.CreateTime = WeixinCoreExtension.GetTimeStamp(DateTime.Now);
                                ArticleItem item = new ArticleItem();
                                item.Title = "支付测试";
                                item.Description = "支付测试";
                                item.PicUrl = "http://test.nkwang.cn/Product/Images/n1/test/9b839728-d085-468e-acb8-88eb9eb008b8.jpg";
                                item.Url = "http://test.nkwang.cn/WeixinPay/Index?orderId=100000000000000";
                                rnm.Articles.Add(item);
                                return Content(rnm.GetReplyMessage());
                            }
                            
                            //var context = WeixinAdaptor.GetWxMessage(baseinfo.ToUserName,baseinfo.FromUserName,"");
                            //return Content(context);
                    }
                }
                else if (baseinfo != null && !baseinfo.IsEvent)
                {
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
            var jsonMenu = "{\"button\":[{\"name\":\"首页\", \"type\":\"click\", \"key\":\"home\"}, {\"name\":\"支付测试\", \"type\":\"click\", \"key\":\"jspay\"},{" +
             
           " \"name\": \"发送位置\", "+
         "   \"type\": \"location_select\", "+
            "\"key\": \"location\"" +
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

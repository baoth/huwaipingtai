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

        /// <summary>
        /// 调用微信统一支付
        /// </summary>
        /// <returns></returns>
        public JsonResult JsCommonPay()
        {
            //1.获取openid
            string openid = Request.Cookies["sid"].Value;
            if (string.IsNullOrEmpty(openid)) return null;
            //2.生成统一支付xml数据
            string datastring = (new WeixinJsCommonPay { total_fee = 1, out_trade_no = "1", openid = "aaa", body = "test cloath" }).GetRequestData();
            WeixinCore wc = WeixinAdaptor.CreateWeixinCore();
            string resultjson = wc.PostRequest(WeixinJsCommonPay.post_url, datastring);
            Response.Write(resultjson);
            return null;
        }


        /// <summary>
        /// 微信统一支付通知地址
        /// </summary>
        /// <returns></returns>
        public JsonResult CommonPayNotify()
        {
            return null;
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

    /// <summary>
    /// 微信统一支付模型
    /// </summary>
    class WeixinJsCommonPay
    {
        /// <summary>
        /// 微信统一支付请求地址
        /// </summary>
        public static const string post_url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        /// <summary>
        /// 用户标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public int total_fee { get; set; }
        /// <summary>
        /// 通知地址
        /// </summary>
        protected string notify_url { get { return "http://test.nkwang.cn/WXApi/CommonPayNotify"; } }
        /// <summary>
        /// 交易类型
        /// </summary>
        protected string trade_type { get { return "JSAPI"; } }
        /// <summary>
        /// 公众账号ID
        /// </summary>
        protected string appid { get{return WeixinAdaptor.AppId;}}
        /// <summary>
        /// 商户号
        /// </summary>
        protected string mch_id { get { return WeixinAdaptor.MchId; } }
        /// <summary>
        /// 终端IP
        /// </summary>
        protected string spbill_create_ip { get { return "114.215.131.65"; } }
        /// <summary>
        /// 随机字符串
        /// </summary>
        protected string nonce_str { get { return Guid.NewGuid().ToString().Replace("-", ""); } }
        /// <summary>
        /// 签名
        /// </summary>
        protected string sign { get; set; }

        public string GetRequestData()
        {
            this.sign = this.createsign();
            StringBuilder strb = new StringBuilder();
            strb.AppendLine("<xml>");
            strb.AppendLine(string.Format("<openid><![{0}]]></openid>", this.openid));
            strb.AppendLine(string.Format("<body><![CDATA[{0}]]></body>",this.body));
            strb.AppendLine(string.Format("<out_trade_no><![CDATA[{0}]]></out_trade_no>",this.out_trade_no));
            strb.AppendLine(string.Format("<total_fee>{0}</total_fee>",this.total_fee));
            strb.AppendLine(string.Format("<notify_url><![CDATA[{0}]]></notify_url>",this.notify_url));
            strb.AppendLine(string.Format("<trade_type><![CDATA[{0}]]></trade_type>",this.trade_type));
            strb.AppendLine(string.Format("<appid><![CDATA[{0}]]></appid>",this.appid));
            strb.AppendLine(string.Format("<mch_id>{0}</mch_id>",this.mch_id));
            strb.AppendLine(string.Format("<spbill_create_ip><![CDATA[{0}]]></spbill_create_ip>",this.spbill_create_ip));
            strb.AppendLine(string.Format("<nonce_str><![CDATA[{0}]]></nonce_str>",this.nonce_str));
            strb.AppendLine(string.Format("<sign><![CDATA[{0}]]></sign>",this.sign));
            strb.AppendLine("</xml>");
            return strb.ToString();
        }

        protected string createsign()
        {
            string StringA = string.Format("appid={0}&body={1}&mch_id={2}&nonce_str={3}&notify_url={4}&" +
                                           "openid={5}&out_trade_no={6}&" +
                                           "spbill_create_ip={7}&trade_type={8}&total_fee={9}",
                                           this.appid,this.body,this.mch_id,this.nonce_str,this.notify_url,
                                           this.openid,this.out_trade_no,this.spbill_create_ip,this.trade_type,this.total_fee);
            string stringSignTemp = StringA + "&key=" + WeixinAdaptor.API密钥;

            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(stringSignTemp, "MD5").ToUpper();
        }
    }
}

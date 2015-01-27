using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wxPay.TenPayLibV3;
using Toolkit;
using System.Xml.Linq;
using wxPay;
using System.IO;
using System.Text;
using System.Xml;

namespace huwaipingtai.Controllers
{
    public class WeixinPayController : Controller
    {
        //
        // GET: /WeixinPay/

         
        private TenPayV3Info _tenPayInfo;
        public TenPayV3Info TenPayInfo
        {
            get
            {
                if (_tenPayInfo == null)
                {
                    try
                    {
                        _tenPayInfo = new TenPayV3Info(WeixinAdaptor.AppId, WeixinAdaptor.MchId, WeixinAdaptor.API密钥, WeixinAdaptor.AppSecret, WeixinAdaptor.PayNotify);
                    }
                    catch { }
                }
                return _tenPayInfo;
            }
        }

        public ActionResult Index()
        {
            string timeStamp = TenPayUtil.GetTimestamp();
            string nonceStr = TenPayUtil.GetNoncestr();
            string paySign = "";
            string sp_billno = Request["order_no"].ToString();
            string openid = Request.Cookies["sid"].Value;
            string body = Request["good_body"].ToString();
            string fee = Request["fee"].ToString();
            //附加数据
            string attach = sp_billno;
            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");

            if (string.IsNullOrEmpty(sp_billno))
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                sp_billno = DateTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(28);
            }

            sp_billno = DateTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(28);

            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(null);
            //初始化
            //packageReqHandler.Init();
            //packageReqHandler.SetKey(TenPayInfo.Key);
            //设置package订单参数
            packageReqHandler.SetParameter("appid", TenPayInfo.AppId);		  //公众账号ID
            packageReqHandler.SetParameter("body", body);
            packageReqHandler.SetParameter("mch_id", TenPayInfo.Mchid);		  //商户号
            packageReqHandler.SetParameter("nonce_str", nonceStr.ToLower());                    //随机字符串
            packageReqHandler.SetParameter("notify_url", TenPayInfo.TenPayNotify);		    //接收财付通通知的URL
            packageReqHandler.SetParameter("openid", openid);	                    //openid
            packageReqHandler.SetParameter("out_trade_no", sp_billno);		//商家订单号
            packageReqHandler.SetParameter("spbill_create_ip", Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP
            packageReqHandler.SetParameter("total_fee", fee);			        //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.SetParameter("trade_type", "JSAPI");	                    //交易类型

            //获取package包
            string sign = packageReqHandler.CreateMd5Sign("key", TenPayInfo.Key);
            packageReqHandler.SetParameter("sign", sign);	                    //交易类型
            string data = packageReqHandler.ParseXML();
            ViewData["createdata"] = data;
            var result = TenPayV3.Unifiedorder(data);
            ViewData["resultdata"] = result;
            var res = XDocument.Parse(result);
            
            string prepayId = "";
            try
            {
                if (res.Element("xml").Element("return_code").Value == "SUCCESS")
                    prepayId = res.Element("xml").Element("prepay_id").Value;
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View();
            }
            string package = string.Format("prepay_id={0}", prepayId);
            timeStamp = TenPayUtil.GetTimestamp();

            //设置支付参数
            RequestHandler paySignReqHandler = new RequestHandler(null);
            paySignReqHandler.SetParameter("appId", TenPayInfo.AppId);
            paySignReqHandler.SetParameter("timeStamp", timeStamp);
            paySignReqHandler.SetParameter("nonceStr", nonceStr);
            paySignReqHandler.SetParameter("package", package);
            paySignReqHandler.SetParameter("signType", "MD5");
            paySign = paySignReqHandler.CreateMd5Sign("key", TenPayInfo.Key);

            ViewData["appId"] = TenPayInfo.AppId;
            ViewData["timeStamp"] = timeStamp;
            ViewData["nonceStr"] = nonceStr;
            ViewData["package"] = package;
            ViewData["paySign"] = paySign;

            return View();
        }

        public void Notify()
        {

            var qcount = Request.QueryString.Count;
            for (int i = 0; i < qcount; i++)
            {
                Log.Logger.Write("weixinpay_notify_get_" + i + ":" + Request.QueryString[i]);
            }
            
            Stream postdata = Request.InputStream;
            if (postdata != null)
            {
                byte[] b = new byte[postdata.Length];
                postdata.Read(b, 0, (int)postdata.Length);
                string postStr = Encoding.UTF8.GetString(b);
                Log.Logger.Write("weixinpay_notify_post:" + postStr);
            }
            XmlDocument xd = new XmlDocument();
            xd.LoadXml("<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[OK]]></return_msg></xml>");
            Response.Clear();
            Response.ContentType = "text/xml";
            Response.Charset = "UTF-8";
            XmlTextWriter writer = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            xd.WriteTo(writer);
            writer.Flush();
            Response.End();
        }

    }
}

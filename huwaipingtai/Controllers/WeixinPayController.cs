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
using IBusinessOrder.Order;

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
            //根据订单号获取从数据库中body fee 等信息
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

            XmlDocument xd = new XmlDocument();

            Stream postdata = Request.InputStream;
            if (postdata != null)
            {
                var res = XDocument.Load(postdata);
                if (res.Element("xml").Element("return_code").Value == "SUCCESS")
                {
                    string out_trade_no = res.Element("xml").Element("out_trade_no").Value;
                    string openid = res.Element("xml").Element("openid").Value;
                    string transaction_id = res.Element("xml").Element("transaction_id").Value;
                    //在数据库中检查out_trade_no是否已经支付过了如果支付过了直接返回SUCCESS
                    //修改数据库中该out_trade_no单据的状态为已支付返回SUCCESS
                    xd.LoadXml("<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[OK]]></return_msg></xml>");
                    Log.Logger.Write(string.Format("Notify:transaction_id->{0},openid->{1},out_trade_no->{2}", transaction_id, openid, out_trade_no));
                }
                else
                {
                    string error_msg = res.Element("xml").Element("err_code_des").Value;
                    xd.LoadXml("<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[" + error_msg + "]]></return_msg></xml>");
                }
            }
            else
            {
                xd.LoadXml("<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[no notify params]]></return_msg></xml>");
            }
            //if (postdata != null)
            //{
            //    byte[] b = new byte[postdata.Length];
            //    postdata.Read(b, 0, (int)postdata.Length);
            //    string postStr = Encoding.UTF8.GetString(b);
            //    Log.Logger.Write("weixinpay_notify_post:" + postStr);
            //}
            try
            {
                Response.Clear();
                Response.ContentType = "text/xml";
                Response.Charset = "UTF-8";
                XmlTextWriter writer = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                xd.WriteTo(writer);
                writer.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Log.Logger.Write(string.Format("Notify:response_errror->{0}", ex.Message));
            }
        }

        

    }
}

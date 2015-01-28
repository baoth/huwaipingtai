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
        private IBusinessOrder.Order.IOPCustomerOrder customerOrder;
         
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

        public WeixinPayController(IOPCustomerOrder customerOrder)
        {
            this.customerOrder = customerOrder;
        }


        public ActionResult Index()
        {

            string sp_billno = Request["order_no"].ToString();

            ViewData["OrderNo"] = sp_billno;
            string timeStamp = TenPayUtil.GetTimestamp();
            string nonceStr = TenPayUtil.GetNoncestr();
            string paySign = "";
       
         
            string body = string.Empty;
            string fee = string.Empty;
            decimal totalfee = 0;
            //根据订单号获取从数据库中body fee 等信息
            try
            {
                var goodsinfos = this.customerOrder.GetOrderById(Int64.Parse(sp_billno));
                ViewData["Goods"] = goodsinfos;
                foreach (var goodinfo in goodsinfos)
                {
                    totalfee += goodinfo.Price * goodinfo.Quantity;
                }
                body = "商品数量" + goodsinfos.Count;
                var integerpart = decimal.Truncate(totalfee);
                ViewData["Fee"] = totalfee;
                var decimalpart = decimal.Floor((totalfee - integerpart)*100);
                fee = (integerpart * 100 + decimalpart).ToString();
               
            }
            catch (Exception ex)
            {
                ViewData["Msg"] = ex.Message;
                return View("failurePay");
            }

            //附加数据
            string attach = sp_billno;
            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");

            if (string.IsNullOrEmpty(sp_billno))
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                //sp_billno = DateTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(28);
                ViewData["Msg"] ="订单生成错误";
                return View("failurePay");
            }

            //sp_billno = DateTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(28);

            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(null);
            //初始化
            //packageReqHandler.Init();
            //packageReqHandler.SetKey(TenPayInfo.Key);
            //设置package订单参数
            string openid = Request.Cookies["sid"].Value;
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
            var result = TenPayV3.Unifiedorder(data);
            var res = XDocument.Parse(result);
            
            string prepayId = "";
            try
            {
                if (res.Element("xml").Element("return_code").Value == "SUCCESS")
                    prepayId = res.Element("xml").Element("prepay_id").Value;
            }
            catch (Exception ex)
            {
                ViewData["Msg"] = ex.Message;
                return View("failurePay");
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
            string Logmsg = string.Empty;
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
                    try
                    {
                        this.customerOrder.UpdateOrderPayStatus(int.Parse(out_trade_no));
                        xd.LoadXml("<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[OK]]></return_msg></xml>");
                    }
                    catch(Exception ex)
                    {
                        xd.LoadXml("<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[updateorderpaystatus error]]></return_msg></xml>");
                        Logmsg = string.Format("Notify:transaction_id->{0},openid->{1},out_trade_no->{2},error->{3}", transaction_id, openid, out_trade_no, ex.Message);
                    }
                    
                    
                }
                else
                {
                    string error_msg = res.Element("xml").Element("err_code_des").Value;
                    xd.LoadXml("<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[return error]]></return_msg></xml>");
                    Logmsg = string.Format("Notify:return_error->code->{0},msg->{1}", res.Element("xml").Element("err_code").Value, res.Element("xml").Element("err_code_des").Value);
                }
            }
            else
            {
                xd.LoadXml("<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[no post data]]></return_msg></xml>");
                Logmsg = string.Format("Notify:no post data");
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
                Logmsg = string.Format("Notify:response_errror->{0}", ex.Message);
            }
            finally
            {
                if (Logmsg != string.Empty) Log.Logger.Write(Logmsg);
            }
        }

        public ActionResult FailurePay() {

            ViewData["Msg"] = Request["Msg"];
            ViewData["OrderNo"] = Request["OrderNo"];
            return View("failurePay");
        }
        public ActionResult SuccessPay()
        {

            ViewData["Msg"] = Request["Msg"];
            ViewData["OrderNo"] = Request["OrderNo"];
            return View("successPay");
        }

    }
}

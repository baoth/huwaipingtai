﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Configuration;

namespace huwaipingtai.Controllers
{
    public class CartController : BasicController
    {
        IBusinessOrder.Cart.IOPCart iopcart;
        public CartController(IBusinessOrder.Cart.IOPCart iopcart)
        {
            this.iopcart = iopcart;
        }
        //
        // GET: /Cart/

        public ActionResult Index()
        {
            //var cid=Request["cid"];
            //cid = "111";
            //if (!string.IsNullOrEmpty(cid))
            //{
            //    ViewData["cid"] = cid;
            //}
           
            return View("cart");
        }
        public ActionResult Add()
        {
            var customerId = this.CurrentUserInfo.Id;

            var type=Request["type"];//等于2时 立即购买           
            DataModel.Order.Cart model = new DataModel.Order.Cart();
            var pid=Request["sku"];
            var quantity=Request["quantity"];
            int q;
            int.TryParse(quantity,out q);
            var actived=true;            
            int sku;
            int.TryParse(pid,out sku);
            model.Sku = sku;
            model.Quantity = q;
            model.Actived =actived;
            model.CustomerId = customerId;
            
           iopcart.Add(model,type);

           var json=GetDataJsonByCustomerId(customerId);
           return Content(json);
        }

        public ActionResult Delete(string pid)
        {
            var customerId = this.CurrentUserInfo.Id;

            if (string.IsNullOrEmpty(pid)) return null;
            int cartId = 0;
            if (int.TryParse(pid, out cartId))
            {
                iopcart.Delete(cartId);

                var json = GetDataJsonByCustomerId(customerId);
                return Content(json);
            }
            return Content(null);
        }
        public ActionResult GetCartProductByCustomer()
        {            
            
            int customerId = this.CurrentUserInfo.Id;
            
            var json = GetDataJsonByCustomerId(customerId);
            return Content(json);
        }

        private string GetDataJsonByCustomerId(int cid)
        {
            List<DataModel.View.CartView> list = iopcart.CartList(cid);
            var json = JsonHelp.objectToJson(list);
            return json;
        }
        public ActionResult UpdateActived()
        {
            try
            {
                var customerId = this.CurrentUserInfo.Id;//客户信息ID
                var para = Request["para"];
                var type=Request["type"];
                var actived=Request["actived"];
                if (type == "all")
                {
                    iopcart.UpdateActived(customerId.ToString(), actived);
                }
                else
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    if (para.IndexOf('#') >= 0)
                    {
                        var arr = para.Split('#');
                        for (int i = 0, j = arr.Length; i < j; i++)
                        {
                            var strArr = arr[0].Split(',');
                            dic[strArr[0]] = strArr[1];
                        }
                    }
                    else
                    {
                        var strArr = para.Split(',');
                        dic[strArr[0]] = strArr[1];
                    }

                    iopcart.UpdateActived(customerId.ToString(), dic);
                }
                var json = GetDataJsonByCustomerId(customerId);

                return Content(json);
            }
            catch (Exception ex)
            {
                return Content("");
            }

        }

        public ActionResult UpdateQuantity()
        {
            try
            {
                var customerId = this.CurrentUserInfo.Id;//客户信息ID
                var productId=Request["pid"];
                var quantity=Request["quantity"];
                int q;
                int.TryParse(quantity,out q);                
                iopcart.UpdateQuantity(customerId.ToString(),productId,q);
                var json = GetDataJsonByCustomerId(customerId);
                return Content(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public class Message
    {
        public Message(string result,string msg,string data)
        {
            this.result = result;
            this.msg = msg;
            this.data = data;
        }
        public string result { set; get; }
        public string msg { set; get; }
        public string data { set; get; }
    }
    public static class JsonHelp
    {
        /// <summary>
        /// 对象序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string objectToJson(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ScriptingJsonSerializationSection section = WebConfigurationManager.GetSection("system.web.extensions/scripting/webServices/jsonSerialization") as ScriptingJsonSerializationSection;
            if (section != null)
            {
                jss.MaxJsonLength = section.MaxJsonLength;
                jss.RecursionLimit = section.RecursionLimit;
            }
            return jss.Serialize(obj);
        }

        public static T josnToObject<T>(string json) where T :  class
        {
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                return jss.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

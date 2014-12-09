using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Configuration;

namespace huwaipingtai.Controllers
{
    public class CartController : Controller
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
            var cid=Request["cid"];
            cid = "111";
            if (!string.IsNullOrEmpty(cid))
            {
                ViewData["cid"] = cid;
            }
           
            return View("cart");
        }
        public ActionResult Add()
        {
            DataModel.Order.Cart model = new DataModel.Order.Cart();

           //iopcart.Add(model);
            return Json(new Message("ok","",""));
        }

        public ActionResult Delete(string cid,string pid)
        {
            if (string.IsNullOrEmpty(pid) || string.IsNullOrEmpty(cid)) return null;
            int cartId = 0;
            if (int.TryParse(pid, out cartId))
            {
                iopcart.Delete(cartId);

                GetCartProductByCustomer(cid);
            }
            return Content(null);
        }
        public ActionResult GetCartProductByCustomer(string cId)
        {
            
            if (string.IsNullOrEmpty(cId)) return null;
            int customerId = 0;
            int.TryParse(cId,out customerId);
            var json = GetDataJsonByCustomerId(customerId);
            return Content(json);
        }

        private string GetDataJsonByCustomerId(int cid)
        {
            List<DataModel.View.CartView> list = iopcart.CartList(cid);
            var json = JsonHelp.objectToJson(list);
            return json;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessOrder;
using DataModel.Order;
using Toolkit.Ext;

namespace huwaipingtai.Controllers
{
    public class UserController : Controller
    {
        #region 维护客户的发货人地址选择
        IOPCustomerAddress iopcustomeraddress;
        public UserController(IOPCustomerAddress iopcustomeraddress)
        {
            this.iopcustomeraddress = iopcustomeraddress;
        }
        //选择发货人地址
        public ActionResult Address()
        {
            List<CustomerAddress> listAddress = iopcustomeraddress.GetAll();
            ViewData["listAddress"] = listAddress;
            return View("address");
        }
        //选中地址跳回到订单
        public ActionResult UpdateOrderAddress()
        {
            /*1、得到订单Id*/
            var addressId = Request["addressId"];
            var userId = Request["userId"];
            var orderId = Request["orderId"];
            /*2、将订单地址修改*/
            /*3、跳转到订单界面 假定叫Order*/
            return View("Order");
        }
        public ActionResult SaveAddress()
        {
            var entity = Request.CreateInstance<CustomerAddress>();
            if (entity.Id == 0)
            {
                iopcustomeraddress.Add(entity);
            }
            else 
            {
                iopcustomeraddress.Update(entity);
            }
            return Redirect("Address"); 
        }

        //编辑地址
        public ActionResult EditAddress()
        {
            /*1、得到地址Id*/
          
            //if (string.IsNullOrEmpty(entity.Id+""))
            //{//新增默认的用户id附上就行了 
            //    iopcustomeraddress.Add(entity);
            //}
            //else
            //{
            //    /*2如果有addressId 是编辑 给界面赋值*/
            //}
            /*打开发货人修改界面*/
            return View("editAddress");
        }
        #endregion
        /// <summary>
        /// 地址信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAddress()
        {
            var json = TestComFun.GetJson();
            return Content(json);
        }
        //
        // GET: /CustomerAddress/

        public ActionResult Logon()
        {
            return View("logon");
        }

    }
}

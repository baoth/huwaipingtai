using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessOrder;
using DataModel.Order;
using Toolkit.Ext;
using QSmart.Core.DataBase;
using System.Data;

namespace huwaipingtai.Controllers
{
    public class UserController : BasicController
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
            var caddress = new CustomerAddress();
            iopcustomeraddress.Add(caddress);
            return View("");
        }

        //编辑地址
        public ActionResult EditAddress()
        {
            /*1、得到地址Id*/
            var entity= Request.CreateInstance<CustomerAddress>();
            if (string.IsNullOrEmpty(entity.Id+""))
            {//新增默认的用户id附上就行了 
                iopcustomeraddress.Add(entity);
            }
            else
            {
                /*2如果有addressId 是编辑 给界面赋值*/
            }
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

        public void DoLogon()
        {
            var username = this.Request["username"];
            var password = this.Request["password"];
            QSmartDatabaseClient db=DataBaseProvider.Create("db");
            string command = string.Format("select * from customer where LoginName='{0}' and password='{1}'",
                username, password);
            DataTable dt = db.QueryTable(command);
            if (dt.Rows.Count > 0)
            {
                
                Session[RequestCommand.SESSION_USERINFO] = new UserInfo
                {
                    Id=(int)dt.Rows[0]["Id"],
                    NickName = dt.Rows[0]["NikeName"].ToString()
                };
                this.Response.Redirect(Session[RequestCommand.DIRECT_PATH] as string);
            }
            
        }

        public void LogOut()
        {
            Session[RequestCommand.SESSION_USERINFO] = null;
            this.Response.Redirect("/Product/1000000011/index.html");
        }
    }
}

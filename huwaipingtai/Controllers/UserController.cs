﻿using System;
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
            try
            {
                var IdStr = Request["Id"];
                var customerId = this.CurrentUserInfo.Id;
                int i;
                int.TryParse(IdStr,out i);
                iopcustomeraddress.SetDefault(i);
                var listAddress = iopcustomeraddress.GetAll(customerId);
                ViewData["listAddress"] = listAddress;
            }
            catch (Exception ex)
            {
                
            }
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
            try
            {
                if (entity.Id == 0)
                {
                    iopcustomeraddress.Add(entity);
                }
                else 
                {
                    iopcustomeraddress.Update(entity);
                }
            }
            catch (Exception ex)
            {
            }
            return Redirect("Address"); 
        }
        //编辑地址
        public ActionResult EditAddress()
        {
            /*1、得到地址Id*/
            var id = Request["Id"];
            if (!string.IsNullOrEmpty(id) && id != "-1")
            {
                var iid = int.Parse(id);
                var customer = iopcustomeraddress.Select(iid);
                ViewData["Province"] = customer.Province;
                ViewData["City"] = customer.City;
                ViewData["County"] = customer.County;
                ViewData["CreateDate"] = customer.CreateDate;
                ViewData["CustomerId"] = customer.CustomerId;
                ViewData["Default"] = customer.Default;
                ViewData["DetailAddress"] = customer.DetailAddress;
                ViewData["Shipper"] = customer.Shipper;
                ViewData["ShipperPhone"] = customer.ShipperPhone;
                ViewData["Id"] = iid;
            }
            else {
                ViewData["CustomerId"] = this.CurrentUserInfo.Id;
            }
            return View("editAddress");
        }

        public ActionResult DelAddress() 
        {
            var id = Request["Id"];
            if (!string.IsNullOrEmpty(id) && id != "-1")
            {
                iopcustomeraddress.Delete(int.Parse(id));
            }
            return Redirect("Address"); 
        }
        #endregion

        public ActionResult Logon()
        {
            if (Session[RequestCommand.LOGON_JUMP_URL]==null) Session[RequestCommand.LOGON_JUMP_URL] = Request.UrlReferrer.AbsolutePath;
            return View("logon");
        }

        public RedirectResult DoLogon()
        {
            var username = this.Request["username"];
            var password = this.Request["password"];
            QSmartDatabaseClient db=DataBaseProvider.Create("db");
            string command = string.Format("select * from customer where LoginName='{0}' and password='{1}'",
                username, password);
            DataTable dt = db.QueryTable(command);
            if (dt.Rows.Count > 0)
            {
                Session[RequestCommand.SESSION_USERINFO] = new UserInfo { Id = dt.Rows[0]["Id"].ToString(), NickName = dt.Rows[0]["NikeName"] as string };
                var jumpurl = Session[RequestCommand.LOGON_JUMP_URL] as string;
                Session[RequestCommand.LOGON_JUMP_URL] = null;
                
                return Redirect(jumpurl);
            }
            return Redirect("logon");
            
        }

        public RedirectResult LogOut()
        {
            Session[RequestCommand.SESSION_USERINFO] = null;
            Session[RequestCommand.LOGON_JUMP_URL] = null;
            return Redirect("/Product/1000000011/index.html");
        }
    }
}

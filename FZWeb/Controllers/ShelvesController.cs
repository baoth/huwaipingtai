using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toolkit.JsonHelp;

namespace FZ.Controllers
{
    public class ShelvesController : BasicController
    {
        IBusinessOrder.Shelves.IOPShelves iopshelves;
        public ShelvesController(IBusinessOrder.Shelves.IOPShelves iopshelves)
        {
            this.iopshelves = iopshelves;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BrandList()
        {
            return View("BrandList");
        }
        public ActionResult ProductList()
        {
            var pingpaiid=Request["pinpaiid"];
            if (!string.IsNullOrEmpty(pingpaiid))
            {
                ViewData["pinpaiid"] = pingpaiid;
            }
            return View("ProductList");
        }
        
        public ActionResult GetBrandList()
        {
            try
            {
                var list = iopshelves.GetBrandList();
                var jsonStr = JsonHelp.objectToJson(list);
                return Content(jsonStr);
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

        public ActionResult GetProductList()
        {
            try
            {
                int id;
                var pingpaiid=Request["pingpaiid"];
                int.TryParse(pingpaiid,out id);
                var list = iopshelves.GetProductList(id);
                var jsonStr = JsonHelp.objectToJson(list);
                return Content(jsonStr);
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

        public ActionResult SelectImage()
        {
            var sku=Request["Sku"];
            if (!string.IsNullOrEmpty(sku))
            { 
                ViewData["Sku"]=sku;
            }
            return View("SelectImage");
        }

    }
}

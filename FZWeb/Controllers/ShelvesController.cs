using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toolkit.JsonHelp;
using DataModel;
using Toolkit.CommonModel;

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
            var mendian = "1";
            var goodsId = 4;
            var dto = iopshelves.GetGoodsShelvesDto(mendian, goodsId);
            ViewData["GoodsSKUS"] = dto;

            var colorDto = iopshelves.GetGoodsShelvesColor(dto);
            ViewData["ColorSKUS"] = colorDto;
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
            var shangpinid = Request["ShangPinId"];
           // ViewData["Sku"] = "1-1-1-1-1";
            if (!string.IsNullOrEmpty(sku))
            {
                ViewData["Sku"] = sku;//sku;
            }         
            if (!string.IsNullOrEmpty(shangpinid))
            {
                ViewData["ShangPinId"] = shangpinid;
            }
            var imgPath = System.Web.Configuration.WebConfigurationManager.AppSettings["WebImgRealPath"];
            if (!string.IsNullOrEmpty(imgPath))
            { 
                ViewData["imgPath"]=imgPath;
            }
            return View("SelectImage");
        }

        public ActionResult GetProductPhotoList()
        {
            try
            {
                var imgKey = Request["ImgKey"];
                var shangpinid = Request["ShangPinId"];
                int id;
               // imgKey = "1-1-1-1";
               // shangpinid = "1";
                
                int.TryParse(shangpinid, out id);
                var list = iopshelves.GetProductPhotoList(id, imgKey);
                var json = JsonHelp.objectToJson(list);
                return Content(json);
            }
            catch (Exception ex)
            {
                return Content("");               
            }

        }

        public ActionResult SaveShangJia_Sku_TuTou()
        {
            CResult r = new CResult();
            r.IsSuccess = false;
            r.Msg = "系统错误！";
            try
            {
                var data = Request["data"];
                var list = JsonHelp.josnToObject<List<ShangJia_Sku_TuTou>>(data);
                if (iopshelves.SaveShangJia_Sku_TuTou(list))
                {
                    r.IsSuccess = true;
                    r.Msg = "保存成功！";
                }
                else
                {
                    r.IsSuccess = false;
                    r.Msg = "保存错误！";
                }
                return Json(r);
            }
            catch (Exception ex)
            {
                return Json(r);
            }
        }

        public ActionResult GoodsImageList() 
        {
            return View("ImageList");
        }
    }
}


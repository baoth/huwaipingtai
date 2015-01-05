using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toolkit.JsonHelp;
using DataModel;
using Toolkit.CommonModel;
using Toolkit.Fun;
using IBusinessOrder.CMS;

namespace FZ.Controllers
{
    public class ShelvesController : BasicController
    {
        IBusinessOrder.Shelves.IOPShelves iopshelves;
        IPublish iPublist;
        public ShelvesController(IBusinessOrder.Shelves.IOPShelves iopshelves,IPublish ipublish)
        {
            this.iopshelves = iopshelves;
            this.iPublist = ipublish;
        }
        public ActionResult Index()
        {
            var mendian = "1";
            //var goodsId = 4;
            var goodsId = Request["shangpinid"];
            var pingpaiId = Request["pingpaiid"];
            ViewData["shangpinid"] = goodsId;
            ViewData["mendianid"] = mendian;
            var dto = iopshelves.GetGoodsShelvesDto(mendian, int.Parse(goodsId));
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
            var pingpaiid = Request["pinpaiid"];
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
                var pingpaiid = Request["pingpaiid"];
                int.TryParse(pingpaiid, out id);
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
            var ImgKey = Request["ImgKey"];
            var shangpinid = Request["ShangPinId"];
            // ViewData["Sku"] = "1-1-1-1-1";
            if (!string.IsNullOrEmpty(ImgKey))
            {
                ViewData["ImgKey"] = ImgKey;//sku;
            }
            if (!string.IsNullOrEmpty(shangpinid))
            {
                ViewData["ShangPinId"] = shangpinid;
            }
            var imgPath = System.Web.Configuration.WebConfigurationManager.AppSettings["WebImgRealPath"];
            if (!string.IsNullOrEmpty(imgPath))
            {
                ViewData["imgPath"] = imgPath;
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
        public ActionResult UploadImage()
        {

            var shangpinid = Request["ShangPinId"];
            if (!string.IsNullOrEmpty(shangpinid))
            {
                ViewData["ShangPinId"] = shangpinid;
            }
            var imgPath = System.Web.Configuration.WebConfigurationManager.AppSettings["WebImgRealPath"];
            if (!string.IsNullOrEmpty(imgPath))
            {
                ViewData["imgPath"] = imgPath;
            }

            return View("UploadImage");
        }
        public ActionResult FileUpLoad(HttpPostedFileBase imageUpLoad)
        {
            try
            {
                List<ShangJia_ShangPin_TuCe> list = new List<ShangJia_ShangPin_TuCe>();
                string fileName = imageUpLoad.FileName;
                string expandName = fileName.Substring(fileName.LastIndexOf('.') + 1);
                Guid guid = Guid.NewGuid();
                var saveFileName = guid + "." + expandName;

                ////转换只取得文件名，去掉路径。
                //if (fileName.LastIndexOf("\\") > -1)
                //{
                //    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                //}
                var root = System.Web.Configuration.WebConfigurationManager.AppSettings["WebImgRealPath"];

                var shangpinid = Request["ShangPinId"];

                var savePath = Server.MapPath("~" + root + "/" + shangpinid);
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                //保存到相对路径下。
                imageUpLoad.SaveAs(Server.MapPath("~" + root + "/" + shangpinid + "/" + saveFileName));
                ShangJia_ShangPin_TuCe model = new ShangJia_ShangPin_TuCe();
                model.ImgName = saveFileName;

                int spid = 0;
                int.TryParse(shangpinid, out spid);
                model.ShangPinId = spid;

                list.Add(model);

                iopshelves.SaveShangJia_ShangPin_TuCe(list);

                //return RedirectToAction("action","controller",new {参数1=xx，参数2=xxx})               
                return RedirectToAction("UploadImage", "Shelves", new { ShangPinId = shangpinid });
            }
            catch (Exception ex)
            {
                return RedirectToAction("UploadImage");
            }

        }

        public ActionResult GetProductPhotoListByShangpinId()
        {
            try
            {

                var shangpinid = Request["ShangPinId"];
                int id;

                int.TryParse(shangpinid, out id);
                var list = iopshelves.GetProductPhotoList(id);
                var json = JsonHelp.objectToJson(list);
                return Content(json);
            }
            catch (Exception ex)
            {
                return Content("");
            }

        }

        public ActionResult GetSelectImgByImgkey()
        {
            var list = iopshelves.GetSelectImgByImgkey("1-1-1-1");
            return null;
        }

        public JsonResult SetShelves() 
        {
            try
            {
                var goodsDesc = Request["desc"];
                var sku = Request["sku"];
                var price = Request["price"];
                var mendianId = "1";
                var bShelves = iopshelves.SetUpShelves(new List<string>() { sku }, goodsDesc,price);
                var newPath = Toolkit.Path.PathConfig.GetGeneratePath("Product");
                iPublist.PublishGoods(sku,newPath);
                return Json(bShelves,JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(FunResult.GetError(ex.Message.ToString()),JsonRequestBehavior.AllowGet);
            }
        }
    }
}


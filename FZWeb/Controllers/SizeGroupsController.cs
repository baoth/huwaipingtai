using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toolkit.JsonHelp;
using Toolkit.CommonModel;
using DataModel;

namespace FZ.Controllers
{   
    public class SizeGroupsController : BasicController
    {
        IBusinessOrder.SizeGroups.IOPSizeGroups iopSizeGroup;
        public SizeGroupsController(IBusinessOrder.SizeGroups.IOPSizeGroups iopSizeGroup)
        {
            this.iopSizeGroup = iopSizeGroup;
        }
        //
        // GET: /GoodsSize/

        public ActionResult Index()
        {
            return View("index");
        }
        public ActionResult SizeGroups()
        {
            return View("sizeGroups");
        }
        public ActionResult editSize()
        {
            var id = Request["id"];
            if (id != null)
            {
                var model = iopSizeGroup.GetSizeGroupsModel(id.ToString());
                if (model != null)
                {
                    ViewData["Id"] = model.Id;
                    ViewData["Name"] = model.Name;
                    // ViewData["GoodsId"] = model.GoodsId;
                    ViewData["title"] = "商品尺寸编辑";
                    ViewData["pageType"] = "2";
                }
            }
            else
            {
                ViewData["title"] = "商品尺寸添加";
                ViewData["pageType"] = "1";
            }
            return View("editSize");
        }
        public ActionResult GetSizeGroupsList()
        {
            string jsonStr = string.Empty;
            var list = iopSizeGroup.GetSizeGroupsList();
            jsonStr = JsonHelp.objectToJson(list); 
            return Content(jsonStr);
        }
        public ActionResult Save()
        {
            CResult r = new CResult();
            r.IsSuccess = false;
            r.Msg = "系统错误！";
            try
            {

                var type = Request["type"];
                var data = Request["data"];
                var model = JsonHelp.josnToObject<ChiMaZu>(data);
                switch (type)
                {
                    case "1"://添加
                        iopSizeGroup.Add(model);
                        r.IsSuccess = true;
                        r.Msg = "添加成功！";
                        break;
                    case "2"://修改
                        iopSizeGroup.Update(model);
                        r.IsSuccess = true;
                        r.Msg = "修改成功！";
                        break;
                    case "3"://删除
                        iopSizeGroup.Delete(model.Id);
                        r.IsSuccess = true;
                        r.Msg = "删除成功！";
                        break;
                }
                return Json(r);
            }
            catch (Exception ex)
            {
                return Json(r);
            }
        }

    }
}

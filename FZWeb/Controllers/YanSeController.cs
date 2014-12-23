using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using Toolkit.CommonModel;

namespace FZ.Controllers
{
   
    public class YanSeController : BasicController
    {
        IBusinessOrder.YanSe.IOPYanSe iopyanse;
        public YanSeController(IBusinessOrder.YanSe.IOPYanSe iopyanse)
        {
            this.iopyanse = iopyanse;
        }
        //
        // GET: /YanSe/

        public ActionResult Index()
        {
            return View("index");
        }
        public ActionResult editYanSe()
        {
            var id = Request["id"];
            if (id != null)
            {
                var model = iopyanse.GetYanSeModel(id.ToString());
                if (model != null)
                {
                    ViewData["Id"] = model.Id;
                    ViewData["Name"] = model.Name;                    
                    ViewData["title"] = "颜色编辑";
                    ViewData["pageType"] = "2";
                }
            }
            else
            {
                ViewData["title"] = "颜色添加";
                ViewData["pageType"] = "1";
            }
            return View("editColor");
        }
        public ActionResult GetYanSeList()
        {
            string jsonStr = string.Empty;
            var list = iopyanse.GetYanSeList();
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
                var model = JsonHelp.josnToObject<YanSe>(data);
                switch (type)
                {
                    case "1"://添加
                        iopyanse.Add(model);
                        r.IsSuccess = true;
                        r.Msg = "添加成功！";
                        break;
                    case "2"://修改
                        iopyanse.Update(model);
                        r.IsSuccess = true;
                        r.Msg = "修改成功！";
                        break;
                    case "3"://删除
                        iopyanse.Delete(model.Id);
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

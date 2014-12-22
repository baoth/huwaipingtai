using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toolkit.CommonModel;
using DataModel;

namespace huwaipingtai.Controllers
{
    public class GoodsSizeController : BasicController
    {
        IBusinessOrder.GoodsSize.IOPGoodsSize iopgoodssize;
        public GoodsSizeController(IBusinessOrder.GoodsSize.IOPGoodsSize iopgoodssize)
        {
            this.iopgoodssize = iopgoodssize;
        }
        //
        // GET: /GoodsSize/

        public ActionResult Index()
        {
            return View("index");
        }
        public ActionResult editSize()
        {
            var id = Request["id"];
            if (id != null)
            {
                var model = iopgoodssize.GetGoodsSizeModel(id.ToString());
                if (model != null)
                {
                    ViewData["Id"] = model.Id;
                    ViewData["Name"] = model.Name;
                   // ViewData["GoodsId"] = model.GoodsId;
                    ViewData["title"] = "商品尺寸编辑";
                    ViewData["pageType"]="2";
                }
            }
            else
            {
                ViewData["title"] = "商品尺寸添加";
                ViewData["pageType"] = "1";
            }
            return View("editSize");
        }
        public ActionResult GetGoodsSizeList()
        {
            string jsonStr = string.Empty;
            var list = iopgoodssize.GetGoodsSizeList();
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
                var model = JsonHelp.josnToObject<ChiMa>(data);
                switch (type)
                {
                    case "1"://添加
                        iopgoodssize.Add(model);
                        r.IsSuccess = true;
                        r.Msg = "添加成功！";
                        break;
                    case "2"://修改
                        iopgoodssize.Update(model);
                        r.IsSuccess = true;
                        r.Msg = "修改成功！";
                        break;
                    case "3"://删除
                        iopgoodssize.Delete(model.Id);
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

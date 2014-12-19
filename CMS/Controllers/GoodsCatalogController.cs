using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using QSmart.Core.Object;
using Toolkit.JsonHelp;
using Toolkit.CommonModel;
using DataModel;
namespace CMS.Controllers
{
    public class GoodsCatalogController : Controller
    {
         IBusinessOrder.CMS.IOPGoodsCatalog iopcatelog;
         public GoodsCatalogController(IBusinessOrder.CMS.IOPGoodsCatalog iopcatelog)
        {
            this.iopcatelog = iopcatelog;
        }
        //
        // GET: /GoodsCatalog/

        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult GetTree()
        {
            string jsonStr = string.Empty;
            var list = iopcatelog.GetGoodsCatalogAllList();
            jsonStr=TreeData.GetGoodsCatalogTree(list);
            return Content(jsonStr);
        }
        public ActionResult GetComboGrid()
        {
            string jsonStr = string.Empty;
            var list = iopcatelog.GetGoodsCatalogNotIsStopList();
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
                var model = JsonHelp.josnToObject<ShangPinFenLei>(data);
                switch (type)
                {
                    case "1"://添加
                        iopcatelog.Add(model);
                        r.IsSuccess = true;
                        r.Msg = "添加成功！";
                        break;
                    case "2"://修改
                        iopcatelog.Update(model);
                        r.IsSuccess = true;
                        r.Msg = "修改成功！";
                        break;
                    case "3"://删除
                        iopcatelog.Delete(model.Id);
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

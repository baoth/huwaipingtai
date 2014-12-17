using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using QSmart.Core.Object;

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using IBusinessOrder.StorageIn;

namespace FZ.Controllers
{
    public class StorageInController :BasicController
    {
        //
        // GET: /StorageIn/
        public IStorageIn iStorageIn;
        public StorageInController(IStorageIn istoragein) {
            iStorageIn = istoragein;
        }
        public ActionResult Index()
        {
            ViewData["Color"] =iStorageIn.GetColorByGoodsId("gid");
            ViewData["Size"] = iStorageIn.GetSizeByGoodsId("gid");
            return View("StorageIn");
        }
        public JsonResult GetHuoWei() 
        {
            var huoWeiList =iStorageIn.GetHuoWei();
            return Json(new { rows = huoWeiList, total = huoWeiList.Count });
        }

    }
}

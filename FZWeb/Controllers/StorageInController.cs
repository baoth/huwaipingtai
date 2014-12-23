using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;

namespace FZ.Controllers
{
    public class StorageInController :BasicController
    {
        //
        // GET: /StorageIn/

        public ActionResult Index()
        {
            ViewData["Color"] = new List<YanSe>()
            {
              new YanSe(){Name="深灰色",Id=1},
              new YanSe(){Name="藏青色",Id=2},
              new YanSe(){Name="天蓝色",Id=3},
              new YanSe(){Name="宝石绿",Id=4},
              new YanSe(){Name="葡萄紫",Id=5}
            };
            ViewData["Size"] = new List<ChiMa>()
            {
              new ChiMa(){Name="M",Id=1},
              new ChiMa(){Name="L",Id=2},
              new ChiMa(){Name="XL",Id=3},
              new ChiMa(){Name="XXL",Id=4},
              new ChiMa(){Name="4XL",Id=5}
            };
            return View("StorageIn");
        }

    }
}

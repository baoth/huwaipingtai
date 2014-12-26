using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toolkit.Fun;
using  IBusinessOrder.CMS;
namespace FZ.Controllers
{
    public class TemplateController : Controller
    {

       IPublish iPublish;
       public TemplateController(IPublish ipublish)
       {
           iPublish = ipublish;
       }
        // GET: /Template/
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GenerateCatalogList()
        {
            try
            {
                var b = iPublish.PublishCatalogTemplate();
                return Json(b,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(FunResult.GetError(ex.Message.ToString()),JsonRequestBehavior.AllowGet);
            }

        }

    }
}

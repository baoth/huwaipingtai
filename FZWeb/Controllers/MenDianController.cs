using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toolkit.JsonHelp;

namespace FZ.Controllers
{
    public class MenDianController : BasicController
    {
        IBusinessOrder.MenDian.IOPMenDian iopmendian;
        public MenDianController(IBusinessOrder.MenDian.IOPMenDian iopmendian)
        {
            this.iopmendian = iopmendian;
        }
        //
        // GET: /MenDian/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return View("List");        
        }
        public ActionResult GetMenDianList()
        {
            try
            {

                var list = iopmendian.GetMenDianList();
                var jsonStr = JsonHelp.objectToJson(list);
                return Content(jsonStr);
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

    }
}

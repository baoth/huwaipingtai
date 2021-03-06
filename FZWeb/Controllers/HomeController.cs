﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toolkit.JsonHelp;
namespace FZ.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View("index");
        }
        public ActionResult GetFunctionList()
        {
            var data = GetFunctionData();
            var jsonStr = JsonHelp.objectToJson(data);
            return Content(jsonStr);
        }
        /// <summary>
        /// 临时测试数据
        /// </summary>
        /// <returns></returns>
        private List<FunctionModel> GetFunctionData()
        {
            List<FunctionModel> list = new List<FunctionModel>();
            FunctionModel model1 = new FunctionModel();
            model1.FunctionName = "颜色设置";
            model1.FunctionUrl = "/YanSe/Index";
            model1.ImgUrl = "../../Content/images/FuncDefault.png";
            list.Add(model1);

            FunctionModel model2 = new FunctionModel();
            model2.FunctionName = "尺码设置";
            model2.FunctionUrl = "/GoodsSize/Index";
            model2.ImgUrl = "../../Content/images/FuncDefault.png";
            list.Add(model2);

            FunctionModel model3 = new FunctionModel();
            model3.FunctionName = "入库单";
            model3.FunctionUrl = "/StorageIn/Index";
            model3.ImgUrl = "../../Content/images/FuncDefault.png";
            list.Add(model3);

            FunctionModel model4 = new FunctionModel();
            model4.FunctionName = "出库单";
            model4.FunctionUrl = "/StorageIn/Index";
            model4.ImgUrl = "../../Content/images/FuncDefault.png";
            list.Add(model4);
            
            FunctionModel model5 = new FunctionModel();
            model5.FunctionName = "商品上架";
            model5.FunctionUrl = "/MenDian/List";
            model5.ImgUrl = "../../Content/images/FuncDefault.png";
            list.Add(model5);

            FunctionModel model6 = new FunctionModel();
            model6.FunctionName = "发货";
            model6.FunctionUrl = "/DeliverGoods/Index";
            model6.ImgUrl = "../../Content/images/FuncDefault.png";
            list.Add(model6 );
            return list;
        }
    }

    public class FunctionModel
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { set; get; }
        /// <summary>
        /// 功能URL
        /// </summary>
        public string FunctionUrl { set; get; }
        /// <summary>
        /// 功能图片
        /// </summary>
        public string ImgUrl { set; get; }
    }
}

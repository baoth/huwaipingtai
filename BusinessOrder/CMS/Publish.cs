using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.CMS;
using Toolkit.PubCms;
using IBusinessOrder.Goods;
using System.IO;
using VTemplate.Engine;
using Toolkit.CommonModel;
using Toolkit.Fun;
using IBusinessOrder.Shelves;
namespace BusinessOrder.CMS
{
    public class Publish : IPublish
    {
        public IOPGoods opGoods = null;
        public IOPGoodsCatalog iOPGoodsCatalog = null;
        public IOPShelves iOPShelves=null;
        public Publish(IOPGoods iopgoods, IOPGoodsCatalog iopgoodscatalog, IOPShelves iopshelves)
        {
            opGoods = iopgoods;
            iOPGoodsCatalog = iopgoodscatalog;
            iOPShelves = iopshelves;
        }
        public CResult PublishGoods(string goodsSKU,string newPath)
        {
            string pathTemplate = this.opGoods.GetGoodsCurTemplate("1231");//模块以后根据不同商品改变 现在就一个
            Document document = GetVTDocument(goodsSKU, pathTemplate);
            TextWriter textWriter = new StringWriter();
            document.Render(textWriter);
            //把生成的静态内容写入到目标文件
            string html = textWriter.ToString();
          
            File.WriteAllText(newPath+string.Format(@"\{0}.html",goodsSKU), html, Encoding.UTF8);
            return FunResult.GetSuccess();
        }
        private Document GetVTDocument(string goodsSKU, string fileName)
        {
            var colorSKU = goodsSKU.Substring(0, goodsSKU.LastIndexOf('-'));
            Document document = new Document(fileName, Encoding.UTF8);
            document.SetValue("Goods", opGoods.GetGoods(goodsSKU));

            var imgs = iOPShelves.GetSelectImgByImgkey(colorSKU);
            if (imgs.Count > 0)
            {
                var imgStrs = "";
                foreach (var item in imgs)
                {
                    imgStrs = imgStrs + (string.IsNullOrEmpty(imgStrs) ? item : "," + item);
                }
                document.SetValue("GoodsTuTou", new { FirstPath = imgs[0], Paths = imgStrs,Count=imgs.Count });
            }
            //注册商品尺码对象
            UserDefinedFunction GetGoodsSize = (o) =>
            {
                /*预留吧 有可能根据商品分类来读取数据*/
                var d = TemplateDocument.CurrentRenderingDocument;
                var tag = d == null ? null : d.CurrentRenderingTag;
                var goodsName = tag.Attributes.GetValue("name");
                var goodsId = tag.Attributes.GetValue("id");
                return opGoods.GetGoodsSize(goodsSKU);
            };
            document.RegisterGlobalFunction("GetGoodsSize", GetGoodsSize);
            //UserDefinedFunction GetTuTou = (o) =>
            //{
            //    /*预留吧 有可能根据商品分类来读取数据*/
            //    var d = TemplateDocument.CurrentRenderingDocument;
            //    var tag = d == null ? null : d.CurrentRenderingTag;
            //    var goodsName = tag.Attributes.GetValue("name");
            //    var goodsId = tag.Attributes.GetValue("id");
            //    var imgs = iOPShelves.GetSelectImgByImgkey(colorSKU);
            //    return imgs;
            //};

            //document.RegisterGlobalFunction("GetTuTou", GetTuTou);
            //注册商品颜色对象
            UserDefinedFunction GetGoodsColor = (o) =>
            {
                /*预留吧 有可能根据商品分类来读取数据*/
                var d = TemplateDocument.CurrentRenderingDocument;
                var tag = d == null ? null : d.CurrentRenderingTag;
                var goodsName = tag.Attributes.GetValue("name");
                var goodsId = tag.Attributes.GetValue("id");
                //返回结果
                return opGoods.GetGoodsColor(goodsSKU);
            };
            document.RegisterGlobalFunction("GetGoodsColor", GetGoodsColor);
            return document;
        }
        /*带调整 应该把变化的路径提出来  放在控制层*/
        public CResult PublishCatalogTemplate()
        {
            string pathTemplate = iOPGoodsCatalog.GetGoodsCatalogTemplate("TCatelogAll.htm");
            Document document = GetVTDocument(pathTemplate);
            TextWriter textWriter = new StringWriter();
            document.Render(textWriter);
            //把生成的静态内容写入到目标文件
            string html = textWriter.ToString();
            var newPath = Toolkit.Path.PathConfig.GetGeneratePath("Template");
            File.WriteAllText(newPath + @"\List.html", html, Encoding.UTF8);
            return PublishCatalog3Template();
        }
        private Document GetVTDocument(string template)
        {
            Document document = new Document(template,Encoding.UTF8);
            //document.SetValue("Goods", opGoods.GetGoods(goodsSKU));
            var catalogList = iOPGoodsCatalog.GetGoodsCataLog(new List<int>() {1,2 });

            //注册商品分类1对象
            UserDefinedFunction GetLevel1Catalog = (o) =>
            {
                /*预留吧 有可能根据商品分类来读取数据*/
                var d = TemplateDocument.CurrentRenderingDocument;
                var tag = d == null ? null : d.CurrentRenderingTag;
                var Name = tag.Attributes.GetValue("name");
                var Id = tag.Attributes.GetValue("id");
                var list= catalogList.Where(e => e.Level == 1).ToList();
                return list;
            };
            document.RegisterGlobalFunction("GetLevel1Catalog", GetLevel1Catalog);
            //注册商品分类2对象
            UserDefinedFunction GetLevel2Catalog = (o) =>
            {
                /*预留吧 有可能根据商品分类来读取数据*/
                var d = TemplateDocument.CurrentRenderingDocument;
                var tag = d == null ? null : d.CurrentRenderingTag;
                var goodsName = tag.Attributes.GetValue("name");
                var goodsId = tag.Attributes.GetValue("id");
                //返回结果
                var list =  catalogList.Where(e => e.Level == 2).ToList();
                return list;
            };
            document.RegisterGlobalFunction("GetLevel2Catalog", GetLevel2Catalog);
            return document;
        }

        public CResult PublishCatalog3Template() 
        {
            string pathTemplate = iOPGoodsCatalog.GetGoodsCatalogTemplate("TCatelog2.htm");
            var listShangePinFeiLei = iOPGoodsCatalog.GetGoodsCataLog(new List<int>() { 2,3});
            var list2ShangePinFeiLei = listShangePinFeiLei.Where(e => e.Level == 2).ToList();
            foreach (var item in list2ShangePinFeiLei)
            {
                var list3ShangePinFeiLei = listShangePinFeiLei.Where(e => e.PBianMa == item.BianMa&&e.Level==3).ToList();
                Document document = GetVTDocument(pathTemplate,list3ShangePinFeiLei);
                TextWriter textWriter = new StringWriter();
                document.Render(textWriter);
                //把生成的静态内容写入到目标文件
                string html = textWriter.ToString();
                var newPath = Toolkit.Path.PathConfig.GetGeneratePath("Template");
                File.WriteAllText(newPath + @"\"+item.BianMa+".html", html, Encoding.UTF8); 
            }
           
            return FunResult.GetSuccess();
        }
        private Document GetVTDocument(string template,List<DataModel.ShangPinFenLei> listShangePinFeiLei)
        {
            Document document = new Document(template, Encoding.UTF8);
            //注册商品分类3对象
            UserDefinedFunction GetLevel3Catalog = (o) =>
            {
                /*预留吧 有可能根据商品分类来读取数据*/
                var d = TemplateDocument.CurrentRenderingDocument;
                var tag = d == null ? null : d.CurrentRenderingTag;
                var Name = tag.Attributes.GetValue("name");
                var Id = tag.Attributes.GetValue("id");
                var list = listShangePinFeiLei;
                return list;
            };
            document.RegisterGlobalFunction("GetLevel3Catalog", GetLevel3Catalog);
            return document;
        }

        public CResult PublishGoods(string goodsSKU)
        {
            throw new NotImplementedException();
        }
    }
}

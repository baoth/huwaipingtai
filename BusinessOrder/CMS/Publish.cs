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
namespace BusinessOrder.CMS
{
    public class Publish : IPublish
    {
        public IOPGoods opGoods = null;
        public IOPGoodsCatalog iOPGoodsCatalog = null;
        public Publish(IOPGoods iopgoods, IOPGoodsCatalog iopgoodscatalog)
        {
            opGoods = iopgoods;
            iOPGoodsCatalog = iopgoodscatalog;
        }
        public CResult PublishGoods(string goodsSKU)
        {
            string pathTemplate = this.opGoods.GetGoodsCurTemplate("1231");
            Document document = GetVTDocument("goodId", pathTemplate);
            TextWriter textWriter = new StringWriter();
            document.Render(textWriter);
            //把生成的静态内容写入到目标文件
            string html = textWriter.ToString();
            var newPath = Toolkit.Path.PathConfig.GetGeneratePath("Product");
            File.WriteAllText(newPath+@"\1000000022\1.html", html, Encoding.UTF8);
            return FunResult.GetSuccess();
        }
        private Document GetVTDocument(string goodsSKU, string fileName)
        {
            Document document = new Document(fileName, Encoding.UTF8);
            document.SetValue("Goods", opGoods.GetGoods(goodsSKU));
            //注册商品尺码对象
            UserDefinedFunction GetGoodsSize = (o) =>
            {
                /*预留吧 有可能根据商品分类来读取数据*/
                var d = TemplateDocument.CurrentRenderingDocument;
                var tag = d == null ? null : d.CurrentRenderingTag;
                var goodsName = tag.Attributes.GetValue("name");
                var goodsId = tag.Attributes.GetValue("id");
                return opGoods.GetGoodsSize(goodsId);
            };
            document.RegisterGlobalFunction("GetGoodsSize", GetGoodsSize);
            //注册商品颜色对象
            UserDefinedFunction GetGoodsColor = (o) =>
            {
                /*预留吧 有可能根据商品分类来读取数据*/
                var d = TemplateDocument.CurrentRenderingDocument;
                var tag = d == null ? null : d.CurrentRenderingTag;
                var goodsName = tag.Attributes.GetValue("name");
                var goodsId = tag.Attributes.GetValue("id");
                //返回结果
                return opGoods.GetGoodsColor(goodsId);
            };
            document.RegisterGlobalFunction("GetGoodsColor", GetGoodsColor);
            return document;
        }


        public CResult PublishCatalogTemplate()
        {
            string pathTemplate = iOPGoodsCatalog.GetGoodsCatalogTemplate();
            Document document = GetVTDocument(pathTemplate);
            TextWriter textWriter = new StringWriter();
            document.Render(textWriter);
            //把生成的静态内容写入到目标文件
            string html = textWriter.ToString();
            var newPath = Toolkit.Path.PathConfig.GetGeneratePath("Template");
            File.WriteAllText(newPath + @"\List.html", html, Encoding.UTF8);
            return FunResult.GetSuccess();
        }
        private Document GetVTDocument(string template)
        {
            Document document = new Document(template,Encoding.UTF8);
            //document.SetValue("Goods", opGoods.GetGoods(goodsSKU));
            var catalogList = iOPGoodsCatalog.GetGoodsCataLog12();

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
    }
}

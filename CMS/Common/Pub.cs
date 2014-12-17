using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTemplate.Engine;
using System.Text;
using System.IO;
using DataModel.CMS.Models;
using IBusinessOrder.Goods;

namespace CMS.Common
{
    public class Pub
    {
        public IOPGoods opGoods = null;
        public Pub(IOPGoods iopgoods)
        {
            opGoods = iopgoods;
        }
       public Document GetVTDocument(string goodsSKU,string fileName)
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
       public void Publish() 
       {
           string pathTemplate = this.opGoods.GetGoodsCurTemplate("1231");
           Document document = GetVTDocument("goodId", pathTemplate);
           TextWriter textWriter = new StringWriter();
           document.Render(textWriter);
           //把生成的静态内容写入到目标文件
           string html = textWriter.ToString();
           File.WriteAllText(@"X:\FuZhuangPingTai2014\huwaipingtai\huwaipingtai\Product\1000000022\1.html", html, Encoding.UTF8);
       }
    }
    public class Document : VTemplate.Engine.TemplateDocument
    {
        public Document(string templateContent)
            : base(templateContent)
        {
            new VTemplate.Engine.TemplateDocument(templateContent);
        }

        public Document(string fileName, Encoding encod)
            : base(fileName, encod)
        {
            new VTemplate.Engine.TemplateDocument(fileName, encod);
        }
    }
}
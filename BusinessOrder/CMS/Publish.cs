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
        public Publish(IOPGoods iopgoods)
        {
            opGoods = iopgoods;
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
    }
}

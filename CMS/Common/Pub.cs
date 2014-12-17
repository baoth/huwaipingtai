using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTemplate.Engine;
using System.Text;
using System.IO;
using DataModel.CMS.Models;

namespace CMS.Common
{
    public class OPGoods 
    {
        /// <summary>
        /// 给商品尺寸
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public List<GoodsSize> GetColor(string goodsId) {
            return new List<GoodsSize> { 
                new GoodsSize() {Id="",Name= "藏青色" },
                new GoodsSize() {Id="",Name= "卡其色" },
                new GoodsSize() {Id="",Name= "浅灰色" },
                new GoodsSize() {Id="",Name= "深灰色" },
            };
        }
        /// <summary>
        /// 给商品颜色
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public List<GoodsColor> GetSize(string goodsId)
        {
            return new List<GoodsColor> { 
                new GoodsColor() {Id="",Name= "M" },
                new GoodsColor() {Id="",Name= "L" },
                new GoodsColor() {Id="",Name= "XL" },
                new GoodsColor() {Id="",Name= "XXL" },
                new GoodsColor() {Id="",Name= "XXXL" },
                new GoodsColor() {Id="",Name= "4XL" },
                new GoodsColor() {Id="",Name= "5XL" } 
            };
        }
        public GoodsDto GetGoods(string goodsId)
        {
         
           var o=   new GoodsDto()
            {
                DiapalyPrice = "¥3699.00",
                Price=3699,
                Id=1,
                Desc = "TZ.mall 2014 男装修身  格子 条纹 加绒 加厚 免烫保暖长袖衬衫 男 MS01藏青色 L",
                Brand = "TZ.mall",
                DonationDesc = "TZ.mall 秋冬保暖纯棉袜子 秋冬保暖必需品 赠品拍下不发货 随机发放颜色 袜子 均码 X  1",
                Ecoupons=""

            };
           o.IsDispalyDonationDesc = string.IsNullOrEmpty(o.DonationDesc) ? "none" : "block";
           o.IsDispalyEcoupons = string.IsNullOrEmpty(o.Ecoupons) ? "none" : "block";
          return o;
        }
    }
    public class Pub
    {
        public OPGoods opGoods = null;
        public Pub() {
            opGoods = new OPGoods();
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
                return opGoods.GetSize(goodsId);
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
                return opGoods.GetColor(goodsId);
            };
            document.RegisterGlobalFunction("GetGoodsColor", GetGoodsColor);
            return document;
        }
       public void Publish() 
       {
           string strFile = @"X:\FuZhuangPingTai2014\huwaipingtai\CMS\Template\mGoodsDetail.htm";
           Document document = GetVTDocument("goodId",strFile);
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
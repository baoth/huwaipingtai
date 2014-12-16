using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTemplate.Engine;
using System.Text;

namespace CMS.Common
{
    public class Pub
    {
       public Document GetVTDocument(string fileName)
        {
            Document document = new Document(fileName, Encoding.UTF8);
            document.SetValue("ArticleObject", new {title="test1" });
            #region 注册文章对象PictureObject
            //注册文章对象ArticleObject
            UserDefinedFunction GetPictureObject = (o) =>
            {
                //返回结果
                return new {title="详细页" };
            };
            document.RegisterGlobalFunction("GetPictureObject", GetPictureObject);
            #endregion

            return document;
        }
       public void Publish() 
       {
           //string strFile=""
           //Document document = GetVTDocument();

           //TextWriter textWriter = new StringWriter();
           //document.SetValue("ArticleObject", entity4Article);
           //document.SetValue("ChannelObject", entity4Channel);
           //document.SetValue("SiteObject", entity4Site);
           //document.Render(textWriter);
           ////把生成的静态内容写入到目标文件
           //string html = textWriter.ToString();
           //string fileName = string.Format(ArticlePubFileName4ArticleFormat, entity4Article.ID);
           //string path4Article = Path.Combine(path4Channel, fileName);
           //string format = @"<iframe src='{0}?id={1}' style='width:0px; height:0px; display:none;'></iframe>";
           //string saveReadCountUrl = db.BAP_CMS_SysConstant.FirstOrDefault(e => e.SignValue == "ChangeArticleReadCount").RealValue;
           //format = string.Format(format, saveReadCountUrl, entity4Article.ID);
           //int index = html.LastIndexOf("</body>");
           //html = html.Insert(index, format);
           //File.WriteAllText(path4Article, html, Encoding.UTF8);
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
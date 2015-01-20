using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolkit.Ext;
namespace Toolkit.Path
{
   public class PathConfig
    {
       /// <summary>
       /// 配置模版相对路径
       /// </summary>
       /// <returns></returns>
       public static string GetTemplatePath() 
       {
           var path = AppDomain.CurrentDomain.BaseDirectory;
           var template = System.IO.Path.Combine(path, System.Configuration.ConfigurationManager.AppSettings["SaveTemplatePath"]);
           return template ;
       }
       /// <summary>
       /// 生成的全路径
       /// </summary>
       /// <returns></returns>
       public static string GetGeneratePath(string path="") 
       {
           var p=System.Configuration.ConfigurationManager.AppSettings["GenerateFullPath"];;
           return string.IsNullOrEmpty(path) ? p : p.CombinePath(path);
       }
       /// <summary>
       /// 缩放图路径
       /// </summary>
       /// <returns></returns>
       public static string GetWebSmallImagPath()
       {
           var path = System.Configuration.ConfigurationManager.AppSettings["WebImgPath"];
           return path;
       }
       /// <summary>
       /// 原图路径
       /// </summary>
       /// <returns></returns>
       public static string GetWebOrgImagPath()
       {
           var path = System.Configuration.ConfigurationManager.AppSettings["WebOrgImgPath"];
           return path;
       }
       /// <summary>
       /// 生成的HTML页面
       /// </summary>
       /// <returns></returns>
       public static string GetWebGenerateHtmlPath()
       {
           var path = System.Configuration.ConfigurationManager.AppSettings["WebGenerateHtmlPath"];
           return path;
       }
    }
}

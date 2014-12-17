using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
           var template =System.IO.Path.Combine(path,System.Configuration.ConfigurationManager.AppSettings["TemplatePath"]);
           return template ;
       }
       /// <summary>
       /// 生成的全路径
       /// </summary>
       /// <returns></returns>
       public static string GetGeneratePath() 
       {
           return System.Configuration.ConfigurationManager.AppSettings["GenerateFullPath"];
       }
    }
}

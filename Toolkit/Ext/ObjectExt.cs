using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolkit.Ext
{
   public static class ObjectExt
    {
       /// <summary>
       /// 字符串合并路径
       /// </summary>
       /// <param name="path1"></param>
       /// <param name="path2"></param>
       /// <returns></returns>
       public static string CombinePath(this string path1,string path2) 
       {
           return System.IO.Path.Combine(path1, path2);
       }
    }
}

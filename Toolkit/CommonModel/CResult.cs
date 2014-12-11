using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolkit.CommonModel
{
    /// <summary>
    /// 简单化处理 太复杂增加解析成本 （不声明抽象）
    /// </summary>
   public class CResult
   {
       public bool IsSuccess { get; set; }
       public string Msg { get; set; }
   }
   /// <summary>
   /// 冗余码值 方便更清晰定义错误
   /// </summary>
   public class CResultCode : CResult 
   {
       public int Code { get; set; }
   }
    /// <summary>
    /// 带回实体集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class CResultListData<T> : CResultCode 
   {
       public List<T> Data { get; set; }
   }
   /// <summary>
   /// 带字符串
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public class CResultStrData : CResultCode
   {
       public string Data { get; set; }
   }

}

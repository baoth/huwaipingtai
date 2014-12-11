using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolkit.CommonModel;

namespace Toolkit.Fun
{
    /// <summary>
    /// 返回结果信息类
    /// </summary>
   public static class FunResult
    {
       /// <summary>
       /// 成功
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
       public static CResult GetSuccess(string msg="") 
       {
           return new CResult() { IsSuccess=true,Msg=msg};
       }
       /// <summary>
       /// 失败
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
       public static CResult GetError(string msg = "")
       {
           return new CResult() { IsSuccess = false, Msg = msg };
       }
    }
}

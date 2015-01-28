using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolkit.CommonModel;

namespace Toolkit.Fun
{
    /// <summary>
    /// 复杂的使用
    /// </summary>
    public static class FunResult<T> where T:CResult, new()
    {
       /// <summary>
       /// 成功
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
       public static T GetSuccess(string msg="") 
       {
           return new T() { IsSuccess = true, Msg = msg };
       }
       /// <summary>
       /// 失败
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
       public static T GetError(string msg = "")
       {
           return new T() { IsSuccess = false, Msg = msg };
       }
    }
    /// <summary>
    /// 趋于简单的使用
    /// </summary>
    public static class FunResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static CResult GetSuccess(string msg = "")
        {
            return new CResult() { IsSuccess = true, Msg = msg };
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

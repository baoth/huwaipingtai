using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;

namespace Common.Ext
{
  public  static class HttpRequestBaseExt
    {

        /// <summary>
        /// 根据HttpRequest创建模型实例
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="request">HttpRequest</param>
        /// <returns>模型实例</returns>
        public static T CreateInstance<T>(this HttpRequestBase request) where T : new()
        {
            PropertyInfo[] pis = typeof(T).GetProperties();
            T obj = new T();
            for (int i = 0; i < pis.Length; i++)
            {
                PropertyInfo pi = pis[i];
                if (!pi.CanWrite) continue;
                string value = string.IsNullOrEmpty(request[pi.Name]) ?
                    string.IsNullOrEmpty(request[pi.Name.ToLower()]) ?
                    string.IsNullOrEmpty(request[pi.Name.ToUpper()]) ? string.Empty : request[pi.Name.ToUpper()]
                    : request[pi.Name.ToLower()] : request[pi.Name];
                if (!string.IsNullOrEmpty(value))
                {
                    pi.SetValue(obj, ParseObj(pi.PropertyType, value), null);
                }
            }
            return obj;
        }

        /// <summary>
        /// 字符串值转换成类型值
        /// </summary>
        /// <param name="PropertyType">类型</param>
        /// <param name="value">字符串值</param>
        /// <returns>类型值</returns>
        public static object ParseObj(Type PropertyType, string value)
        {
            if (PropertyType == typeof(String))
            {
                return value;
            }
            else if (PropertyType.IsEnum)
            {
                return int.Parse(value);
            }
            else if (PropertyType == typeof(Int16) || PropertyType == typeof(Nullable<Int16>))
            {
                Int16 result = 0;
                Int16.TryParse(value, out result);
                return result;
            }
            else if (PropertyType == typeof(Int32) || PropertyType == typeof(Nullable<Int32>))
            {
                Int32 result = 0;
                Int32.TryParse(value, out result);
                return result;
            }
            else if (PropertyType == typeof(Int64) || PropertyType == typeof(Nullable<Int64>))
            {
                Int64 result = 0;
                Int64.TryParse(value, out result);
                return result;
            }
            else if (PropertyType == typeof(float) || PropertyType == typeof(Nullable<float>))
            {
                float result = 0;
                float.TryParse(value, out result);
                return result;
            }
            else if (PropertyType == typeof(double) || PropertyType == typeof(Nullable<double>))
            {
                double result = 0;
                double.TryParse(value, out result);
                return result;
            }
            else if (PropertyType == typeof(decimal) || PropertyType == typeof(Nullable<decimal>))
            {
                decimal result = 0;
                decimal.TryParse(value, out result);
                return result;
            }
            else if (PropertyType == typeof(Boolean) || PropertyType == typeof(Nullable<Boolean>))
            {
                bool result = false;
                value = value.ToLower();
                //加上是 和 1 hanyx 2012.5.30
                value = (value == "on" || value == "true" || value == "是" || value == "1" ? "true" : "false");
                Boolean.TryParse(value, out result);
                return result;
            }
            else if (PropertyType == typeof(Double) || PropertyType == typeof(Nullable<Double>))
            {
                double result = 0.0;
                Double.TryParse(value, out  result);
                return result;
            }
            else if (PropertyType == typeof(Array))
            {
                string[] arr = value.Split(',');
                return arr;
            }
            else if (PropertyType == typeof(String[]))
            {
                string[] arr = value.Split(',');
                return arr;
            }
            else if (PropertyType == typeof(DateTime) || PropertyType == typeof(Nullable<DateTime>))
            {
                DateTime dateTime = DateTime.Now;
                DateTime.TryParse(value, out dateTime);
                return dateTime;
            }
            else if (PropertyType == typeof(Guid) || PropertyType == typeof(Nullable<Guid>))
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = Guid.Empty.ToString();
                }
                return new Guid(value);
            }
            else if (PropertyType == typeof(byte[]))
            {
                return null;
            }
            else
            {
                throw new Exception("不支持的类型解析");
            }
        }
    }
}

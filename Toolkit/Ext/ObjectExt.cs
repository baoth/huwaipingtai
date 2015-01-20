using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Reflection;

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
        /// <summary>
        /// DataTable To IList<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return null;
            IList<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T obj = row.ToEntity<T>();
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// DataRow To T
        /// </summary>
        public static T ToEntity<T>(this DataRow row)
        {
            Type objType = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo property =
                objType.GetProperty(column.ColumnName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property == null || !property.CanWrite)
                {
                    continue;
                }
                object value = row[column.ColumnName];
                if (value == DBNull.Value) value = "";

                property.SetValue(obj,ParseObj(property.PropertyType,value.ToString()), null);

            }
            return obj;
        }


        /// <summary>
        /// List To DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> list)
        {
            try
            {
                Type objType = typeof(T);
                DataTable dataTable = new DataTable(objType.Name);
                if (list != null ? list.Count > 0 : false)
                {
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objType);
                    foreach (PropertyDescriptor property in properties)
                    {
                        Type propertyType = property.PropertyType;

                        //nullables must use underlying types
                        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            propertyType = Nullable.GetUnderlyingType(propertyType);
                        //enums also need special treatment
                        if (propertyType.IsEnum)
                            propertyType = Enum.GetUnderlyingType(propertyType); //probably Int32

                        dataTable.Columns.Add(property.Name, propertyType);
                    }

                    foreach (T li in list)
                    {
                        DataRow row = dataTable.NewRow();
                        foreach (PropertyDescriptor property1 in properties)
                        {
                            row[property1.Name] = property1.GetValue(li) ?? DBNull.Value; //can't use null
                        }
                        dataTable.Rows.Add(row);

                    }
                }
                return dataTable;
            }
            catch
            {
                return null;
            }
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

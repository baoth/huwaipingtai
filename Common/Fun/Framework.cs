using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Fun
{
   public static  class Framework
    {
       public static object locker = new object();//添加一个对象作为锁
       /// <summary>
       /// 给的全局数值型唯一码
       /// </summary>
       /// <returns></returns>
       public static int GetOrderNum() 
       {
           lock (locker)//锁 
           {
               try
               {
                   var db = DbFactory.CreateDbSession();
                   db.Context.ExcuteNoQuery("INSERT INTO   ordernum ()values ()");
                   var dataTable = db.Context.QueryTable("select last_insert_id()");
                   return int.Parse(dataTable.Rows[0][0].ToString());
               }
               catch (Exception ex)
               {
                   return 0;
                  
               }
             
           }
       }
    }
}

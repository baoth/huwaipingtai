using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  QSmart.Core.Object;
namespace huwaipingtai
{
    /*与李哥讨论暂定的表*/

    public class Customer:QSmartEntity          //客户
    {
        [AutoIncrement]
        public int Id { get; set; }//Consumer
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string CustomerName { get; set; }
        [StringMaxLength(20, VarCharType.nvarchar)]
        public string Phone { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string LoginName { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string NikeName { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Password { get; set; }
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Mail { get; set; }
    }
    public class CreateData
    {
       public  static void CreateTable(){
           try
           {
                var constring=System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
                var op=new  QSmart.Core.DataBase.QSmartMySqlClient(constring);
                op.CreateTable<Customer>();
           }
           catch (Exception ex)
           {

               throw;
           }
       }
    }
}
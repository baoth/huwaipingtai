using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
namespace DataModel.Order
{
    public class Customer : QSmartEntity          //客户
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }//Consumer
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string RealName { get; set; }
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
   
}

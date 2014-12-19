using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    /// <summary>
    /// 经销商信息  （在用）
    /// </summary>
    public class Merchant : QSmartEntity
    {
       [PrimaryKey]
       [AutoIncrement]
       public int Id { get; set; }
       /// <summary>
       /// 登录名称
       /// </summary>
       [StringMaxLength(50, VarCharType.nvarchar)]
       public string LoginName { get; set; }
       /// <summary>
       /// 登录密码
       /// </summary>
       [StringMaxLength(50, VarCharType.nvarchar)]
       public string Password { get; set; }
       /// <summary>
       /// 经销商名称
       /// </summary>
       [StringMaxLength(50,VarCharType.nvarchar)]
       public string Name { get; set; }
       /// <summary>
       /// 经销商地址
       /// </summary>
       [StringMaxLength(150, VarCharType.nvarchar)]
       public string Address { get; set; }
       /// <summary>
       /// 经销商电话
       /// </summary>
       [StringMaxLength(50, VarCharType.nvarchar)]
       public string Phone { get; set; }

       
    }
}

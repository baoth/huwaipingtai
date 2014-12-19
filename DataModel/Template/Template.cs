using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Template
{
    public class Template : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [StringMaxLength(50,VarCharType.nvarchar)]
        public string Name { get; set;}
        public int TemplateType { get; set; }
        [StringMaxLength(200, VarCharType.nvarchar)]
        public string Path { get; set; }
        /*也可以不存 规定好模版格式*/
        [StringMaxLength(200, VarCharType.nvarchar)]
        public string ShowImagePath { get; set; }
        [StringMaxLength(150, VarCharType.nvarchar)]
        public string Describe { get; set; }
    }
}

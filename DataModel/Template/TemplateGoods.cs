using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Template
{
    public class TemplateGoods : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int GoodsId { get; set; }
        public int TempalteId { get; set; }
    }
}

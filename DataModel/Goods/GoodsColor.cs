using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;
namespace DataModel
{
    public class GoodsColor : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 货品Id
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        ///颜色
        /// </summary>
        public string ColorCode { get; set; }
        public string Name { get; set; }
    }
}

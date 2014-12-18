using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.Goods
{
    public class GoodsSize : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 货品Id
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        ///尺码
        /// </summary>
        public string Size { get; set; }
    }
}

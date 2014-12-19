using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    /// <summary>
    /// 商品表(在用)
    /// </summary>
    public class ShangPin : QSmartEntity
    {
       [PrimaryKey]
       [AutoIncrement]
        public int Id { get; set; }
        public int ShangPinFenLeiId { get; set; }
        public int ChiMaZuId { get; set; }
        public int YanSeZuId { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public int PinPaiId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
    }
}

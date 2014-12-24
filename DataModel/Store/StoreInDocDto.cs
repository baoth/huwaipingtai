using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    /// <summary>
    /// 入库单传输类
    /// </summary>
    public class RuKuMingXiDto
    {
        public int MainId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Num { get; set; }
        public int HWId { get; set; }
        public string RuKuRen { get; set; }


    }
}

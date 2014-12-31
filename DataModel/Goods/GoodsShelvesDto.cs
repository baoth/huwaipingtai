using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Goods
{
   /// <summary>
   /// 商品上架 获取商品传输类
   /// </summary>
   public class GoodsShelvesDto
    {
       public int ShangPinFenLeiId { get; set; }
       public int Id { get; set; }
       public int YanseId { get; set; }
       public int ChiMaId { get; set; }
       public int PinPaiId { get; set; }
       public string ChiMa { get; set; }
       public string Yanse { get; set; }
       public string Name { get; set; }
       public string PinPaiName { get; set; }
       public string ColorSKU { get; set; }
       public string SKU { get; set; }
       public string Price { get; set; }
    }

}

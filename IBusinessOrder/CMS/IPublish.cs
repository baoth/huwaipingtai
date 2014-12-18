using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolkit.CommonModel;

namespace IBusinessOrder.CMS
{
   public interface IPublish
    {
        /// <summary>
        /// 发布商品
        /// </summary>
       CResult PublishGoods(string goodsSKU);
    }
}

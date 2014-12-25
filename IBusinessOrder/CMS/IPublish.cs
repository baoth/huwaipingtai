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
       /// <summary>
       /// 发布目录模版
       /// </summary>
       /// <returns></returns>
       CResult PublishCatalogTemplate();
    }
}

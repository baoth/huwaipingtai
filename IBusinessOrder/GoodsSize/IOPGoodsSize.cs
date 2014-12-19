using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBusinessOrder.GoodsSize
{
    public interface IOPGoodsSize
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cart">实体类</param>       
        /// <returns>bool</returns>
        bool Add(DataModel.ChiMa goodsSize);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="catalog">目录</param>
        /// <returns>bool</returns>
        bool Update(DataModel.ChiMa goodsSize);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool Delete(int Id);
        /// <summary>
        /// 获取所有的目录数据
        /// </summary>
        /// <returns></returns>
        List<DataModel.ChiMa> GetGoodsSizeList();
       
    }
}

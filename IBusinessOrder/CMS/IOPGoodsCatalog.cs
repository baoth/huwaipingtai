using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Order;
using DataModel;

namespace IBusinessOrder.CMS
{
    public interface IOPGoodsCatalog
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cart">实体类</param>       
        /// <returns>bool</returns>
        bool Add(ShangPinFenLei catalog);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="catalog">目录</param>
        /// <returns>bool</returns>
        bool Update(ShangPinFenLei catalog);
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
        List<ShangPinFenLei> GetGoodsCatalogAllList();
        /// <summary>
        /// 获取数据子数据
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        List<ShangPinFenLei> GetGoodsCatalogChild(string code);
        /// <summary>
        /// 未停用的数据
        /// </summary>
        /// <returns></returns>
        List<ShangPinFenLei> GetGoodsCatalogNotIsStopList();

     
    }
}

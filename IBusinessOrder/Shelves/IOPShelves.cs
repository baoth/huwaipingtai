using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;
using DataModel.Goods;
using Toolkit.CommonModel;

namespace IBusinessOrder.Shelves
{
    public interface IOPShelves
    {
        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <returns></returns>
       List<PinPai> GetBrandList();
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        List<ShangPin> GetProductList(int pingpaiid);
        /// <summary>
        /// 根据商品和SKU获取对应的商品照片
        /// </summary>
        /// <param name="shangpinid"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        List<ShangJia_ShangPin_Sku_TuTouDto> GetProductPhotoList(int shangpinid, string sku);
        /// <summary>
        /// 保存详细页图头信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool SaveShangJia_Sku_TuTou(List<ShangJia_Sku_TuTou> list);
        /// <summary>
        /// 根据sku码删除
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        bool DeleteShangJia_Sku_TuTou(string sku);

        /// <summary>
        /// 根据门店获取商品上架信息
        /// </summary>
        /// <param name="mendian"></param>
        /// <returns></returns>
        IList<GoodsShelvesDto> GetGoodsShelvesDto(string mendian,int goodsId);
        IList<GoodsShelvesDto> GetGoodsShelvesColor(IList<GoodsShelvesDto> goodsShelvesDto);

        CResult PutawayGoods(string sku);

        CResult SetUpShelves(List<string> sku, string desc,string price);


        List<DataModel.ShangJia_ShangPin_Sku_TuTouDto> GetProductPhotoList(int shangpinid);
        List<string> GetSelectImgByImgkey(string imgKey);
        bool SaveShangJia_ShangPin_TuCe(List<DataModel.ShangJia_ShangPin_TuCe> list);
    }
}

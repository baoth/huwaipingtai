using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Goods;
using DataModel.Order;
using Toolkit.CommonModel;
using Toolkit.Fun;
using Toolkit.Ext;
using BusinessOrder.Enum;
using IBusinessOrder.DeliverGoods;
using Toolkit.Path;
using DataModel.DeliverGoods;
namespace BusinessOrder.DeliverGoods
{
    public class OPDeliverGoods : IDeliverGoods
    {
        public List<OrderGoodsDto> GetDeliverGoodsList(int pageSize, int pageIndex)
        {
            var dbSession = Common.DbFactory.CreateDbSession();
            var sql =string.Format(@"
                                  SELECT 
                                    o.Id,
                                    info.Sku,
                                    info.ImgName,
                                    c.CustomerId,
                                    b.Quantity,
                                    b.Price,
                                    c.CreateDate,
                                    info.ShangPinId
                                FROM
                                    `order` AS o
                                        LEFT JOIN
                                    (SELECT 
                                        SUM(price) AS Price,
                                            SUM(Quantity) AS Quantity,
                                            MAX(Id) AS Id,
                                            OrderId
                                    FROM
                                        ordergoods
                                    GROUP BY OrderId) b ON o.id = b.orderid
                                        LEFT JOIN
                                    customerorder c ON c.Id = o.customerorderid
                                        LEFT JOIN
                                    (SELECT 
                                        f.*, a.ImgName, m.Description, m.FenLeiId, m.ShangPinId
                                    FROM
                                        ordergoods f
                                    LEFT JOIN shangjia_sku_info m ON f.sku = m.sku
                                    LEFT JOIN shangjia_sku_tutou a ON SUBSTRING_INDEX(f.`Sku`, '-', 4) = a.ImgKey
                                        AND (`a`.`Sort` = 1)) info ON info.Id = b.id
	                                 where 1=1 and  o.Status<>'{0}' order by c.createdate desc  limit {1}
                                    ", (int)OrderStatusEnum.Payment, pageSize * pageIndex);//无测试数据 记得将o.Status<>'{0}' 改为等于

            var dt = dbSession.Context.QueryTable(sql);
            var list = dt.ToList<OrderGoodsDto>() as List<OrderGoodsDto>;
          
            return list;
        }


        public OrderShiperDto GetShiperByOrderNo(int orderId)
        {
            var sql =string.Format( @"
            SELECT 
	            a.Id as OrderId,
                b.Shipper as ShiperName,
                b.ShipperPhone as ShiperPhone,
                b.DetailAddress as Address,
                a.CreateDate as ShiperDate
            FROM
                customerorder a
                    LEFT JOIN
                customeraddress b ON a.AddressId = b.Id
            WHERE
                a.Id IN (SELECT 
                        CustomerOrderId
                    FROM
                        `order`
                    WHERE
                        id = '{0}')",orderId);
            var dbSession = Common.DbFactory.CreateDbSession();
            var dt = dbSession.Context.QueryTable(sql);
            var list = dt.ToList<OrderShiperDto>() as List<OrderShiperDto>;
            return list.Count>0?list[0]:null;
        }

        public List<DeliverOrderGoods> GetGoodsByOrderNo(int orderNo)
        {
            var sql =string.Format( @"   select 
               a.Id,
               a.OrderId,
               a.Sku,
               a.Quantity,
               a.Price,
               b.name as Color,
               c.name as Size,
               d.Description 
            from ordergoods a
            left join Yanse b on substring_index(substring_index(`Sku`,'-',-2),'-',1)=b.id
            left join Chima c on substring_index(`Sku`,'-',-1)=c.id
            left join shangjia_sku_info d on a.sku=d.sku   where a.OrderId='{0}'",orderNo);
            var dbSession = Common.DbFactory.CreateDbSession();
            var dt = dbSession.Context.QueryTable(sql);
            var list = dt.ToList<DeliverOrderGoods>() as List<DeliverOrderGoods>;
            return list;
        }


        public CResult SetDeliver(ExpressDto expressDto)
        {
            var sql = string.Format(@"update `order` set ExpressCompany='{0}',
            Expresser='{1}',ExpressPhone='{2}',ExpressNum='{3}',ExpressDateTime='{4}',Status='{5}'
            where Id='{6}'",expressDto.ExpressCompany,expressDto.Expressor,expressDto.ExpressPhone,expressDto.ExpressNo,DateTime.Now,(int)OrderStatusEnum.DeliverGoods);
            var dbSession = Common.DbFactory.CreateDbSession();
            dbSession.Context.ExcuteNoQuery(sql);
            return FunResult.GetSuccess();
        }
    }
}

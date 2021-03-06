﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Cart;
using Common;
using QSmart.Core.Object;
using QSmart.Core.DataBase;
using System.Data;
using Toolkit.Path;
namespace BusinessOrder.Cart
{
   public class OPCart:IOPCart
    {
       public OPCart() { 
       }
        DbSession db = DbFactory.CreateDbSession();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>        
        public bool Add(DataModel.Order.Cart cart, string type)
        {           
            try
            {
                List<string> sqlList = new List<string>();    
                if (type == "2")//立即购买
                {
                   
                    //删除同类商品
                    var sql1 = string.Format("delete from cart where CustomerId='{0}' and Sku='{1}' ",cart.CustomerId, cart.Sku);
                    sqlList.Add(sql1);
                   //修改状态
                    var sql2 = string.Format("update cart set Actived={0} where CustomerId='{1}' ",0,cart.CustomerId);
                    sqlList.Add(sql2);
                   //增加商品
                    var addSql = string.Format("insert cart(Sku,CustomerId,Actived,Quantity) values('{0}','{1}','{2}','{3}') ",cart.Sku,cart.CustomerId,1,cart.Quantity);
                    sqlList.Add(addSql);
                    db.Context.ExcuteNoQuery(sqlList);
                }
                else 
                {
                    //先删除后添加

                    //删除同类商品
                    var sql1 = string.Format("delete from cart where CustomerId='{0}' and Sku='{1}' ",cart.CustomerId, cart.Sku);
                    sqlList.Add(sql1);
                    //增加商品
                    var addSql = string.Format("insert cart(Sku,CustomerId,Actived,Quantity) values('{0}','{1}','{2}','{3}') ",cart.Sku,cart.CustomerId,1,cart.Quantity);
                    sqlList.Add(addSql);
                    db.Context.ExcuteNoQuery(sqlList);
                    
                }
                return true;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private bool IsExists(DataModel.Order.Cart cart)
        {
            //删除同类商品
            var sql= string.Format("select Id from cart where CustomerId='{0}' and Sku='{1}' ",cart.CustomerId, cart.Sku);
            var dt=db.Context.QueryTable(sql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        //删除
        public bool Delete(int Id)
        {
            try
            {
                //DataModel.Order.Cart model = new DataModel.Order.Cart() {Id=Id };
                //db.Context.DeleteEntity(model.CreateQSmartObject());
                //db.Context.SaveChange();
                var sql = string.Format("delete from cart where Id='{0}' ",Id);
                db.Context.ExcuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

       /// <summary>
       /// 获取数据
       /// </summary>
       /// <param name="customerId"></param>
       /// <returns></returns>
        public List<DataModel.View.CartView> CartList(string customerId)
        {
            try
            {
                //QSmartQuery Query = new QSmartQuery();
                //Query.Tables.Add(new QSmartQueryTable());
                //Query.Tables[0].tableName = typeof(DataModel.View.CartView).Name;
                //Query.FilterConditions.Add(new QSmartQueryFilterCondition
                //{
                //    Column = new QSmartQueryColumn { columnName = "CustomerId", dataType = typeof(string) },
                //    Operator = QSmartOperatorEnum.equal,
                //    Values = new List<object> { customerId },
                //    Connector = QSmartConnectorEnum.and
                //});

                //List<DataModel.View.CartView> list = db.Context.QueryEntity<DataModel.View.CartView>(Query);

                var rootPath = PathConfig.GetWebSmallImagPath();
                var sql = string.Format("select Id,Sku,CustomerId,Actived,Quantity,Description,Price,concat('" + rootPath + "',ShangPinId,'/',ImgName) as ImgUrl   from cartView where customerId='{0}' ", customerId);
                DataTable dt = db.Context.QueryTable(sql);
                var list = db.Context.ConversionEntity<DataModel.View.CartView>(dt);
              
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool UpdateQuantity(string customerId, string productId, int quantity)
        {
            try
            {               
                var sql = string.Format("update cart set quantity={0} where CustomerId='{1}' and Sku='{2}' ", quantity, customerId, productId);
                db.Context.ExcuteNoQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public bool UpdateActived(string customerId,Dictionary<string,string> dic)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string,string> item in dic)
                {
                    var sql = string.Format("update cart set Actived={0} where CustomerId='{1}' and Sku='{2}' ; ", item.Value, customerId, item.Key);
                    sb.Append(sql);
                }
                db.Context.ExcuteNoQuery(sb.ToString());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public bool UpdateActived(string customerId, string actived)
        {
            try
            {                
                var sql = string.Format("update cart set Actived={0} where CustomerId='{1}'",actived,customerId);
                db.Context.ExcuteNoQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }


        public string GetDeleteActivedSql(string customerId)
        {
            var sql = string.Format("delete from cart where actived=1 and  customerId='{0}' ", customerId);
            return sql;
        }

        public List<DataModel.View.CartView> CartActivedList(string customerId)
        {
         
            var sql = string.Format("select Id,Sku,CustomerId,Actived,Quantity,Description,Price  from cartView where actived=1 and  customerId='{0}' ", customerId);
            DataTable dt= db.Context.QueryTable(sql);
            var list= db.Context.ConversionEntity<DataModel.View.CartView>(dt);           
            return list;
        }
    }
}

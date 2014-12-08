using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBusinessOrder.Cart;
using Common;
using QSmart.Core.Object;
using QSmart.Core.DataBase;
namespace BusinessOrder.Cart
{
   public class OPCart:IOPCart
    {
        DbSession db = DbFactory.CreateDbSession();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public bool Add(DataModel.Order.Cart cart)
        {
            try
            {
                db.Context.InsertEntity(cart.CreateQSmartObject());
                db.Context.SaveChange();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public bool Update(DataModel.Order.Cart cart)
        
        {
            try
            {
                db.Context.ModifyEntity(cart.CreateQSmartObject());
                db.Context.SaveChange();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        //删除
        public bool Delete(int Id)
        {
            try
            {
                DataModel.Order.Cart model = new DataModel.Order.Cart() {Id=Id };
                db.Context.ModifyEntity(model.CreateQSmartObject());
                db.Context.SaveChange();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        public List<DataModel.View.CartView> CartList(int customerId)
        {
            try
            {
                QSmartQuery Query = new QSmartQuery();
                Query.Tables.Add(new QSmartQueryTable());
                Query.Tables[0].tableName = typeof(DataModel.View.CartView).Name;
                Query.FilterConditions.Add(new QSmartQueryFilterCondition
                {
                    Column = new QSmartQueryColumn { columnName = "CustomerId", dataType = typeof(int) },
                    Operator = QSmartOperatorEnum.equal,
                    Values = new List<object> { customerId },
                    Connector = QSmartConnectorEnum.and
                });

                List<DataModel.View.CartView> list = db.Context.QueryEntity<DataModel.View.CartView>(Query);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

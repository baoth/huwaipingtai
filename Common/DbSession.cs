using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.DataBase;
using QSmart.Core.Object;
using System.Data;
using System.Reflection;

namespace Common
{
    public abstract class DbSession : QSmartSession
    {
        protected DbSession(string name) : base(name) { }

        /// <summary>
        /// 通过唯一键获取模型对象
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="UniqueKeyName">唯一键名称</param>
        /// <param name="UniqueKeyValue">唯一键值</param>
        /// <returns>模型对象</returns>
        public T Retrieve<T>(string UniqueKeyName, object UniqueKeyValue) where T : QSmartEntity
        {
            QSmartQuery Query = new QSmartQuery();
            Query.Tables.Add(new QSmartQueryTable());
            Query.Tables[0].tableName = typeof(T).Name;
            Query.FilterConditions.Add(new QSmartQueryFilterCondition());
            Query.FilterConditions[0].Column = new QSmartQueryColumn();
            Query.FilterConditions[0].Column.columnName = UniqueKeyName;
            Query.FilterConditions[0].Column.dataType = UniqueKeyValue.GetType();
            Query.FilterConditions[0].Operator = QSmartOperatorEnum.equal;
            Query.FilterConditions[0].Values.Add(UniqueKeyValue);
            var results = this.Context.QueryEntity<T>(Query);
            return results.Count == 0 ? null : results[0];
        }

        /// <summary>
        /// 判断是否存在实例
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="UniqueKeyName">唯一键名称</param>
        /// <param name="UniqueKeyValue">唯一键值</param>
        /// <returns>true,存在 false,不存在</returns>
        public bool Exists<T>(string UniqueKeyName, object UniqueKeyValue) where T : QSmartEntity
        {
            QSmartQuery Query = new QSmartQuery();
            Query.Tables.Add(new QSmartQueryTable());
            Query.Tables[0].tableName = typeof(T).Name;
            Query.FilterConditions.Add(new QSmartQueryFilterCondition());
            Query.FilterConditions[0].Column = new QSmartQueryColumn();
            Query.FilterConditions[0].Column.columnName = UniqueKeyName;
            Query.FilterConditions[0].Column.dataType = UniqueKeyValue.GetType();
            Query.FilterConditions[0].Operator = QSmartOperatorEnum.equal;
            Query.FilterConditions[0].Values.Add(UniqueKeyValue);
            DataTable dt = this.Context.QueryTable(Query);
            return dt.Rows.Count > 0 ? true : false;
        }


        public T ExistsEnt<T>(string UniqueKeyName, object UniqueKeyValue) where T : QSmartEntity
        {
            QSmartQuery Query = new QSmartQuery();
            Query.Tables.Add(new QSmartQueryTable());
            Query.Tables[0].tableName = typeof(T).Name;
            Query.FilterConditions.Add(new QSmartQueryFilterCondition());
            Query.FilterConditions[0].Column = new QSmartQueryColumn();
            Query.FilterConditions[0].Column.columnName = UniqueKeyName;
            Query.FilterConditions[0].Column.dataType = UniqueKeyValue.GetType();
            Query.FilterConditions[0].Operator = QSmartOperatorEnum.equal;
            Query.FilterConditions[0].Values.Add(UniqueKeyValue);
            var results = this.Context.QueryEntity<T>(Query);
            return results == null || results.Count == 0 ? null : results[0];

        }
        public bool ExistsEnt<T>(string UniqueKeyName, object UniqueKeyValue, List<QSmartQueryFilterCondition> listQFilter) where T : QSmartEntity
        {
            QSmartQuery Query = new QSmartQuery();
            Query.Tables.Add(new QSmartQueryTable());
            Query.Tables[0].tableName = typeof(T).Name;
            Query.FilterConditions.AddRange(listQFilter);
            Query.FilterConditions.Add(new QSmartQueryFilterCondition
            {
                Column = new QSmartQueryColumn { columnName = UniqueKeyName, dataType = UniqueKeyValue.GetType() },
                Operator = QSmartOperatorEnum.equal,
                Values = new List<object> { UniqueKeyValue },
                Connector = QSmartConnectorEnum.and
            });

            DataTable dt = this.Context.QueryTable(Query);
            return dt.Rows.Count > 0 ? true : false;

        }
        /// <summary>
        /// 获取分页数据集合
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="PageStart">起始行index</param>
        /// <param name="PageLength">一页需要显示多少条记录</param>
        /// <param name="Conditions">过滤条件</param>
        /// <param name="OrderBys">排序条件</param>
        /// <param name="TotalCount">返回总条数</param>
        /// <returns>数据集合</returns>
        public List<T> PaginationRetrieve<T>(int PageStart, int PageLength, List<QSmartQueryFilterCondition> Conditions
            , Dictionary<QSmartQueryColumn, QSmartOrderByEnum> OrderBys, out int TotalCount)
            where T : QSmartEntity
        {
            TotalCount = 0;

            QSmartQuery QueryA = new QSmartQuery();
            QueryA.Tables.Add(new QSmartQueryTable());
            QueryA.Tables[0].tableName = typeof(T).Name;
            QueryA.TopSetting.Effective = true;
            QueryA.TopSetting.Value = PageLength;
            QueryA.TopSetting.BeginValue = PageStart;
            QueryA.TopSetting.PrimaryKeyName = GetPrimaryKeyName<T>();

            if (Conditions != null && Conditions.Count > 0)
            {
                foreach (QSmartQueryFilterCondition fc in Conditions) QueryA.FilterConditions.Add(fc);
            }
            if (OrderBys != null && OrderBys.Count > 0)
            {
                foreach (QSmartQueryColumn qc in OrderBys.Keys) QueryA.OrderBys.Add(qc, OrderBys[qc]);
            }


            QSmartQuery QueryB = new QSmartQuery();
            QueryB.Tables.Add(new QSmartQueryTable());
            QueryB.Tables[0].tableName = typeof(T).Name;
            QueryB.CountSetting.Effective = true;
            QueryB.CountSetting.AliasName = "TotalCount";
            if (Conditions != null && Conditions.Count > 0)
            {
                foreach (QSmartQueryFilterCondition fc in Conditions) QueryB.FilterConditions.Add(fc);
            }

            QSmartQuery Query = new QSmartQuery();
            Query.Tables.Add(new QSmartQueryTable
            {
                aliasName = "a",
                joinType = QSmartJoinEnum.comma,
                tableNameCreator = QueryA
            });

            Query.Tables.Add(new QSmartQueryTable
            {
                aliasName = "b",

                tableNameCreator = QueryB
            });
            DataTable dt = this.Context.QueryTable(Query);
            if (dt != null && dt.Rows.Count > 0) TotalCount = Convert.ToInt32(dt.Rows[0]["TotalCount"]);

            return this.Context.ConversionEntity<T>(dt);
        }

        /// <summary>
        /// 获取模型主键名称
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <returns>主键名称</returns>
        private string GetPrimaryKeyName<T>() where T : QSmartEntity
        {
            Type mtype = typeof(T);
            PropertyInfo[] infos = mtype.GetProperties();
            for (int i = 0; i < infos.Length; i++)
            {
                PropertyInfo pi = infos[i];
                object[] cas = pi.GetCustomAttributes(typeof(PrimaryKeyAttribute), false);
                if (cas.Count() > 0) return pi.Name;
            }
            return string.Empty;
        }
    }

    class DbSessionInstance : DbSession
    {
        public DbSessionInstance() : base("db") { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace huwaipingtai.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult pay()
        {
            return View("pay");
        }
        public ActionResult delivery()
        {
            return View("delivery");
        }
        public ActionResult invoice()
        {
            return View("invoice");
        }
        public ActionResult courier()
        {
            return View("courier");
        }
        public ActionResult Address()
        {
            return View("address");
        }
        /// <summary>
        /// 地址信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAddress()
        {
            var json = TestComFun.GetJson();
            return Content(json);
        }
    }
    /// <summary>
    /// 收货地址
    /// </summary>
    public class ShippingAddress
    {
        public string ConsumerName { set; get; }
        public string Phone { set; get; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { set; get; }
        public string City { set; get; }
        /// <summary>
        /// 区县
        /// </summary>
        public string County { set; get; }
        public string DetailAddress { set; get; }

    }
    public class TestComFun
    {
        public static DataTable NewDataTable(List<string> columns)
        {
            DataTable dt = new DataTable();
            foreach (string col in columns)
            {
                dt.Columns.Add(col);
            }
            return dt;
        }
        public static DataTable GetAddressData()
        {
            string[] colArr = new string[] { "ConsumerName", "Phone", "Province", "City", "County", "DetailAddress" };
            DataTable dt = NewDataTable(colArr.ToList());
            DataRow row = dt.NewRow();
            row["ConsumerName"] = "小王";
            row["Phone"] = "12345678912";
            row["Province"] = "北京";
            row["City"] = "朝阳";
            row["County"] = "三环以内";
            row["DetailAddress"] = "大望路 建国路 5号院 1107室";
            dt.Rows.Add(row);
            return dt;
        }
        public static string GetJson()
        { 
            DataTable dt=GetAddressData();
            string strJson = DataTableToJson(dt);
            return strJson;
        }
        public static string DataTableToJson(DataTable dt)
        {
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
            jsonBuilder.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    jsonBuilder.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        jsonBuilder.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + stringToJson(dt.Rows[i][j].ToString()) + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            jsonBuilder.Append(",");
                        }
                    }
                    jsonBuilder.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        jsonBuilder.Append(",");
                    }
                }
            }
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String stringToJson(String s)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {

                char c = s[i];
                switch (c)
                {

                    case '\'':
                        sb.Append("\\\'");
                        break;
                    case '"':
                        sb.Append("\\\"");
                        break;
                    case '\b':      //退格
                        sb.Append("\\b");
                        break;
                    case '\f':      //走纸换页
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n"); //换行    
                        break;
                    case '\r':      //回车
                        sb.Append("\\r");
                        break;
                    case '\t':      //横向跳格
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            sb.Replace("\r\n", "<br/>");
            var temp = System.Text.RegularExpressions.Regex.Replace(sb.ToString(), "\\s{2,}", "");//利用正则去掉key中间的空格
            return temp;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Configuration;
using Toolkit.JsonHelp;
using DataModel;
namespace CMS.Models
{
    public class TreeModel
    {
        public string id {set;get;}
        public string text{set;get;}
        public string iconCls{set;get;}
        public string state{set;get;}
        public object attributes { set; get; }
        public List<TreeModel> children{set;get;}

    }
    public class TreeData
    {
        /// <summary>
        /// 分类目录树
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetGoodsCatalogTree(List<GoodsCatalog> list)
        { 
            string jsonStr=string.Empty;
            List<TreeModel> treeModelList = new List<TreeModel>();
            var pList = list.FindAll(e => e.PCode == null || e.PCode == "").ToList();
            foreach (GoodsCatalog item in pList)
            {
                TreeModel treemodel = new TreeModel();
                treemodel.id = item.Id.ToString();
                treemodel.text = item.Name;
                treemodel.state = "closed";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["id"] = item.Id.ToString();
                dic["code"]=item.Code;
                dic["level"] = item.Level.ToString();
                dic["name"] = item.Name;
                dic["isstop"] = item.IsStop.ToString();
                dic["pcode"] = item.PCode==null?"":item.PCode.ToString();
                treemodel.attributes = dic;
                var childList=list.FindAll(e=>e.PCode==item.Code);
                if (childList != null && childList.Count > 0)
                {
                    var childData = ChildGoodsCatalogList(list, childList);
                    if (childData != null && childData.Count > 0)
                    {
                        treemodel.children = childData;
                    }
                }
                else
                {
                    treemodel.state = "open";
                }
                treeModelList.Add(treemodel);
            }

            jsonStr =JsonHelp.objectToJson(treeModelList);
           return jsonStr;
        }
        /// <summary>
        /// 子项数据
        /// </summary>
        /// <param name="list">原始数据列表</param>
        /// <param name="childList">子项数据列表</param>
        /// <returns></returns>
        private static List<TreeModel> ChildGoodsCatalogList(List<GoodsCatalog> list, List<GoodsCatalog> childList)
        {
            List<TreeModel> treeModelList = new List<TreeModel>() ;
            if (childList == null || childList.Count == 0) return null;
            foreach (GoodsCatalog item in childList)
            {
                TreeModel treeModel=new TreeModel ();
                treeModel.id = item.Id.ToString();
                treeModel.text = item.Name;
                treeModel.state = "closed";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["id"] = item.Id.ToString();
                dic["code"] = item.Code;
                dic["level"] = item.Level.ToString();
                dic["name"] = item.Name;
                dic["isstop"] = item.IsStop.ToString();
                dic["pcode"] =item.PCode==null?"": item.PCode.ToString();
                treeModel.attributes = dic;
                var child = list.FindAll(e=>e.PCode==item.Code);
                if (child != null && child.Count > 0)
                {
                    var childData = ChildGoodsCatalogList(list, child);
                    if (childData != null && childData.Count > 0)
                    {
                        treeModel.children = childData;
                    }
                }
                else
                {
                    treeModel.state = "open";
                }
                treeModelList.Add(treeModel);
            }
            return treeModelList;
        }


      
    }
   

   
}
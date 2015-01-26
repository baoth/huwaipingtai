using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QSmart.Weixin.Core;
using Log;

namespace Toolkit
{
    public static class WeixinAdaptor
    {
        public const string API密钥 = "5ded26c2550f4c76bb8399aade46f168";
        public const string MchId = "1228373802";
        public const string Token = "tiantian315";
        public const string AppId = "wxadcc12090e7138b3";
        public const string AppSecret = "722dfaf58953c47ffd9ac9548c727461";//vUCeU6wEImFjM9oo3JgjfZ6nHXQMz8yhIoPdUjRECLl
        public const string authurl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxadcc12090e7138b3&redirect_uri=http%3A%2F%2Fwww.baoth.com%2fHome&response_type=code&scope=snsapi_base&state=123#wechat_redirect";

        public static WeixinCore CreateWeixinCore()
        {
            return new WeixinCore(WeixinAdaptor.AppId, WeixinAdaptor.AppSecret, WeixinAdaptor.Token, System.Configuration.ConfigurationManager.AppSettings);
        }
        public static string GetWxMessage(string fromUserName,string toUserName,string context){
            Logger.Write("发消息");
             ReplyWeixinNewsMessage rnm = new ReplyWeixinNewsMessage();
             rnm.FromUserName = fromUserName;
            rnm.ToUserName = toUserName;
            rnm.CreateTime = WeixinCoreExtension.GetTimeStamp(DateTime.Now);
            ArticleItem item = new ArticleItem();
            item.Title ="首页";
            item.Description = "欢迎光临1";
            item.PicUrl = "http://test.nkwang.cn/Product/Images/n1/test/9b839728-d085-468e-acb8-88eb9eb008b8.jpg";
            item.Url = "http://test.nkwang.cn?idt=wx&sid=" + toUserName;
            rnm.Articles.Add(item);
            Logger.Write("准备返回"+toUserName);
            return rnm.GetReplyMessage();
        }

        
    }
}
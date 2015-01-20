using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Common.Config;
using Newtonsoft.Json;
using Log;

namespace Common.Weixin
{
       public class AccessToken
       {
           private long _expiresTime;


           public string access_token { get; set; }

           public long expires_in
           {
               get { return _expiresTime; }
               set
               {
                   _expiresTime = value;
                   ExpireDate = DateTime.UtcNow.AddSeconds(expires_in);
               }
           }

           public bool IsExpired
           {
               get
               {
                   return ExpireDate >= DateTime.UtcNow;
               }
           }
           private DateTime ExpireDate { get; set; }

           public AccessToken Renew()
           {
               //access_token = "fJmIR_d71GFtNf4oAtMbOXKMMGjc4JJUo_bZ8gf1coNkJ2Sc3koI0u_DvbdQawid";
               RenewToken();
               return this;
           }

    
           private void RenewToken()
           {

               //https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
               var client = new RestClient(WeixinConfig.WeixinBaseUrl);
               var request = new RestRequest("cgi-bin/token", Method.GET);
               request.AddParameter("grant_type", "client_credential");
               request.AddParameter("appid", WeixinConfig.CorpId);
               request.AddParameter("secret", WeixinConfig.CorpSecret);
               var clientResult = client.Execute(request);
               var response = clientResult.Content;
               var resObject = JsonConvert.DeserializeObject<dynamic>(response);
               if (string.IsNullOrEmpty(resObject+"")&&resObject.errorCode != null)
               {
                   Logger.Write(resObject.errcode + ":" + resObject.errmsg);
                   return;
               }
               access_token = resObject.access_token;
               expires_in = 7200;
           }

       }
}

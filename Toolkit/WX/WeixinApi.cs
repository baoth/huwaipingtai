using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Common.Config;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Log;

namespace Common.Weixin
{
  public  class WeixinApi
    {

      public static string CreateMenuByStream(string menuInfo) {
          string postUrl = WeixinConfig.WeixinBaseUrl+"cgi-bin/menu/create?access_token={0}";
          var token =WxServiceHelper.Token;
          if (token == null)
          {
              Logger.Write("token is empty");
              return "";
          }
          postUrl = string.Format(postUrl,token.access_token);

          return PostWebRequest(postUrl, menuInfo);
      }
      private static string PostWebRequest(string postUrl, string menuInfo)
      {
          string returnValue = string.Empty;
          try
          {
              byte[] byteData = Encoding.UTF8.GetBytes(menuInfo);
              Uri uri = new Uri(postUrl);
              HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(uri);
              webReq.Method = "POST";
              webReq.ContentType = "application/x-www-form-urlencoded";
              webReq.ContentLength = byteData.Length;
              //定义Stream信息
              Stream stream = webReq.GetRequestStream();
              stream.Write(byteData, 0, byteData.Length);
              stream.Close();
              //获取返回信息
              HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
              StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);
              returnValue = streamReader.ReadToEnd();
              //关闭信息
              streamReader.Close();
              response.Close();
              stream.Close();
          }
          catch (Exception ex)
          {
          }
          return returnValue;
      }
      public static bool CreateMenu(string menuJsonData) { 
       // https://api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN
          var client = new RestClient(WeixinConfig.WeixinBaseUrl);
          var token =WxServiceHelper.Token;
          if (token == null)
          {
              Logger.Write("token is empty");
              return false;
          }
          var notifyRequest = new RestRequest("cgi-bin/menu/create?access_token={access_token}", Method.POST);
          notifyRequest.AddUrlSegment("access_token", token.access_token);
          notifyRequest.AddBody(menuJsonData);
          var clientResult= client.Execute(notifyRequest);
          var response = clientResult.Content;
          var resObject = JsonConvert.DeserializeObject<dynamic>(response);
          if (string.IsNullOrEmpty(resObject + "") && resObject.errorCode != null)
          {
              Logger.Write(resObject.errcode + ":" + resObject.errmsg);
              return false;
          }
          return true;
      }
      public static bool DeleteMenu() { 
            var client = new RestClient(WeixinConfig.WeixinBaseUrl);
          var token = new { access_token = "kwjiT4u8ZnBX-d3D_e3PDO2SepIS9mUQFpnDhxh4YYxda4B6aUUMIpAEL2QPHOyjx-GrCg4kCH1VTqPVaZWqATSurG5s5IoCYektEU6BslQ" };// WxServiceHelper.Token;
          if (token == null)
          {
              Logger.Write("token is empty");
              return false;
          }
      https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=ACCESS_TOKEN
        var notifyRequest = new RestRequest("cgi-bin/menu/delete?access_token={access_token}", Method.GET);
      notifyRequest.AddUrlSegment("access_token", token.access_token);
      var clientResult = client.Execute(notifyRequest);
      var response = clientResult.Content;
      var resObject = JsonConvert.DeserializeObject<dynamic>(response);
      if (string.IsNullOrEmpty(resObject + "") && resObject.errorCode != null)
      {
          Logger.Write(resObject.errcode + ":" + resObject.errmsg);
          return false;
      }
      return true;
      }

      public static bool SendMessage(dynamic requestData, Action<dynamic> successCallback, Action<dynamic> failCallback)
      {
          var client = new RestClient(WeixinConfig.WeixinBaseUrl);
          var token = WxServiceHelper.Token;
          if (token == null)
          {
              Logger.Write("token is empty");
              return false;
          }
          //https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=ACCESS_TOKEN
          var notifyRequest = new RestRequest("cgi-bin/message/send?access_token={access_token}", Method.POST);
          notifyRequest.RequestFormat = DataFormat.Json;
          notifyRequest.AddUrlSegment("access_token", token.access_token);
          notifyRequest.AddBody(requestData);
          var notifyResponse = JsonConvert.DeserializeObject<dynamic>(client.Execute(notifyRequest).Content);

          if (notifyResponse.errcode == 0)
          {
              if (successCallback != null)
                  successCallback(notifyResponse);
              return true;
          }
          else
          {
              if (failCallback != null)
                  failCallback(notifyResponse);
              return false;
          }


      }
      //或者媒体文件接口
      //https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token=ACCESS_TOKEN&media_id=MEDIA_ID
      public static bool GetPath(string mediaId) 
      {
          var client = new RestClient(WeixinConfig.WeixinBaseUrl);
          var token = WxServiceHelper.Token;
          if (token == null)
          {
              Logger.Write("token is empty");
              return false;
          }
          var notifyRequest = new RestRequest("cgi-bin/media/get", Method.GET);
          notifyRequest.AddParameter("access_token", token.access_token);
          notifyRequest.AddParameter("media_id", mediaId);
        
          var notifyResponse = JsonConvert.DeserializeObject<dynamic>(client.Execute(notifyRequest).Content);

          if (notifyResponse.errcode == 0)
          {
              return true;
          }
          else
          {
              return false;
          }
      }
      //上传媒体文件
      //https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token=ACCESS_TOKEN&type=TYPE
      public static string UploadFile(string strFileName,string strFilePath) 
      {
          var client = new RestClient(WeixinConfig.WeixinBaseUrl);
          var token = WxServiceHelper.Token;
          if (token == null)
          {
              Logger.Write("token is empty");
          }
          FileStream fileStream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
          BinaryReader br = new BinaryReader(fileStream);
          byte[] buffer = br.ReadBytes(Convert.ToInt32(fileStream.Length));
          var notifyRequest = new RestRequest("cgi-bin/media/upload?access_token={access_token}", Method.POST);
          notifyRequest.RequestFormat = DataFormat.Json;
          notifyRequest.AddUrlSegment("access_token", token.access_token);
          notifyRequest.AddUrlSegment("type", "image");
          //notifyRequest.AlwaysMultipartFormData = true;
          notifyRequest.AddHeader("Content-Disposition", "form-data");
          notifyRequest.AddHeader("name", strFileName);
          notifyRequest.AddHeader("filename", "form-data");
          notifyRequest.AddHeader("filelength", buffer.Length.ToString());
          notifyRequest.AddHeader("content-type", "image/jpeg");

          notifyRequest.AddFile("media",strFilePath);
          var notifyResponse = JsonConvert.DeserializeObject<dynamic>(client.Execute(notifyRequest).Content);
          return "";

      }
      private static bool RemoteCertificateValidate(object sender, X509Certificate cert,

X509Chain chain, SslPolicyErrors error)
      {
          // trust any certificate!!!
          System.Console.WriteLine("Warning, trust any certificate");
          //为了通过证书验证，总是返回true
          return true;
      }
      /// <summary>
      /// 服务号：上传多媒体文件
      /// </summary>
      /// <param name="accesstoken">调用接口凭据</param>
      /// <param name="type">图片（image）、语音（voice）、视频（video）和缩略图（thumb）</param>
      /// <param name="filename">文件路径</param>
      /// <param name="contenttype">文件Content-Type类型(例如：image/jpeg、audio/mpeg)</param>
      /// <returns></returns>
      public static string UploadFile(string filename)
        {
            //var client = new RestClient(WeixinConfig.WeixinBaseUrl);
            var token = WxServiceHelper.Token;
            //文件
            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fileStream);
            byte[] buffer = br.ReadBytes(Convert.ToInt32(fileStream.Length));

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            //请求
            WebRequest req = WebRequest.Create(WeixinConfig.WeixinBaseUrl + @"cgi-bin/media/upload?access_token=" + token.access_token + "&type=image");
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;//验证服务器证书回调自动验证
            req.Method = "POST";
            req.ContentType = "multipart/form-data; boundary=" + boundary;
            //组织表单数据
            StringBuilder sb = new StringBuilder();
            sb.Append("--" + boundary+"\r\n");
            sb.Append("Content-Disposition: form-data; name=\"media\"; filename=\"" + filename + "\"; filelength=\""+fileStream.Length+"\"");
            sb.Append("\r\n");
            sb.Append("Content-Type:image/jpeg ");
            sb.Append("\r\n\r\n");
            string head = sb.ToString();
            byte[] form_data = Encoding.UTF8.GetBytes(head);

            //结尾
            byte[] foot_data = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            //post总长度
            long length = form_data.Length + fileStream.Length + foot_data.Length;

            req.ContentLength = length;

            Stream requestStream = req.GetRequestStream();
            //这里要注意一下发送顺序，先发送form_data > buffer > foot_data
            //发送表单参数
            requestStream.Write(form_data, 0, form_data.Length);
            //发送文件内容
            requestStream.Write(buffer,0,buffer.Length);
            //结尾
            requestStream.Write(foot_data, 0, foot_data.Length);

            requestStream.Close();
            fileStream.Close();
            fileStream.Dispose();
            br.Close();
            br.Dispose();
            //响应
            WebResponse pos = req.GetResponse();
            StreamReader sr = new StreamReader(pos.GetResponseStream(), Encoding.UTF8);
            string html = sr.ReadToEnd().Trim();
            sr.Close();
            sr.Dispose();
            if (pos != null)
            {
                pos.Close();
                pos = null;
            }
            if (req != null)
            {
                req = null;
            }
            return html;
        }
    }

  //上传文件类型
  public enum UploadType 
  {
      image=1,
      voice=2,
      video=3,
      file=4
  }
}

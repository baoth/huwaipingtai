using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Toolkit.Fun
{
   public class VerifyCode
    {

       private String GetRandomint(int codeCount)
       {
           Random random = new Random();
           string min = "";
           string max = "";
           for (int i = 0; i < codeCount; i++)
           {
               min += "1";
               max += "9";
           }
           return (random.Next(Convert.ToInt32(min), Convert.ToInt32(max)).ToString());
       }
       private string CreateRandomCode(int codeCount)
       {
           string allChar = "0,1,2,3,4,5,6,7,8,9";
           string[] allCharArray = allChar.Split(',');
           string randomCode = "";
           int temp = -1;
           Random rand = new Random();
           for (int i = 0; i < codeCount; i++)
           {
               if (temp != -1)
               {
                   rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
               }
               int t = rand.Next(9);
               if (temp == t)
               {
                   return CreateRandomCode(codeCount);
               }
               temp = t;
               randomCode += allCharArray[t];
           }
           return randomCode;
       }
       //组合
       private string CreateRandomCodeZ(int codeCount)
       {
           string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
           string[] allCharArray = allChar.Split(',');
           string randomCode = "";
           int temp = -1;
           Random rand = new Random();
           for (int i = 0; i < codeCount; i++)
           {
               if (temp != -1)
               {
                   rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
               }
               int t = rand.Next(61);
               if (temp == t)
               {
                   return CreateRandomCodeZ(codeCount);
               }
               temp = t;
               randomCode += allCharArray[t];
           }
           return randomCode;
       }
       /**/
       /*
            此函数在汉字编码范围内随机创建含两个元素的十六进制字节数组，每个字节数组代表一个汉字，并将
            四个字节数组存储在object数组中。
            参数：strlength，代表需要产生的汉字个数
        */
       public static object[] CreateRegionCode(int strlength)
       {
           //定义一个字符串数组储存汉字编码的组成元素
           string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
           Random rnd = new Random();
           //定义一个object数组用来
           object[] bytes = new object[strlength];
           /**/
           /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中
       每个汉字有四个区位码组成
       区位码第1位和区位码第2位作为字节数组第一个元素
       区位码第3位和区位码第4位作为字节数组第二个元素
       */
           for (int i = 0; i < strlength; i++)
           {
               //区位码第1位
               int r1 = rnd.Next(11, 14);
               string str_r1 = rBase[r1].Trim();
               //区位码第2位
               rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的种子避免产生重复值
               int r2;
               if (r1 == 13)
               {
                   r2 = rnd.Next(0, 7);
               }
               else
               {
                   r2 = rnd.Next(0, 16);
               }
               string str_r2 = rBase[r2].Trim();
               //区位码第3位
               rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
               int r3 = rnd.Next(10, 16);
               string str_r3 = rBase[r3].Trim();
               //区位码第4位
               rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
               int r4;
               if (r3 == 10)
               {
                   r4 = rnd.Next(1, 16);
               }
               else if (r3 == 15)
               {
                   r4 = rnd.Next(0, 15);
               }
               else
               {
                   r4 = rnd.Next(0, 16);
               }
               string str_r4 = rBase[r4].Trim();
               //定义两个字节变量存储产生的随机汉字区位码
               byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
               byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
               //将两个字节变量存储在字节数组中
               byte[] str_r = new byte[] { byte1, byte2 };
               //将产生的一个汉字的字节数组放入object数组中
               bytes.SetValue(str_r, i);
           }
           return bytes;
       }
       public static object[] CreateRegionCode1(int strlength)
       {
           //定义一个字符串数组储存汉字编码的组成元素
           string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
           Random rnd = new Random();
           //定义一个object数组用来
           object[] bytes = new object[strlength];
           /**/
           /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中
       每个汉字有四个区位码组成
       区位码第1位和区位码第2位作为字节数组第一个元素
       区位码第3位和区位码第4位作为字节数组第二个元素
       */
           for (int i = 0; i < strlength; i++)
           {
               //区位码第1位
               int r1 = rnd.Next(11, 14);
               string str_r1 = rBase[r1].Trim();
               //区位码第2位
               rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的种子避免产生重复值
               int r2;
               if (r1 == 13)
               {
                   r2 = rnd.Next(0, 7);
               }
               else
               {
                   r2 = rnd.Next(0, 16);
               }
               string str_r2 = rBase[r2].Trim();
               //区位码第3位
               rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
               int r3 = rnd.Next(10, 16);
               string str_r3 = rBase[r3].Trim();
               //区位码第4位
               rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
               int r4;
               if (r3 == 10)
               {
                   r4 = rnd.Next(1, 16);
               }
               else if (r3 == 15)
               {
                   r4 = rnd.Next(0, 15);
               }
               else
               {
                   r4 = rnd.Next(0, 16);
               }
               string str_r4 = rBase[r4].Trim();
               //定义两个字节变量存储产生的随机汉字区位码
               byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
               byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
               //将两个字节变量存储在字节数组中
               byte[] str_r = new byte[] { byte1, byte2 };
               //将产生的一个汉字的字节数组放入object数组中
               bytes.SetValue(str_r, i);
           }
           return bytes;
       }
       private string stxt(int num)
       {
           Encoding gb = Encoding.GetEncoding("gb2312");
           //调用函数产生10个随机中文汉字编码
           object[] bytes = CreateRegionCode1(num);
           string strtxt = "";
           //根据汉字编码的字节数组解码出中文汉字
           for (int i = 0; i < num; i++)
           {
               strtxt += gb.GetString((byte[])Convert.ChangeType(bytes, typeof(byte[])));
           }
           return strtxt;
       }
       /// <summary>
       /// 这个是使用字母,数字混合
       /// </summary>
       /// <param name="VcodeNum"></param>
       /// <returns></returns>
       private string CreateRandomCode1(int codeCount)
       {
           string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
           string[] allCharArray = allChar.Split(',');
           string randomCode = "";
           int temp = -1;
           Random rand = new Random();
           for (int i = 0; i < codeCount; i++)
           {
               if (temp != -1)
               {
                   rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
               }
               int t = rand.Next(61);
               if (temp == t)
               {
                   return CreateRandomCode1(codeCount);
               }
               temp = t;
               randomCode += allCharArray[t];
           }
           return randomCode;
       }
       public byte[] CreateImage(string checkCode)
       {
           int iwidth = (int)(checkCode.Length * 20);
           System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 25);
           Graphics g = Graphics.FromImage(image);
           Font f = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
           Brush b = new System.Drawing.SolidBrush(Color.White);
           //g.FillRectangle(new System.Drawing.SolidBrush(Color.Blue),0,0,image.Width, image.Height);   
           g.Clear(Color.Black);
           g.DrawString(checkCode, f, b, 3, 3);
           Pen blackPen = new Pen(Color.Black, 0);
           Random rand = new Random();
           //for (int i=0;i<5;i++)   
           //{       
           //    int y = rand.Next(image.Height); 
           //    g.DrawLine(blackPen,0,y,image.Width,y);
           //}       
           System.IO.MemoryStream ms = new System.IO.MemoryStream();
           image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
           //Response.ClearContent();
           //Response.ContentType = "image/Jpeg";
           //Response.BinaryWrite(ms.ToArray());
           g.Dispose();
           image.Dispose();
           return ms.ToArray();
       }
    }
}

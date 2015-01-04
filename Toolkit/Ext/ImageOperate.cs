using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
namespace Toolkit.Ext
{
    public class ImageThumbnail
    {
        public Image ResourceImage;
        private int ImageWidth;
        private int ImageHeight;
        public string ErrorMessage;
        public ImageThumbnail(string ImageFileName)
        {
            ResourceImage = Image.FromFile(ImageFileName);
            ErrorMessage = "";
        }
        public void DisImage()
        {
            ResourceImage.Dispose();
        }
        public bool ThumbnailCallback()
        {
            return false;
        }

        // 方法1，按大小
        /// <summary>
        /// 按大小缩放图片
        /// </summary>
        /// <param name="Width">缩放到的宽</param>
        /// <param name="Height">缩放到的高</param>
        /// <param name="targetFilePath">图片的名字</param>
        /// <returns>bool</returns>
        public bool ReducedImage(int Width, int Height, string targetFilePath)
        {
            try
            {
                Image ReducedImage;
                Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                ReducedImage = ResourceImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);
                ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);
                ReducedImage.Dispose();
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
        }

        // 方法2，按百分比 缩小60% Percent为0.6 targetFilePath为目标路径
        /// <summary>
        /// 按百分比缩放
        /// </summary>
        /// <param name="Percent">小数：0.4表示百分之40</param>
        /// <param name="targetFilePath">图片的名称</param>
        /// <returns>bool</returns>
        public bool ReducedImage(double Percent, string targetFilePath)
        {
            try
            {
                Image ReducedImage;
                Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                ImageWidth = Convert.ToInt32(ResourceImage.Width * Percent);
                ImageHeight = (ResourceImage.Height) * ImageWidth / ResourceImage.Width;//等比例缩放
                ReducedImage = ResourceImage.GetThumbnailImage(ImageWidth, ImageHeight, callb, IntPtr.Zero);
                ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);
                ReducedImage.Dispose();
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
        }
        #region GetPicThumbnail 无损压缩图片为指定大小，质量
        /// <summary> 
        /// 无损压缩图片为指定大小，质量 
        /// </summary> 
        /// <param name="ms">原图片</param> 
        /// <param name="dFile">压缩后保存位置</param> 
        /// <param name="dHeight">高度</param> 
        /// <param name="dWidth"></param> 
        /// <param name="flag">压缩质量 1-100</param> 
        /// <returns></returns> 

        public bool ReducedImageByPercent(string dFile, int dHeight, int dWidth, int flag)
        {
            System.Drawing.Image iSource = ResourceImage;

            ImageFormat tFormat = iSource.RawFormat;

            int sW = 0, sH = 0;

            //按比例缩放 

            Size tem_size = new Size(iSource.Width, iSource.Height);

            if (tem_size.Width > dHeight || tem_size.Width > dWidth) //将**改成c#中的或者操作符号 
            {

                if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                {

                    sW = dWidth;

                    sH = (dWidth * tem_size.Height) / tem_size.Width;

                }

                else
                {

                    sH = dHeight;

                    sW = (tem_size.Width * dHeight) / tem_size.Height;

                }

            }

            else
            {

                sW = tem_size.Width;

                sH = tem_size.Height;

            }

            Bitmap ob = new Bitmap(dWidth, dHeight);

            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);

            g.CompositingQuality = CompositingQuality.HighQuality;

            g.SmoothingMode = SmoothingMode.HighQuality;

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量 

            EncoderParameters ep = new EncoderParameters();

            long[] qy = new long[1];

            qy[0] = flag;//设置压缩的比例1-100 

            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);

            ep.Param[0] = eParam;

            try
            {

                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();

                ImageCodecInfo jpegICIinfo = null;

                for (int x = 0; x < arrayICI.Length; x++)
                {

                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {

                        jpegICIinfo = arrayICI[x];

                        break;

                    }

                }

                if (jpegICIinfo != null)
                {

                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径 

                }

                else
                {

                    ob.Save(dFile, tFormat);

                }

                return true;

            }

            catch
            {

                return false;

            }

            finally
            {

                iSource.Dispose();

                ob.Dispose();

            }
        }
        #endregion
        public bool ReducedImageByPercent(int fitWidth, int fitHeight, string targetFilePath)
        {
            try
            {
                ReducedImageByPercent(targetFilePath, fitHeight, fitWidth, 99);
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
        }
    }
}
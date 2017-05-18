using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace NLayer.MVC_PAL.Util
{
    public class ImageHelper
    {
        public byte[] CropImage(byte[] content, int x, int y, int width, int height)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                return CropImage(stream, x, y, width, height);
            }
        }

        public byte[] CropImage(Stream content, int x, int y, int width, int height)
        {
            //Parsing stream to bitmap
            using (Bitmap sourceBitmap = new Bitmap(content))
            {
                //Get new dimensions
                double sourceWidth = Convert.ToDouble(sourceBitmap.Size.Width);
                double sourceHeight = Convert.ToDouble(sourceBitmap.Size.Height);
                Rectangle cropRect = new Rectangle(x, y, width, height);

                //Creating new bitmap with valid dimensions
                using (Bitmap newBitMap = new Bitmap(cropRect.Width, cropRect.Height))
                {
                    using (Graphics g = Graphics.FromImage(newBitMap))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        g.DrawImage(sourceBitmap, new Rectangle(0, 0, newBitMap.Width, newBitMap.Height), cropRect, GraphicsUnit.Pixel);

                        return GetBitmapBytes(newBitMap);
                    }
                }
            }
        }

        public byte[] GetBitmapBytes(Bitmap source)
        {
            //Settings to increase quality of the image
            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders()[4];
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            //Temporary stream to save the bitmap
            using (MemoryStream tmpStream = new MemoryStream())
            {
                source.Save(tmpStream, codec, parameters);

                //Get image bytes from temporary stream
                byte[] result = new byte[tmpStream.Length];
                tmpStream.Seek(0, SeekOrigin.Begin);
                tmpStream.Read(result, 0, (int)tmpStream.Length);

                return result;
            }
        }

        public void CreateThumbFile(string pathToFile, string pathToFileThumb)
        {
            WebImage imgThumb = new WebImage(pathToFile);
            if (imgThumb.Width >= 250 && imgThumb.Height >= 250)
            {
                double koef = 1.00;
                double width = 250.00;
                double height = 250.00;
                double widthTemp = imgThumb.Width;
                double heightTemp = imgThumb.Height;

                if (imgThumb.Width > imgThumb.Height)
                {
                    koef = widthTemp / heightTemp;
                    width *= koef;
                }
                else
                {
                    koef = heightTemp / widthTemp;
                    height *= koef;
                }

                imgThumb.Resize((int)width + 1, (int)height + 1, true, false);
                imgThumb.Save(pathToFileThumb);

                byte[] imageBytes = System.IO.File.ReadAllBytes(pathToFileThumb);
                byte[] croppedImage = CropImage(imageBytes, imgThumb.Width / 2 - 125, imgThumb.Height / 2 - 125, 250, 250);

                try
                {
                    FileHelper fileHelper = new FileHelper();
                    fileHelper.SaveFile(croppedImage, pathToFileThumb);
                }
                catch (Exception ex)
                {
                    //Log an error     
                    throw ex;
                }
            }
            else
            {
                imgThumb.Resize(250, 250, true, false);
                imgThumb.Save(pathToFileThumb);
            }
        }
    }
}
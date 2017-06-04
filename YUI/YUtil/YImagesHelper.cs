using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace YUI.YUtil
{
    /// <summary>
    /// 
    /// </summary>
    public static class YImagesHelper
    {
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="penColor"></param>
        /// <returns></returns>
        public static Image CreateThumbnail(Stream fileStream, int width, int height, System.Drawing.Color penColor)
        {
            Bitmap bitmap;
            try
            {
                bitmap = new Bitmap(fileStream);
            }
            catch
            {
                bitmap = new Bitmap(width, height);
            }
            return CreateThumbnail(bitmap, width, height, penColor);
        }

        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="penColor"></param>
        /// <returns></returns>
        public static Image CreateThumbnail(string fileName, int width, int height, System.Drawing.Color penColor)
        {
            Bitmap bitmap;
            try
            {
                bitmap = new Bitmap(fileName);
            }
            catch
            {
                bitmap = new Bitmap(width, height);
            }
            return CreateThumbnail(bitmap, width, height, penColor);
        }

        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="penColor"></param>
        /// <returns></returns>
        public static Image CreateThumbnail(Image image, int width, int height, System.Drawing.Color penColor)
        {
            Bitmap bitmap;
            try
            {
                bitmap = new Bitmap(image);
            }
            catch
            {
                bitmap = new Bitmap(width, height);
            }
            return CreateThumbnail(bitmap, width, height, penColor);
        }

        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="penColor"></param>
        /// <returns></returns>
        public static Image CreateThumbnail(Bitmap bitmap, int width, int height, System.Drawing.Color penColor)
        {
            width = bitmap.Width > width ? width : bitmap.Width;
            height = bitmap.Height > height ? height : bitmap.Height;
            var bitmap1 = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format64bppPArgb);
            using (var graphics = Graphics.FromImage(bitmap1))
            {
                var width1 = width;
                var height1 = height;
                if (bitmap.Width > bitmap.Height)
                    height1 = (int)(bitmap.Height / (double)bitmap.Width * width1);
                else if (bitmap.Width < bitmap.Height)
                    width1 = (int)(bitmap.Width / (double)bitmap.Height * height1);
                var x = width / 2 - width1 / 2;
                var y = height / 2 - height1 / 2;
                graphics.PixelOffsetMode = PixelOffsetMode.None;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(bitmap, x, y, width1, height1);
                using (var pen = new System.Drawing.Pen(penColor, 1f))
                    graphics.DrawRectangle(pen, 0, 0, bitmap1.Width - 1, bitmap1.Height - 1);
                return bitmap1;
            }
        }

        /// <summary>
        /// 绘制图片
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="img"></param>
        /// <param name="opacity"></param>
        public static void DrawImage(Graphics g, Rectangle rect, Image img, float opacity)
        {
            if (opacity <= 0.0)
                return;

            using (var imageAttributes = new ImageAttributes())
            {
                SetImageOpacity(imageAttributes, (double)opacity >= 1.0 ? 1f : opacity);
                g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imageAttributes);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgAttributes"></param>
        /// <param name="opacity"></param>
        public static void SetImageOpacity(ImageAttributes imgAttributes, float opacity)
        {
            var newColorMatrix1 = new float[5][];
            var numArray1 = newColorMatrix1;
            const int index1 = 0;
            var numArray2 = new float[5];
            numArray2[0] = 1f;
            var numArray3 = numArray2;
            numArray1[index1] = numArray3;
            var numArray4 = newColorMatrix1;
            const int index2 = 1;
            var numArray5 = new float[5];
            numArray5[1] = 1f;
            var numArray6 = numArray5;
            numArray4[index2] = numArray6;
            var numArray7 = newColorMatrix1;
            const int index3 = 2;
            var numArray8 = new float[5];
            numArray8[2] = 1f;
            var numArray9 = numArray8;
            numArray7[index3] = numArray9;
            var numArray10 = newColorMatrix1;
            const int index4 = 3;
            var numArray11 = new float[5];
            numArray11[3] = opacity;
            var numArray12 = numArray11;
            numArray10[index4] = numArray12;
            var numArray13 = newColorMatrix1;
            const int index5 = 4;
            var numArray14 = new float[5];
            numArray14[4] = 1f;
            var numArray15 = numArray14;
            numArray13[index5] = numArray15;
            var newColorMatrix2 = new ColorMatrix(newColorMatrix1);
            imgAttributes.SetColorMatrix(newColorMatrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }

        /// <summary>
        /// 创建ImageSource缩略图
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static ImageSource CreateImageSourceThumbnia(string fileName, double width, double height)
        {
            var original = Image.FromFile(fileName);
            var num = (float)Math.Min(width / original.Width, height / original.Height);
            var width1 = original.Width;
            var height1 = original.Height;
            if (num < 1.0)
            {
                width1 = (int)Math.Round(original.Width * (double)num);
                height1 = (int)Math.Round(original.Height * (double)num);
            }
            var bitmap = new Bitmap(original, width1, height1);
            var hbitmap = bitmap.GetHbitmap();
            var sourceFromHbitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            sourceFromHbitmap.Freeze();
            DeleteObject(hbitmap);
            original.Dispose();
            bitmap.Dispose();
            return sourceFromHbitmap;
        }

        /// <summary>
        /// 创建ImageSource缩略图
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static ImageSource CreateImageSourceThumbnia(Image sourceImage, double width, double height)
        {
            if (sourceImage == null)
                return null;
            var num = (float)Math.Min(width / sourceImage.Width, height / sourceImage.Height);
            var width1 = sourceImage.Width;
            var height1 = sourceImage.Height;
            if (num < 1.0)
            {
                width1 = (int)Math.Round(sourceImage.Width * (double)num);
                height1 = (int)Math.Round(sourceImage.Height * (double)num);
            }
            var bitmap = new Bitmap(sourceImage, width1, height1);
            var hbitmap = bitmap.GetHbitmap();
            var sourceFromHbitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            sourceFromHbitmap.Freeze();
            DeleteObject(hbitmap);
            sourceImage.Dispose();
            bitmap.Dispose();
            return sourceFromHbitmap;
        }

        public static ImageSource CreateImageSourceThumbnia(byte[] data, double width, double height)
        {
            using (Stream stream = new MemoryStream(data, true))
            {
                using (var sourceImage = Image.FromStream(stream))
                    return CreateImageSourceThumbnia(sourceImage, width, height);
            }
        }

        /// <summary>
        /// 从Bitemap创建ImageSource
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static ImageSource CreateImageSourceFromImage(Bitmap image)
        {
            if (image == null)
                return null;
            try
            {
                var hbitmap = image.GetHbitmap();
                var sourceFromHbitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                sourceFromHbitmap.Freeze();
                image.Dispose();
                DeleteObject(hbitmap);
                return sourceFromHbitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 从文件读取ImageSource
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static ImageSource CreatImageSourceFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;

                using (Stream ms = new MemoryStream(File.ReadAllBytes(filePath)))
                {
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }

                return bitmap;
            }
            else
            {
                return null;
            }
        }
    }
}

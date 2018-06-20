using System;
using System.IO;
using System.Linq;
using System.Text;

// https://stackoverflow.com/questions/210650/validate-image-from-file-in-c-sharp/9446045#9446045
namespace ImageViewer.Common
{

    static class ImageValidator
    {
        public static bool IsImage(string filePath)
        {
            try
            {

                var exist = File.Exists(filePath);
                byte[] buffer = new byte[10];
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                }
                return exist && GetImageFormat(buffer) != ImageFormat.Unknown;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            // see http://www.mikekunz.com/image_file_header.html  
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.Bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.Gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.Png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.Tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.Tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.Jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.Jpeg;

            return ImageFormat.Unknown;
        }
    }
}

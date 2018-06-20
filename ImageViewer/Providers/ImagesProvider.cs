using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;
using static ImageViewer.Common.ImageValidator;

namespace ImageViewer.Providers
{
    static class ImagesProvider
    {
        private static string _imagesDirectoryPath = null;
        private static BitmapImage _defaultThumbnailImg;

        public static BitmapSource CreateThumbnail(string path)
        {
            BitmapSource thumbnail;
            try
            {
                thumbnail = BitmapFrame.Create(new Uri(path, UriKind.Absolute)).Thumbnail;
                if (thumbnail == null)
                {
                    var bmpImage = new BitmapImage();
                    bmpImage.BeginInit();
                    bmpImage.UriSource = new Uri(path);
                    bmpImage.DecodePixelWidth = 200;
                    bmpImage.EndInit();
                    thumbnail = bmpImage;
                }
            }
            catch (Exception)
            {
                thumbnail = CreateDefaulThumbnail();
            }

            return thumbnail;
        }

        public static BitmapImage CreateBitmapImage(string path)
        {
            var bitmapImage = new BitmapImage();

            if (File.Exists(path))
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = fs;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                }
            }

            return bitmapImage;
        }

        public static List<string> GetImages()
        {
            List<string> result = null;
            try
            {
                var imagesDirectoryPath = Path.Combine(GetResourcesDirPath(), "Test");
                result = GetImages(imagesDirectoryPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = new List<string>();
            }

            return result;
        }

        private static string GetResourcesDirPath()
        {
            if (_imagesDirectoryPath == null)
            {
                var currentAssem = Assembly.GetExecutingAssembly();
                var exeDirectoryPath = Path.GetDirectoryName(currentAssem.Location);
                if (Directory.Exists(exeDirectoryPath))
                {
                    _imagesDirectoryPath = Path.Combine(exeDirectoryPath, "Resources");
                }
            }

            return _imagesDirectoryPath;
        }

        private static List<string> GetImages(string imagesDirectoryPath)
        {
            var result = new List<string>();
            try
            {
                if (Directory.Exists(imagesDirectoryPath))
                {
                    var images = Directory.EnumerateFiles(imagesDirectoryPath).Where(IsImage).ToArray();

                    foreach (var imagePath in images)
                    {
                        result.Add(imagePath);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }

        static BitmapImage CreateDefaulThumbnail()
        {
            if (_defaultThumbnailImg == null)
            {
                var defaultThumbnailImgPath = Path.Combine(GetResourcesDirPath(), "default.png");
                _defaultThumbnailImg = CreateBitmapImage(defaultThumbnailImgPath);
            }

            return _defaultThumbnailImg;
        }
    }
}
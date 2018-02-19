using PhotoSorterWPF.Model;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PhotoSorterWPF.Converters
{
    public class ImageResizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ImageInfo imgInfo)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(imgInfo.Path);
                image.DecodePixelWidth = (int)Constants.ImagePreviewWidth;
                image.Rotation = imgInfo.Rotation;
                image.EndInit();
                return image;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }
    }
}

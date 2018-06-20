using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ImageViewer.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InvertBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                return value != null && (bool)value
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }

            return value != null && (bool)value
                ? Visibility.Hidden
                : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

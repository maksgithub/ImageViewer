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
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                return value != null && (bool)value
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }

            return value != null && (bool)value
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

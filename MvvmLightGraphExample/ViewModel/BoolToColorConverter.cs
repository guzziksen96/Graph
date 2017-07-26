using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MvvmLightGraphExample.ViewModel
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool p = (bool)value;

            if (p)
            {
                return Brushes.Purple;
            }

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

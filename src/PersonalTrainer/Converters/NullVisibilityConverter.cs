using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Figroll.PersonalTrainer.Converters
{
    public class NullVisibilityConverter : IValueConverter
    {
        public Visibility IsNull { get; set; }
        public Visibility IsNotNull { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? IsNull : IsNotNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
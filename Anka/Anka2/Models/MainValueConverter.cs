using System;
using System.Globalization;
using System.Windows.Data;

namespace Anka2.Models
{
    public class InverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
                return value is bool ? !(bool)value : value;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }

    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                bool gender = (bool)value;
                switch (gender)
                {
                    case true:
                        return "男";
                    case false:
                        return "女";
                    default:
                }
            }
            else
                return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string gender = (string)value;
            var male = gender switch
            {
                "男" => true,
                "女" => false,
                _ => throw new NotImplementedException(),
            };
            return male;
        }

    }
}

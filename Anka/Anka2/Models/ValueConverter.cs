using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Anka2.Models
{
    public class InverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool ? !(bool)value : value;
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string gender = (string)value;
            bool male ;
            switch (gender)
            {
                case "男":
                    male =  true;
                    break;
                case "女":
                    male = false;
                    break;
                default:
                    throw new NotImplementedException();

            }
            return male;
        }
       
    }

    public class CollatCircContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            bool CollatCircIsChecked = value is null ? false : (bool)value;
            switch (CollatCircIsChecked)
            {
                case true:
                    return "侧枝循环：有";
                case false:
                    return "侧枝循环：无";
                default:
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class Risk13CheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ss = value is null ? false : true;
            return value is null ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is true ? "" : null; 
        }
    }
}

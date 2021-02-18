using System;
using System.Globalization;
using System.Windows.Data;

namespace Anka2.Models.BasicInfoConverter
{
    

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
    public class RiskOtherCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
                return !String.IsNullOrEmpty(value.ToString());
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is true ? "" : null; 
        }
    }
    public class DCRCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
                return value.ToString() == "1";
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : null;
        }
    }

    public class DCBCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
                return value.ToString() == "0";
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 0 : null;
        }
    }
    public class DCLCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
                return value.ToString() == "-1";
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? -1 : null;
        }
    }

    public class RisksConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                if(value.ToString().Length==12)
                {
                    char[] chValues = value.ToString().ToCharArray();
                    return chValues[0] == 1;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? -1 : null;
        }
    }

    public class BasicRiskConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            int RiskNum = System.Convert.ToInt32( parameter);
            if (value is not null)
            {
                if (value.ToString().Length == 13)
                {
                    char[] chValues = value.ToString().ToCharArray();
                    if (chValues[RiskNum] == '1')
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int RiskNum = System.Convert.ToInt32(parameter);
            char[] chValues = tempValue.ToString().ToCharArray();
            chValues[RiskNum] = (bool)value ? '1' : '0';
            tempValue = new string(chValues);
            return tempValue;
        }
    }
}

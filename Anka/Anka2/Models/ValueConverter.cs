using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

    public class ExerciseNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                
                var exerciseNumberList = ((List<Exercise>)value).Select(t => t.ExerciseNumber).ToList();
                for (int i = 0; i < exerciseNumberList.Count; i++)
                {
                    if (exerciseNumberList[i].Substring(0, 8) == ((List<Exercise>)value)[i].basicinfoNumber)
                    {
                        exerciseNumberList[i] = exerciseNumberList[i].Remove(0, 9);
                        exerciseNumberList[i] = Regex.Replace(exerciseNumberList[i], @"[^0-9]+", "/");
                    }
                }
                return exerciseNumberList;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

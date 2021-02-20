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
                if (value.ToString().Length == 12)
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
            int RiskNum = System.Convert.ToInt32(parameter);
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
            char[] chValues = ("0000000000000").ToCharArray();;
            if (tempValue is not null)
            {               
                chValues = tempValue.ToString().ToCharArray();               
            }
            int RiskNum = System.Convert.ToInt32(parameter);
            chValues[RiskNum] = (bool)value ? '1' : '0';
            tempValue = new string(chValues);
            return tempValue;
        }
    }

    public class RiskOtherConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            bool isChecked = false;
            string strContent = null;
            if (value is not null)
            {
                if (((BasicInfo)value).BasicRisk is not null)
                {

                    if (((BasicInfo)value).BasicRisk.Length == 13)
                    {
                        char[] chValues = ((BasicInfo)value).BasicRisk.ToString().ToCharArray();
                        if (chValues[12] == '1')
                        {
                            strContent = ((BasicInfo)value).RiskOther is null ? null : ((BasicInfo)value).RiskOther.ToString();
                            isChecked = true;
                        }
                    }
                   
                }
            }
            switch (parameter.ToString())
            {
                case "12": return isChecked;
                default:return strContent;
            }           
        }
        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            char[] risksChecksArray = ("0000000000000").ToCharArray();
            if (((BasicInfo)tempValue).BasicRisk is not null)
            {
                risksChecksArray = ((BasicInfo)tempValue).BasicRisk.ToCharArray();
            }

                switch (parameter.ToString())
            {
                case "12":
                    {
                        if((bool)value==false)
                        {
                            risksChecksArray[12] = '0';
                            ((BasicInfo)tempValue).RiskOther = null;
                        }
                        else
                        {
                            risksChecksArray[12] = '1'; 
                        }
                        
                    } break;
                default:
                    {
                        
                        if(string.IsNullOrWhiteSpace(value.ToString()))
                        {
                            ((BasicInfo)tempValue).RiskOther = null;
                            risksChecksArray[12] = '0';
                        }
                        else
                            ((BasicInfo)tempValue).RiskOther = value.ToString();
                    }
                    break;
            }
            ((BasicInfo)tempValue).BasicRisk = new string(risksChecksArray);
            return tempValue;
        }

    }

    
    
}

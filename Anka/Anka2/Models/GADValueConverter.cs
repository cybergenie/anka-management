using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Anka2.Models.GADValueConverter
{
    public class GADNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {

                var gadNumberList = ((List<GAD>)value).Select(t => t.GADNumber).ToList();
                for (int i = 0; i < gadNumberList.Count; i++)
                {
                    if (gadNumberList[i].Substring(0, 8) == ((List<GAD>)value)[i].basicinfoNumber)
                    {
                        gadNumberList[i] = gadNumberList[i].Remove(0, 9);
                        var endNumber = gadNumberList[i].Substring(gadNumberList[i].Length - 3, 3);
                        gadNumberList[i] = gadNumberList[i].Remove(gadNumberList[i].Length - 3, 3);
                        gadNumberList[i] = Regex.Replace(gadNumberList[i], @"[^0-9]+", "/");
                        gadNumberList[i] += endNumber;
                    }
                }
                return gadNumberList;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GADResultConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            var parameterArray = ((string)parameter).ToCharArray();
            int itemIndex = System.Convert.ToInt32(parameterArray[0].ToString());
            int checkItem = System.Convert.ToInt32(parameterArray[1].ToString());
            if (value is not null)
            {
                if (((GAD)value).GADResult is not null)
                {
                    var ChecksArray = ((GAD)value).GADResult.Split('|');
                    switch (ChecksArray[itemIndex])
                    {
                        case "0":
                            {
                                switch (checkItem)
                                {
                                    case 0:
                                        return true;
                                    default:
                                        return false;
                                }
                            }
                        case "1":
                            {
                                switch (checkItem)
                                {
                                    case 1: return true;
                                    default: return false;
                                }
                            }
                        case "2":
                            {
                                switch (checkItem)
                                {
                                    case 2: return true;
                                    default: return false;
                                }
                            }
                        case "3":
                            {
                                switch (checkItem)
                                {
                                    case 3: return true;
                                    default: return false;
                                }
                            }
                        default: return false;

                    }
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterArray = ((string)parameter).ToCharArray();
            int index = System.Convert.ToInt32(parameterArray[0].ToString());
            int checkItem = System.Convert.ToInt32(parameterArray[1].ToString());
            if (tempValue is not null)
            {
                var ChecksArray = new string[10];
                if (((GAD)tempValue).GADResult is not null)
                {
                    ChecksArray = ((GAD)tempValue).GADResult.Split('|');
                }
                if (value is not null)
                {
                    switch (value)
                    {
                        case true:
                            {
                                ChecksArray[index] = checkItem.ToString(); break;
                            }
                        case false:
                            {
                                break;
                            }
                    }
                }
                ((GAD)tempValue).GADResult = string.Join('|', ChecksArray);

            }
            return tempValue;
        }
    }

    public class GADResultAllConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string GADResultAll = null;
            if (value is not null)
            {
                if (((GAD)value).GADResult is not null)
                {
                    int iGADResultAll = 0;
                    var GADResultArray = ((GAD)value).GADResult.Split('|');
                    foreach (var GADResult in GADResultArray)
                    {
                        if (!string.IsNullOrEmpty(GADResult))
                        {
                            if (System.Convert.ToInt32(GADResult) > 0)
                            {
                                iGADResultAll += System.Convert.ToInt32(GADResult);
                            }
                        }
                    }
                    GADResultAll = iGADResultAll.ToString();
                }
            }
            return GADResultAll;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

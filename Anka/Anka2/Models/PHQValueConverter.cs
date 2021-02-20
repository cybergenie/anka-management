using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Anka2.Models.PHQValueConverter
{

    public class PHQNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {

                var phqNumberList = ((List<PHQ>)value).Select(t => t.PHQNumber).ToList();
                for (int i = 0; i < phqNumberList.Count; i++)
                {
                    if (phqNumberList[i].Substring(0, 8) == ((List<PHQ>)value)[i].basicinfoNumber)
                    {
                        phqNumberList[i] = phqNumberList[i].Remove(0, 9);
                        var endNumber = phqNumberList[i].Substring(phqNumberList[i].Length - 3, 3);
                        phqNumberList[i] = phqNumberList[i].Remove(phqNumberList[i].Length - 3, 3);
                        phqNumberList[i] = Regex.Replace(phqNumberList[i], @"[^0-9]+", "/");
                        phqNumberList[i] += endNumber;
                    }
                }
                return phqNumberList;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PHQResultConverter : IValueConverter
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
                if (((PHQ)value).PHQResult is not null)
                {
                    var ChecksArray = ((PHQ)value).PHQResult.Split('|');
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
                if (((PHQ)tempValue).PHQResult is not null)
                {
                    ChecksArray = ((PHQ)tempValue).PHQResult.Split('|');
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
                ((PHQ)tempValue).PHQResult = string.Join('|', ChecksArray);

            }
            return tempValue;
        }
    }

    public class PHQResultAllConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string PHQResultAll = null;
            if (value is not null)
            {
                int iPHQResultAll = 0;
                if (((PHQ)value).PHQResult is not null)
                {

                    var PHQResultArray = ((PHQ)value).PHQResult.Split('|');
                    foreach (var PHQResult in PHQResultArray)
                    {
                        if (!string.IsNullOrEmpty(PHQResult))
                        {
                            if (System.Convert.ToInt32(PHQResult) > 0)
                            {
                                iPHQResultAll += System.Convert.ToInt32(PHQResult);
                            }
                        }
                    }
                    PHQResultAll = iPHQResultAll.ToString();
                }

                if (iPHQResultAll is >= 0 and <= 4)
                {
                    if (parameter is "1")
                    {
                        return true;
                    }
                }

                if (iPHQResultAll is >= 5 and <= 9)
                {
                    if (parameter is "2")
                    {
                        return true;
                    }
                }
                if (iPHQResultAll is >= 10 and <= 13)
                {
                    if (parameter is "3")
                    {
                        return true;
                    }
                }
                if (iPHQResultAll is >= 14 and <= 18)
                {
                    if (parameter is "4")
                    {
                        return true;
                    }
                }
                if (iPHQResultAll is >= 19)
                {
                    if (parameter is "5")
                    {
                        return true;
                    }
                }

            }
            return PHQResultAll;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

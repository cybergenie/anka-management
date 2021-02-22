using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Anka2.Models.OHQValueConverter
{

    public class OHQNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {

                var ohqNumberList = ((List<OHQ>)value).Select(t => t.OHQNumber).ToList();
                for (int i = 0; i < ohqNumberList.Count; i++)
                {
                    if (ohqNumberList[i].Substring(0, 8) == ((List<OHQ>)value)[i].basicinfoNumber)
                    {
                        ohqNumberList[i] = ohqNumberList[i].Remove(0, 9);
                        var endNumber = ohqNumberList[i].Substring(ohqNumberList[i].Length - 3, 3);
                        ohqNumberList[i] = ohqNumberList[i].Remove(ohqNumberList[i].Length - 3, 3);
                        ohqNumberList[i] = Regex.Replace(ohqNumberList[i], @"[^0-9]+", "/");
                        ohqNumberList[i] += endNumber;
                    }
                }
                return ohqNumberList;
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

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
                    if (ohqNumberList[i].Substring(0, ((List<OHQ>)value)[i].basicinfoNumber.Length) == ((List<OHQ>)value)[i].basicinfoNumber)
                    {
                        try
                        {
                            ohqNumberList[i] = ohqNumberList[i].Remove(0, ((List<OHQ>)value)[i].basicinfoNumber.Length+1);
                            var endNumber = ohqNumberList[i].Substring(ohqNumberList[i].Length - 3, 3);
                            ohqNumberList[i] = ohqNumberList[i].Remove(ohqNumberList[i].Length - 3, 3);
                            ohqNumberList[i] = Regex.Replace(ohqNumberList[i], @"[^0-9]+", "/");
                            ohqNumberList[i] += endNumber;
                        }
                        catch(Exception)
                        {
                            ohqNumberList[i] = ohqNumberList[i].Remove(0, 9);
                        }
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

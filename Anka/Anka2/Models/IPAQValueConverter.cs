using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Anka2.Models.IPAQValueConverter
{
    public class IPAQNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                var ipaqNumberList = ((List<IPAQ>)value).Select(t => t.IPAQNumber).ToList();
                for (int i = 0; i < ipaqNumberList.Count; i++)
                {
                    if (ipaqNumberList[i].Substring(0, 8) == ((List<IPAQ>)value)[i].basicinfoNumber)
                    {
                        ipaqNumberList[i] = ipaqNumberList[i].Remove(0, 9);
                        var endNumber = ipaqNumberList[i].Substring(ipaqNumberList[i].Length - 3, 3);
                        ipaqNumberList[i] = ipaqNumberList[i].Remove(ipaqNumberList[i].Length - 3, 3);
                        ipaqNumberList[i] = Regex.Replace(ipaqNumberList[i], @"[^0-9]+", "/");
                        ipaqNumberList[i] += endNumber;
                    }
                }
                return ipaqNumberList;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IPAQ0Converter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            switch (value)
            {
                case false:
                    {
                        if ((string)parameter == "0")
                            return true;
                        else
                            return false;
                    }
                case true:
                    {
                        if ((string)parameter == "1")
                            return true;
                        else
                            return false;
                    }                    
                default: return false;
            } 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           switch (value)
            {
                case true:
                    {
                        if ((string)parameter == "0")
                            return false;
                        else
                            return true;
                    }
                default: return null;
            }
        }
    }

    public class IPAQ4Converter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double time = 0;           
            if (value.ToString().Contains('h') || value.ToString().Contains('H'))
            {
                string[] strHour = ((string)value).Split('h', 'H');
                time += System.Convert.ToDouble(strHour[0]) * 60;
                if(strHour.Length>1)
                {
                    if(strHour[1].Contains('m') || strHour[1].Contains('M'))
                    {
                        string[] strMinute = ((string)value).Split('h', 'H');
                        time += System.Convert.ToDouble(strMinute[0]);
                    }
                    else
                    {
                        time += System.Convert.ToDouble(strHour[1]);
                    }
                }                
            }
            else
            {
                string strTime = Regex.Replace((string)value, @"[^0-9]+", "");
                time += System.Convert.ToDouble(strTime);
            }

            return time;
            
        }
    }



}

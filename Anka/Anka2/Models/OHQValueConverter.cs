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

    public class OHQ1Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           switch (value.ToString())
            {
                case "0":
                    {
                        if (parameter.ToString() == "0")
                            return true;
                        else
                            return false;
                    }
                case "1":
                    {
                        if (parameter.ToString() == "1")
                            return true;
                        else
                            return false;
                    }
                case "2":
                    {
                        if (parameter.ToString() == "2")
                            return true;
                        else
                            return false;
                    }
                case "3":
                    {
                        if (parameter.ToString() == "3")
                            return true;
                        else
                            return false;
                    }
                default:return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string OHQ1 = null;
            if ((bool)value == true)
            {
                 OHQ1 = parameter.ToString() switch
                {
                    "0" => "0",
                    "1" => "1",
                    "2" => "2",
                    "3" => "3",
                    _ => null
                };
            }

            return OHQ1;
        }
    }
    public class OHQ2Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "0":
                    {
                        if (parameter.ToString() == "0")
                            return true;
                        else
                            return false;
                    }
                case "1":
                    {
                        if (parameter.ToString() == "1")
                            return true;
                        else
                            return false;
                    }
                case "2":
                    {
                        if (parameter.ToString() == "2")
                            return true;
                        else
                            return false;
                    }              
                default: return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string OHQ2 = null;
            if ((bool)value == true)
            {
                OHQ2 = parameter.ToString() switch
                {
                    "0" => "0",
                    "1" => "1",
                    "2" => "2",                    
                    _ => null
                };
            }

            return OHQ2;
        }
    }
    public class OHQ3Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "0":
                    {
                        if (parameter.ToString() == "0")
                            return true;
                        else
                            return false;
                    }
                case "1":
                    {
                        if (parameter.ToString() == "1")
                            return true;
                        else
                            return false;
                    }
                case "2":
                    {
                        if (parameter.ToString() == "2")
                            return true;
                        else
                            return false;
                    }
                case "3":
                    {
                        if (parameter.ToString() == "3")
                            return true;
                        else
                            return false;
                    }
                case "4":
                    {
                        if (parameter.ToString() == "4")
                            return true;
                        else
                            return false;
                    }
                case "5":
                    {
                        if (parameter.ToString() == "5")
                            return true;
                        else
                            return false;
                    }
                default: return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string OHQ3 = null;
            if ((bool)value == true)
            {
                OHQ3 = parameter.ToString() switch
                {
                    "0" => "0",
                    "1" => "1",
                    "2" => "2",
                    "3" => "3",
                    "4" => "4",
                    "5" => "5",
                    _ => null
                };
            }

            return OHQ3;
        }
    }
    public class OHQ4Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var OHQ4s = value.ToString().Split('-');           
            switch (OHQ4s[0])
            {
                case "A":
                    {
                        switch (parameter.ToString())
                        {
                            case "0x": return true;
                            case "0t": return OHQ4s[1];
                            default:
                                {
                                    if (parameter.ToString().Contains('x'))
                                        return false;
                                    else
                                        return null;
                                }
                        }
                    }
                case "B":
                    {
                        switch (parameter.ToString())
                        {
                            case "1x": return true;
                            case "1t": return OHQ4s[1];
                            default:
                                {
                                    if (parameter.ToString().Contains('x'))
                                        return false;
                                    else
                                        return null;
                                }
                        }
                    }
                case "C":
                    {
                        switch (parameter.ToString())
                        {
                            case "2": return true;
                            default: return false;
                        }
                    }
                default: return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            char[] OHQ4s = new char[3];
            if (parameter.ToString().Contains('x'))
            {
                if ((bool)value == true)
                {
                    switch(parameter.ToString().ToCharArray()[0])
                    {
                        case '0':
                            {
                                OHQ4s[0] = 'A';                                
                            }
                            break;
                        case '1': OHQ4s[0] = 'B'; break;
                        case '2': OHQ4s[0] = 'C'; break;
                    }
                }
            }

            return string.Join('-', OHQ4s);


        }
    }

}

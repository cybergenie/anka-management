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
            if (value is not null)
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
                    default: return false;
                }
            }
            else
            {
                return false;
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
            if (value is not null)
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
            else
                return false;
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
            if (value is not null)
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
            else
                return false;
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
            if (value is not null)
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
                                case "2x": return true;
                                default:
                                    {
                                        if (parameter.ToString().Contains('x'))
                                            return false;
                                        else
                                            return null;
                                    }
                            }
                        }
                    default:
                        {
                            if (parameter.ToString().Contains('x'))
                                return false;
                            else
                                return null;
                        }
                }
            }
            else
            {
                if (parameter.ToString().Contains('x'))
                    return false;
                else
                    return null;
            }
               
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            string OHQ4s = parameter.ToString() switch
            {
                "0x" => "A-" + string.Empty,
                "0t" => "A-" + value.ToString(),
                "1x" => "B-" + string.Empty,
                "1t" => "B-" + value.ToString(),
                "2x" => "C-" + string.Empty,
                _ => null

            };

            return OHQ4s;


        }
    }
    public class OHQ5Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
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
                    default: return false;
                }
            }
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string OHQ5 = null;
            if ((bool)value == true)
            {
                OHQ5 = parameter.ToString() switch
                {
                    "0" => "0",
                    "1" => "1",
                    "2" => "2",
                    "3" => "3",                   
                    _ => null
                };
            }

            return OHQ5;
        }
    }
    public class OHQ6Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                switch (value.ToString())
                {
                    case "0":
                        {
                            if (parameter.ToString() == "00")
                                return true;
                            else if (parameter.ToString().ToCharArray()[1] == '1')
                                return null;
                            else
                                return false;
                        }
                    case "99":
                        {
                            if (parameter.ToString() == "20")
                                return true;
                            else if (parameter.ToString().ToCharArray()[1] == '1')
                                return null;
                            else
                                return false;
                        }                   
                    default:
                        {
                            if (parameter.ToString() == "10")
                                return true;
                            if (parameter.ToString().ToCharArray()[1] == '1')
                                return value.ToString();
                            else
                                return false;
                        }
                }
            }
            else
            {
                if (parameter.ToString().ToCharArray()[1] == '1')
                    return null;
                else
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string OHQ6 = parameter.ToString() switch
            {
                "00" => "0",
                "10" => "0",
                "11" => value.ToString(),
                "20" => "99",               
                _ => null

            };

            return OHQ6;
        }
    }
    public class OHQ7Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
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
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string OHQ7 = null;
            if ((bool)value == true)
            {
                OHQ7 = parameter.ToString() switch
                {
                    "0" => "0",
                    "1" => "1",
                    "2" => "2",                   
                    _ => null
                };
            }

            return OHQ7;
        }
    }

    public class OHQ8Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
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
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string OHQ8 = null;
            if ((bool)value == true)
            {
                OHQ8 = parameter.ToString() switch
                {
                    "0" => "0",
                    "1" => "1",
                    "2" => "2",
                    _ => null
                };
            }
            return OHQ8;
        }
    }
    public class OHQ9Converter : IValueConverter
    {
        private object tempOHQ9Result;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempOHQ9Result = value;
            if (tempOHQ9Result is null)
            {
                value = null;
            }            
            else
            {
                value = tempOHQ9Result.ToString();
            }

            if (value is not null)
            {
                var OHQ9s = value.ToString().Split('-');
                switch (OHQ9s[0])
                {
                    case "A":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => true,
                                "01" => false,
                                _ => null,
                            };
                        }
                    case "B":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => true,
                                "10" => OHQ9s[1],
                                "11" => OHQ9s[2],
                                _ => null,
                            };
                        }
                    default:
                        {
                            if (parameter.ToString().ToCharArray()[0] == '0')
                                return false;
                            else
                                return null;
                        }
                }
            }
            else
            {
                if (parameter.ToString().ToCharArray()[0] == '0')
                    return false;
                else
                    return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] OHQ9ResultArray;
            if (tempOHQ9Result is null)
            {
                OHQ9ResultArray = new string[3];
            }           
            else
            {
                OHQ9ResultArray = tempOHQ9Result.ToString().Split('-');
            }            
            switch (parameter.ToString())
            {
                case "00":
                    {
                        if ((bool)value == true)
                        {
                            OHQ9ResultArray[0] = "A";
                            OHQ9ResultArray[1] = null;
                            OHQ9ResultArray[2] = null;
                            tempOHQ9Result = string.Join('-', OHQ9ResultArray);
                        }
                    }break;
                case "01":
                    {
                        if ((bool)value == true)
                            OHQ9ResultArray[0] = "B";
                        tempOHQ9Result = string.Join('-', OHQ9ResultArray);
                    }
                    break;
                case "10":
                    {
                        OHQ9ResultArray[0] = "B";
                        OHQ9ResultArray[1] = value.ToString();
                        tempOHQ9Result = string.Join('-', OHQ9ResultArray);
                    }
                    break;
                case "11":
                    {
                        OHQ9ResultArray[0] = "B";
                        OHQ9ResultArray[2] = value.ToString();
                        tempOHQ9Result = string.Join('-', OHQ9ResultArray);
                    }
                    break;
            }            
            return tempOHQ9Result;
        }
    }

}

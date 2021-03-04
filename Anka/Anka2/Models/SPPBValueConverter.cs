using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Anka2.Models.SPPBValueConverter
{

    public class SPPBNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {

                var sppbNumberList = ((List<SPPB>)value).Select(t => t.SPPBNumber).ToList();
                for (int i = 0; i < sppbNumberList.Count; i++)
                {
                    if (sppbNumberList[i].Substring(0, ((List<SPPB>)value)[i].basicinfoNumber.Length) == ((List<SPPB>)value)[i].basicinfoNumber)
                    {
                        sppbNumberList[i] = sppbNumberList[i].Remove(0, ((List<SPPB>)value)[i].basicinfoNumber.Length+1);
                        var endNumber = sppbNumberList[i].Substring(sppbNumberList[i].Length - 3, 3);
                        sppbNumberList[i] = sppbNumberList[i].Remove(sppbNumberList[i].Length - 3, 3);
                        sppbNumberList[i] = Regex.Replace(sppbNumberList[i], @"[^0-9]+", "/");
                        sppbNumberList[i] += endNumber;
                    }
                }
                return sppbNumberList;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BalanceTesting1Converter : IValueConverter
    {
        private object tempBalanceTestingResult;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempBalanceTestingResult = value;
            if (tempBalanceTestingResult is null)
            {
                value = null;
            }
            else
            {
                value = tempBalanceTestingResult.ToString();
            }

            if (value is not null)
            {
                var BalanceTestings = value.ToString().Split('-');
                switch (BalanceTestings[0])
                {
                    case "A":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => true,
                                "01" => false,
                                "10" => BalanceTestings[1],
                                "11" => BalanceTestings[2],
                                _ => null,
                            };
                        }
                    case "B":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => true,                               
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
            string[] BalanceTestingResultArray;
            if (tempBalanceTestingResult is null)
            {
                BalanceTestingResultArray = new string[3];
            }
            else
            {
                BalanceTestingResultArray = tempBalanceTestingResult.ToString().Split('-');
            }
            switch (parameter.ToString())
            {
                case "00":
                    {
                        if ((bool)value == true)
                        {
                            BalanceTestingResultArray[0] = "A";
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            BalanceTestingResultArray[0] = "B";
                            BalanceTestingResultArray[1] = null;
                            BalanceTestingResultArray[2] = null;
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "10":
                    {
                        BalanceTestingResultArray[0] = "A";
                        BalanceTestingResultArray[1] = value.ToString();
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "11":
                    {
                        BalanceTestingResultArray[0] = "A";
                        BalanceTestingResultArray[2] = value.ToString();
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
            }
            return tempBalanceTestingResult;
        }
    }
    public class BalanceTesting2Converter : IValueConverter
    {
        private object tempBalanceTestingResult;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempBalanceTestingResult = value;
            if (tempBalanceTestingResult is null)
            {
                value = null;
            }
            else
            {
                value = tempBalanceTestingResult.ToString();
            }

            if (value is not null)
            {
                var BalanceTestings = value.ToString().Split('-');
                switch (BalanceTestings[0])
                {
                    case "A":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => true,
                                "01" => false,
                                "10" => BalanceTestings[1],
                                "11" => BalanceTestings[2],
                                _ => null,
                            };
                        }
                    case "B":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => true,
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
            string[] BalanceTestingResultArray;
            if (tempBalanceTestingResult is null)
            {
                BalanceTestingResultArray = new string[3];
            }
            else
            {
                BalanceTestingResultArray = tempBalanceTestingResult.ToString().Split('-');
            }
            switch (parameter.ToString())
            {
                case "00":
                    {
                        if ((bool)value == true)
                        {
                            BalanceTestingResultArray[0] = "A";
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            BalanceTestingResultArray[0] = "B";
                            BalanceTestingResultArray[1] = null;
                            BalanceTestingResultArray[2] = null;
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "10":
                    {
                        BalanceTestingResultArray[0] = "A";
                        BalanceTestingResultArray[1] = value.ToString();
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "11":
                    {
                        BalanceTestingResultArray[0] = "A";
                        BalanceTestingResultArray[2] = value.ToString();
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
            }
            return tempBalanceTestingResult;
        }
    }
    public class BalanceTesting3Converter : IValueConverter
    {
        private object tempBalanceTestingResult;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempBalanceTestingResult = value;
            if (tempBalanceTestingResult is null)
            {
                value = null;
            }
            else
            {
                value = tempBalanceTestingResult.ToString();
            }

            if (value is not null)
            {
                var BalanceTestings = value.ToString().Split('-');
                switch (BalanceTestings[0])
                {
                    case "A":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => true,
                                "01" => false,
                                "10" => BalanceTestings[1],
                                "11" => BalanceTestings[2],
                                _ => null,
                            };
                        }
                    case "B":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => true,
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
            string[] BalanceTestingResultArray;
            if (tempBalanceTestingResult is null)
            {
                BalanceTestingResultArray = new string[3];
            }
            else
            {
                BalanceTestingResultArray = tempBalanceTestingResult.ToString().Split('-');
            }
            switch (parameter.ToString())
            {
                case "00":
                    {
                        if ((bool)value == true)
                        {
                            BalanceTestingResultArray[0] = "A";
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            BalanceTestingResultArray[0] = "B";
                            BalanceTestingResultArray[1] = null;
                            BalanceTestingResultArray[2] = null;
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "10":
                    {
                        BalanceTestingResultArray[0] = "A";
                        BalanceTestingResultArray[1] = value.ToString();
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "11":
                    {
                        BalanceTestingResultArray[0] = "A";
                        BalanceTestingResultArray[2] = value.ToString();
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
            }
            return tempBalanceTestingResult;
        }
    }
    public class WalkingTestingConverter : IValueConverter
    {
        private object tempWalkingTestingResult;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempWalkingTestingResult = value;
            if (tempWalkingTestingResult is null)
            {
                value = null;
            }
            else
            {
                value = tempWalkingTestingResult.ToString();
            }

            if (value is not null)
            {
                var WalkingTestings = value.ToString().Split('-');
                switch (WalkingTestings[0])
                {
                    case "A":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => true,
                                "01" => false,
                                "10" => WalkingTestings[1],
                                "11" => WalkingTestings[2],
                                _ => null,
                            };
                        }
                    case "B":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => true,
                                "10" => WalkingTestings[1],
                                "11" => WalkingTestings[2],
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
            string[] WalkingTestingResultArray;
            if (tempWalkingTestingResult is null)
            {
                WalkingTestingResultArray = new string[3];
            }
            else
            {
                WalkingTestingResultArray = tempWalkingTestingResult.ToString().Split('-');
            }
            switch (parameter.ToString())
            {
                case "00":
                    {
                        if ((bool)value == true)
                        {
                            WalkingTestingResultArray[0] = "A";
                        }
                        tempWalkingTestingResult = string.Join('-', WalkingTestingResultArray);
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            WalkingTestingResultArray[0] = "B";
                        }
                        tempWalkingTestingResult = string.Join('-', WalkingTestingResultArray);
                    }
                    break;
                case "10":
                    {                        
                        WalkingTestingResultArray[1] = value.ToString();
                        tempWalkingTestingResult = string.Join('-', WalkingTestingResultArray);
                    }
                    break;
                case "11":
                    {                       
                        WalkingTestingResultArray[2] = value.ToString();
                        tempWalkingTestingResult = string.Join('-', WalkingTestingResultArray);
                    }
                    break;
            }
            return tempWalkingTestingResult;
        }
    }



}

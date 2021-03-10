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
                                "00" => false,
                                "01" => true,                               
                                _ => null,
                            };
                        }
                    case "B":
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
                            BalanceTestingResultArray[0] = "B";
                            BalanceTestingResultArray[1] = null;
                            BalanceTestingResultArray[2] = null;
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            BalanceTestingResultArray[0] = "A";
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "10":
                    {
                        BalanceTestingResultArray[0] = "B";
                        BalanceTestingResultArray[1] = value.ToString();
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "11":
                    {
                        BalanceTestingResultArray[0] = "B";
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
                                "00" => false,
                                "01" => true,
                                _ => null,
                            };
                        }
                    case "B":
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
                            BalanceTestingResultArray[0] = "B";
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            BalanceTestingResultArray[0] = "A";
                            BalanceTestingResultArray[1] = null;
                            BalanceTestingResultArray[2] = null;
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "10":
                    {
                        BalanceTestingResultArray[0] = "B";
                        BalanceTestingResultArray[1] = value.ToString();
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "11":
                    {
                        BalanceTestingResultArray[0] = "B";
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
                                "00" => false,
                                "01" => true,
                                _ => null,
                            };
                        }
                    case "B":
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
                            BalanceTestingResultArray[0] = "B";
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            BalanceTestingResultArray[0] = "A";
                            BalanceTestingResultArray[1] = null;
                            BalanceTestingResultArray[2] = null;
                        }
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "10":
                    {
                        BalanceTestingResultArray[0] = "B";
                        BalanceTestingResultArray[1] = value.ToString();
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
                case "11":
                    {
                        BalanceTestingResultArray[0] = "B";
                        BalanceTestingResultArray[2] = value.ToString();
                        tempBalanceTestingResult = string.Join('-', BalanceTestingResultArray);
                    }
                    break;
            }
            return tempBalanceTestingResult;
        }
    }
    public class WalkingTesting1Converter : IValueConverter
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
                                "00" => false,
                                "01" => true,
                                "10" => WalkingTestings[1],
                                "11" => WalkingTestings[2],
                                _ => null,
                            };
                        }
                    case "B":
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
                    default:
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => false,
                                "10" => WalkingTestings[1],
                                "11" => WalkingTestings[2],
                                _ => null,
                            };
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
                            WalkingTestingResultArray[0] = "B";
                        }
                        tempWalkingTestingResult = string.Join('-', WalkingTestingResultArray);
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            WalkingTestingResultArray[0] = "A";
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
    public class WalkingTesting2Converter : IValueConverter
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

                return (parameter.ToString()) switch
                {
                    "10" => WalkingTestings[1],
                    "11" => WalkingTestings[2],
                    _ => null,
                };


            }
            else
            {
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
    public class SitUpTesting1Converter : IValueConverter
    {
        private object tempSitUpTestingResult;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempSitUpTestingResult = value;
            if (tempSitUpTestingResult is null)
            {
                value = null;
            }
            else
            {
                value = tempSitUpTestingResult.ToString();
            }

            if (value is not null)
            {
                var SitUpTestings = value.ToString().Split('-');

                switch (SitUpTestings[0])
                {
                    case "A":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => true,
                                "01" => false,
                                "10" => SitUpTestings[1],
                                "11" => SitUpTestings[2],                                
                                _ => null,
                            };
                        }
                    case "B":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => true,                               
                                "22" => SitUpTestings[2],
                                _ => null,
                            };
                        }
                    default:
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => false,                                
                                _ => null,
                            };
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
            string[] SitUpTestingResultArray;
            if (tempSitUpTestingResult is null)
            {
                SitUpTestingResultArray = new string[3];
            }
            else
            {
                SitUpTestingResultArray = tempSitUpTestingResult.ToString().Split('-');
            }
            switch (parameter.ToString())
            {
                case "00":
                    {
                        if ((bool)value == true)
                        {
                            SitUpTestingResultArray[0] = "A";
                            SitUpTestingResultArray[1] = null;
                            SitUpTestingResultArray[2] = null;
                            tempSitUpTestingResult = string.Join('-', SitUpTestingResultArray);
                        }                        
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            SitUpTestingResultArray[0] = "B";
                            SitUpTestingResultArray[1] = null;
                            SitUpTestingResultArray[2] = null;
                            tempSitUpTestingResult = string.Join('-', SitUpTestingResultArray);
                        }
                       
                    }
                    break;
                case "10":
                    {
                        if (SitUpTestingResultArray[0] == "A")
                        {
                            SitUpTestingResultArray[1] = value.ToString();
                            tempSitUpTestingResult = string.Join('-', SitUpTestingResultArray);
                        }
                        
                    }
                    break;
                case "11":
                    {
                        if (SitUpTestingResultArray[0] == "A")
                        {
                            SitUpTestingResultArray[2] = value.ToString();
                            tempSitUpTestingResult = string.Join('-', SitUpTestingResultArray);
                        }
                        
                    }
                    break;
                case "22":
                    {
                        if (SitUpTestingResultArray[0] == "B")
                        {
                            SitUpTestingResultArray[1] = null;
                            SitUpTestingResultArray[2] = value.ToString();
                            tempSitUpTestingResult = string.Join('-', SitUpTestingResultArray);
                        }
                        
                    }
                    break;
            }
            return tempSitUpTestingResult;
        }
    }
    public class SitUpTesting2Converter : IValueConverter
    {
        private object tempSitUpTestingResult;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempSitUpTestingResult = value;
            if (tempSitUpTestingResult is null)
            {
                value = null;
            }
            else
            {
                value = tempSitUpTestingResult.ToString();
            }

            if (value is not null)
            {
                var SitUpTestings = value.ToString().Split('-');

                switch (SitUpTestings[0])
                {
                    case "A":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => true,
                                "01" => false,
                                "10" => SitUpTestings[1],
                                "11" => SitUpTestings[2],
                                _ => null,
                            };
                        }
                    case "B":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => true,
                                "22" => SitUpTestings[2],
                                _ => null,
                            };
                        }
                    default:
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => false,
                                _ => null,
                            };
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
            string[] SitUpTestingResultArray;
            if (tempSitUpTestingResult is null)
            {
                SitUpTestingResultArray = new string[3];
            }
            else
            {
                SitUpTestingResultArray = tempSitUpTestingResult.ToString().Split('-');
            }
            switch (parameter.ToString())
            {
                case "00":
                    {
                        if ((bool)value == true)
                        {
                            SitUpTestingResultArray[0] = "A";
                            SitUpTestingResultArray[1] = null;
                            SitUpTestingResultArray[2] = null;
                            tempSitUpTestingResult = string.Join('-', SitUpTestingResultArray);
                        }
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            SitUpTestingResultArray[0] = "B";
                            SitUpTestingResultArray[1] = null;
                            SitUpTestingResultArray[2] = null;
                            tempSitUpTestingResult = string.Join('-', SitUpTestingResultArray);
                        }

                    }
                    break;
                case "10":
                    {
                        if (SitUpTestingResultArray[0] == "A")
                        {
                            SitUpTestingResultArray[1] = value.ToString();
                            tempSitUpTestingResult = string.Join('-', SitUpTestingResultArray);
                        }

                    }
                    break;
                case "11":
                    {
                        if (SitUpTestingResultArray[0] == "A")
                        {
                            SitUpTestingResultArray[2] = value.ToString();
                            tempSitUpTestingResult = string.Join('-', SitUpTestingResultArray);
                        }

                    }
                    break;
                case "22":
                    {
                        if (SitUpTestingResultArray[0] == "B")
                        {
                            SitUpTestingResultArray[1] = null;
                            SitUpTestingResultArray[2] = value.ToString();
                            tempSitUpTestingResult = string.Join('-', SitUpTestingResultArray);
                        }

                    }
                    break;
            }
            return tempSitUpTestingResult;
        }
    }
    public class TUGConverter : IValueConverter
    {
        private object tempTUGResult;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempTUGResult = value;
            if (tempTUGResult is null)
            {
                value = null;
            }
            else
            {
                value = tempTUGResult.ToString();
            }

            if (value is not null)
            {
                var TUGs = value.ToString().Split('-');
                switch (TUGs[0])
                {
                    case "A":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => true,
                                "10" => TUGs[1],
                                "11" => TUGs[2],
                                _ => null,
                            };
                        }
                    case "B":
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => true,
                                "01" => false,
                                "10" => TUGs[1],
                                "11" => TUGs[2],
                                _ => null,
                            };
                        }
                    default:
                        {
                            return (parameter.ToString()) switch
                            {
                                "00" => false,
                                "01" => false,
                                "10" => TUGs[1],
                                "11" => TUGs[2],
                                _ => null,
                            };
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
            string[] TUGResultArray;
            if (tempTUGResult is null)
            {
                TUGResultArray = new string[3];
            }
            else
            {
                TUGResultArray = tempTUGResult.ToString().Split('-');
            }
            switch (parameter.ToString())
            {
                case "00":
                    {
                        if ((bool)value == true)
                        {
                            TUGResultArray[0] = "B";
                        }
                        tempTUGResult = string.Join('-', TUGResultArray);
                    }
                    break;
                case "01":
                    {
                        if ((bool)value == true)
                        {
                            TUGResultArray[0] = "A";
                        }
                        tempTUGResult = string.Join('-', TUGResultArray);
                    }
                    break;
                case "10":
                    {
                        TUGResultArray[1] = value.ToString();
                        tempTUGResult = string.Join('-', TUGResultArray);
                    }
                    break;
                case "11":
                    {
                        TUGResultArray[2] = value.ToString();
                        tempTUGResult = string.Join('-', TUGResultArray);
                    }
                    break;
            }
            return tempTUGResult;
        }
    }
    public class HurtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                bool hurt = (bool)value;
                switch (hurt)
                {
                    case true:
                        {
                            if (parameter.ToString() == "1")
                                return true;
                            else
                                return false;
                        }
                    case false:
                        {
                            if (parameter.ToString() == "0")
                                return true;
                            else
                                return false;
                        }                   
                }
            }
            else
                return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                if (parameter.ToString() == "1")
                    return true;
                else
                    return false;
            }
            else
            {
                if (parameter.ToString() == "0")
                    return true;
                else
                    return false;
            }


        }

    }



}

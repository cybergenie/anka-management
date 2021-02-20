using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace Anka2.Models.ExerciseValueConverter
{
    public class ExerciseNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {

                var exerciseNumberList = ((List<Exercise>)value).Select(t => t.ExerciseNumber).ToList();
                for (int i = 0; i < exerciseNumberList.Count; i++)
                {
                    if (exerciseNumberList[i].Substring(0, 8) == ((List<Exercise>)value)[i].basicinfoNumber)
                    {
                        exerciseNumberList[i] = exerciseNumberList[i].Remove(0, 9);
                        var endNumber = exerciseNumberList[i].Substring(exerciseNumberList[i].Length - 3, 3);
                        exerciseNumberList[i] = exerciseNumberList[i].Remove(exerciseNumberList[i].Length - 3, 3);
                        exerciseNumberList[i] = Regex.Replace(exerciseNumberList[i], @"[^0-9]+", "/");
                        exerciseNumberList[i] += endNumber;
                    }
                }
                return exerciseNumberList;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InRoomUpConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            if (value is not null)
            {
                var inRoomUp = ((Exercise)value).InRoomUp;
                switch (parameter)
                {
                    case "05":
                        {
                            switch (inRoomUp)
                            {
                                case false: return true;
                                default: return false;
                            }
                        }
                    case "10":
                        {
                            switch (inRoomUp)
                            {
                                case true: return true;
                                default: return false;
                            }
                        }
                }

            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (tempValue is not null)
            {
                switch (parameter)
                {
                    case "05":
                        {
                            switch (value)
                            {
                                case true:
                                    ((Exercise)tempValue).InRoomUp = false; break;
                                default:
                                    ((Exercise)tempValue).InRoomUp = null; break;
                            }
                            break;
                        }
                    case "10":
                        {
                            switch (value)
                            {
                                case true:
                                    ((Exercise)tempValue).InRoomUp = true; break;
                                default:
                                    ((Exercise)tempValue).InRoomUp = null; break;
                            }
                            break;
                        }
                }
            }
            return tempValue;
        }
    }

    public class DatePickerConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            int index = System.Convert.ToInt32(parameter);
            string strDate = null;
            if (value is not null)
            {
                if (!string.IsNullOrEmpty(((Exercise)value).Date))
                {
                    var Date = ((Exercise)value).Date.Split('|');
                    if (!string.IsNullOrEmpty(Date[index]))
                    {
                        strDate = Date[index];
                    }
                }

            }
            return strDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = System.Convert.ToInt32(parameter);
            if (tempValue is not null)
            {
                var strDate = new string[9];
                if (((Exercise)tempValue).Date is not null)
                {
                    strDate = ((Exercise)tempValue).Date.Split('|');
                }
                if (value is not null)
                {
                    strDate[index] = value.ToString();

                }
                ((Exercise)tempValue).Date = string.Join('|', strDate);

            }
            return tempValue;
        }
    }

    public class BloodPressureConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            int index = System.Convert.ToInt32(parameter);
            string BloodPressure = null;
            if (value is not null)
            {
                if (!string.IsNullOrEmpty(((Exercise)value).BloodPressureUpper))
                {
                    var BloodPressureUpper = ((Exercise)value).BloodPressureUpper.Split('|');
                    if (!string.IsNullOrEmpty(BloodPressureUpper[index]))
                    {
                        BloodPressure += BloodPressureUpper[index];
                    }
                }

                if (!string.IsNullOrEmpty(((Exercise)value).BloodPressureLower))
                {

                    var BloodPressureLower = ((Exercise)value).BloodPressureLower.Split('|');
                    if (!string.IsNullOrEmpty(BloodPressureLower[index]))
                    {
                        BloodPressure += @"/";
                        BloodPressure += BloodPressureLower[index];
                    }
                }
            }
            return BloodPressure;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = System.Convert.ToInt32(parameter);
            if (tempValue is not null)
            {
                var BloodPressureLower = new string[9];
                var BloodPressureUpper = new string[9];
                if (((Exercise)tempValue).BloodPressureLower is not null)
                {
                    BloodPressureLower = ((Exercise)tempValue).BloodPressureLower.Split('|');
                }
                if (((Exercise)tempValue).BloodPressureUpper is not null)
                {
                    BloodPressureUpper = ((Exercise)tempValue).BloodPressureUpper.Split('|');
                }
                if (value is not null)
                {
                    var BloodPressure = ((string)value).Split('/');
                    if (BloodPressure.Length == 2)
                    {
                        var value1 = System.Convert.ToInt32(BloodPressure[0]);
                        var value2 = System.Convert.ToInt32(BloodPressure[1]);
                        BloodPressureLower[index] = (value1 > value2 ? value2 : value1).ToString();
                        BloodPressureUpper[index] = (value1 > value2 ? value1 : value2).ToString();
                    }
                    else
                    {
                        MessageBox.Show("第" + (index + 1) + "个血压值填写错误。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                ((Exercise)tempValue).BloodPressureLower = string.Join('|', BloodPressureLower);
                ((Exercise)tempValue).BloodPressureUpper = string.Join('|', BloodPressureUpper);
            }
            return tempValue;
        }
    }

    public class HeartRateConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            int index = System.Convert.ToInt32(parameter);
            string strHeartRate = null;
            if (value is not null)
            {
                if (!string.IsNullOrEmpty(((Exercise)value).HeartRate))
                {
                    var HeartRate = ((Exercise)value).HeartRate.Split('|');
                    if (!string.IsNullOrEmpty(HeartRate[index]))
                    {
                        strHeartRate = HeartRate[index];
                    }
                }
            }
            return strHeartRate == "0" ? null : strHeartRate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = System.Convert.ToInt32(parameter);
            if (tempValue is not null)
            {
                var HeartRate = new string[9];
                if (((Exercise)tempValue).HeartRate is not null)
                {
                    HeartRate = ((Exercise)tempValue).HeartRate.Split('|');
                }
                if (value is not null)
                {
                    HeartRate[index] = value.ToString();

                }
                ((Exercise)tempValue).HeartRate = string.Join('|', HeartRate);

            }
            return tempValue;
        }
    }

    public class BloodOxygenConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            int index = System.Convert.ToInt32(parameter);
            string strBloodOxygen = null;
            if (value is not null)
            {
                if (!string.IsNullOrEmpty(((Exercise)value).BloodOxygen))
                {
                    var BloodOxygen = ((Exercise)value).BloodOxygen.Split('|');
                    if (!string.IsNullOrEmpty(BloodOxygen[index]))
                    {
                        strBloodOxygen = BloodOxygen[index];
                    }
                }

            }
            return strBloodOxygen == "0" ? null : strBloodOxygen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = System.Convert.ToInt32(parameter);
            if (tempValue is not null)
            {
                var BloodOxygen = new string[9];
                if (((Exercise)tempValue).BloodOxygen is not null)
                {
                    BloodOxygen = ((Exercise)tempValue).BloodOxygen.Split('|');
                }
                if (value is not null)
                {
                    BloodOxygen[index] = value.ToString();

                }
                ((Exercise)tempValue).BloodOxygen = string.Join('|', BloodOxygen);

            }
            return tempValue;
        }

    }

    public class BorgIndexConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            int index = System.Convert.ToInt32(parameter);
            string strBorgIndex = null;
            if (value is not null)
            {
                if (!string.IsNullOrEmpty(((Exercise)value).BorgIndex))
                {
                    var BorgIndex = ((Exercise)value).BorgIndex.Split('|');
                    if (!string.IsNullOrEmpty(BorgIndex[index]))
                    {
                        strBorgIndex = BorgIndex[index];
                    }
                }
            }
            return strBorgIndex == "0" ? null : strBorgIndex;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = System.Convert.ToInt32(parameter);
            if (tempValue is not null)
            {
                var BorgIndex = new string[9];
                if (((Exercise)tempValue).BorgIndex is not null)
                {
                    BorgIndex = ((Exercise)tempValue).BorgIndex.Split('|');
                }
                if (value is not null)
                {
                    BorgIndex[index] = value.ToString();

                }
                ((Exercise)tempValue).BorgIndex = string.Join('|', BorgIndex);

            }
            return tempValue;
        }
    }


    public class RemarksConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            int index = System.Convert.ToInt32(parameter);
            string strRemarks = null;
            if (value is not null)
            {
                if (!string.IsNullOrEmpty(((Exercise)value).Remarks))
                {
                    var Remarks = ((Exercise)value).Remarks.Split('|');
                    if (!string.IsNullOrEmpty(Remarks[index]))
                    {
                        strRemarks = Remarks[index];
                    }
                }

            }
            return strRemarks;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = System.Convert.ToInt32(parameter);
            if (tempValue is not null)
            {
                var Remarks = new string[9];
                if (((Exercise)tempValue).Remarks is not null)
                {
                    Remarks = ((Exercise)tempValue).Remarks.Split('|');
                }
                if (value is not null)
                {
                    Remarks[index] = value.ToString();

                }
                ((Exercise)tempValue).Remarks = string.Join('|', Remarks);

            }
            return tempValue;
        }
    }

    public class ECGsConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            int index = System.Convert.ToInt32(parameter);
            string strECGs = null;
            if (value is not null)
            {
                if (!string.IsNullOrEmpty(((Exercise)value).ECGs))
                {
                    var ECGs = ((Exercise)value).ECGs.Split('|');
                    if (!string.IsNullOrEmpty(ECGs[index]))
                    {
                        strECGs = ECGs[index];
                    }
                }

            }
            return strECGs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = System.Convert.ToInt32(parameter);
            if (tempValue is not null)
            {

                var ECGs = new string[9];
                if (((Exercise)tempValue).Remarks is not null)
                {
                    ECGs = ((Exercise)tempValue).ECGs.Split('|');
                }
                if (value is not null)
                {
                    ECGs[index] = value.ToString();

                }
                ((Exercise)tempValue).ECGs = string.Join('|', ECGs);

            }
            return tempValue;
        }
    }

    public class ChecksConverter : IValueConverter
    {
        private object tempValue = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            tempValue = value;
            var parameterArray = ((string)parameter).ToCharArray();
            int index = System.Convert.ToInt32(parameterArray[0].ToString());
            bool checkItem = parameterArray[1] == '1' ? true : false;
            if (value is not null)
            {
                if (((Exercise)value).Checks is not null)
                {
                    var ChecksArray = ((Exercise)value).Checks.Split('|');
                    switch (ChecksArray[index])
                    {
                        case "0":
                            {
                                switch (checkItem)
                                {
                                    case true:
                                        return false;
                                    case false:
                                        return true;
                                }
                            }
                        case "1":
                            {
                                switch (checkItem)
                                {
                                    case true:
                                        return true;
                                    case false:
                                        return false;
                                }
                            }
                        default:
                            {
                                return false;
                            }
                    }
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterArray = ((string)parameter).ToCharArray();
            int index = System.Convert.ToInt32(parameterArray[0].ToString());
            bool checkItem = parameterArray[1] == '1' ? true : false;
            if (tempValue is not null)
            {
                var ChecksArray = new string[10];
                if (((Exercise)tempValue).Checks is not null)
                {
                    ChecksArray = ((Exercise)tempValue).Checks.Split('|');
                }
                if (value is not null)
                {
                    switch (checkItem)
                    {
                        case true:
                            {
                                switch (value)
                                {
                                    case false: ChecksArray[index] = "0"; break;
                                    case true: ChecksArray[index] = "1"; break;
                                    default: ChecksArray[index] = ""; break;
                                }
                                break;
                            }
                        case false:
                            {
                                switch (value)
                                {
                                    case false: ChecksArray[index] = "1"; break;
                                    case true: ChecksArray[index] = "0"; break;
                                    default: ChecksArray[index] = ""; break;
                                }
                                break;
                            }
                    }
                }
                ((Exercise)tempValue).Checks = string.Join('|', ChecksArray);

            }
            return tempValue;
        }
    }
}

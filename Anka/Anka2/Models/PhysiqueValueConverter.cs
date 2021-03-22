using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace Anka2.Models.PhysiqueValueConverter
{

    public class PhysiqueNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {

                var physiqueNumberList = ((List<Physique>)value).Select(t => t.PhysiqueNumber).ToList();
                for (int i = 0; i < physiqueNumberList.Count; i++)
                {
                    if (physiqueNumberList[i].Substring(0, ((List<Physique>)value)[i].basicinfoNumber.Length) == ((List<Physique>)value)[i].basicinfoNumber)
                    {
                        physiqueNumberList[i] = physiqueNumberList[i].Remove(0, ((List<Physique>)value)[i].basicinfoNumber.Length+1);
                        var endNumber = physiqueNumberList[i].Substring(physiqueNumberList[i].Length - 3, 3);
                        physiqueNumberList[i] = physiqueNumberList[i].Remove(physiqueNumberList[i].Length - 3, 3);
                        physiqueNumberList[i] = Regex.Replace(physiqueNumberList[i], @"[^0-9]+", "/");
                        physiqueNumberList[i] += endNumber;
                    }
                }
                return physiqueNumberList;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BMIConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0, Weight=0, Hight=0;
            if (values is not null)
            {
                if (values[0] is not null)
                {
                    try
                    {
                        Weight = System.Convert.ToDouble(values[0].ToString());
                    }
                    catch (Exception)
                    {
                        Weight = 0;
                    }
                }
                if (values[1] is not null)
                {
                    try
                    {
                        Hight = System.Convert.ToDouble(values[1].ToString());
                    }
                    catch (Exception)
                    {
                        Hight = 0;
                    }
                }
                if (Hight > 0)
                {
                    result = Math.Round(Weight / (Hight * Hight), 2);
                }
            }
            switch (parameter)
            {
                case "BarValue": return result;
                default: return result.ToString();
            }
            


        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class FMPercentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0, FM = 0, Weight = 0;
            if (values is not null)
            {
                if (values[0] is not null)
                {
                    try
                    {
                        FM = System.Convert.ToDouble(values[0].ToString());
                    }
                    catch (Exception)
                    {
                        FM = 0;
                    }
                }
                if (values[1] is not null)
                {
                    try
                    {
                        Weight = System.Convert.ToDouble(values[1].ToString());
                    }
                    catch (Exception)
                    {
                        Weight = 0;
                    }
                }
                if (Weight > 0)
                {
                    result = Math.Round(FM / Weight, 2);
                }
            }
            switch (parameter)
            {
                case "BarValue": return result;
                default: return result.ToString();
            }



        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class FMIndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0, FM = 0, Hight = 0;
            if (values is not null)
            {
                if (values[0] is not null)
                {
                    try
                    {
                        FM = System.Convert.ToDouble(values[0].ToString());
                    }
                    catch (Exception)
                    {
                        FM = 0;
                    }
                }
                if (values[1] is not null)
                {
                    try
                    {
                        Hight = System.Convert.ToDouble(values[1].ToString());
                    }
                    catch (Exception)
                    {
                        Hight = 0;
                    }
                }
                if (Hight > 0)
                {
                    result = Math.Round(FM / (Hight * Hight), 2);
                }
            }
            switch (parameter)
            {
                case "BarValue": return result;
                default: return result.ToString();
            }



        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TBWConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0, TBW = 0, Weight = 0;
            if (values is not null)
            {
                if (values[0] is not null)
                {
                    try
                    {
                        TBW = System.Convert.ToDouble(values[0].ToString());
                    }
                    catch (Exception)
                    {
                        TBW = 0;
                    }
                }
                if (values[1] is not null)
                {
                    try
                    {
                        Weight = System.Convert.ToDouble(values[1].ToString());
                    }
                    catch (Exception)
                    {
                        Weight = 0;
                    }
                }
                if (Weight > 0)
                {
                    result = Math.Round(TBW / Weight, 2);
                }
            }
            switch (parameter)
            {
                case "BarValue": return TBW;
                default: return result.ToString();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BCWConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0, BCW = 0, Weight = 0;
            if (values is not null)
            {
                if (values[0] is not null)
                {
                    try
                    {
                        BCW = System.Convert.ToDouble(values[0].ToString());
                    }
                    catch (Exception)
                    {
                        BCW = 0;
                    }
                }
                if (values[1] is not null)
                {
                    try
                    {
                        Weight = System.Convert.ToDouble(values[1].ToString());
                    }
                    catch (Exception)
                    {
                        Weight = 0;
                    }
                }
                if (Weight > 0)
                {
                    result = Math.Round(BCW / Weight, 2);
                }
            }
            switch (parameter)
            {
                case "BarValue": return BCW;
                default: return result.ToString();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class EBWConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0, BCW = 0, TBW = 0;
            if (values is not null)
            {
                if (values[0] is not null)
                {
                    try
                    {
                        BCW = System.Convert.ToDouble(values[0].ToString());
                    }
                    catch (Exception)
                    {
                        BCW = 0;
                    }
                }
                if (values[1] is not null)
                {
                    try
                    {
                        TBW = System.Convert.ToDouble(values[1].ToString());
                    }
                    catch (Exception)
                    {
                        TBW = 0;
                    }
                }
                if (TBW > 0)
                {
                    result = Math.Round(BCW / TBW, 3)*100;
                }
            }
            switch (parameter)
            {
                case "BarValue": return result;
                default: return result.ToString();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }




}

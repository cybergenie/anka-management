using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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

    
   

}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Anka2.CustomControls
{
    class ColorValueBarConverter : IMultiValueConverter
    {  
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double IndicatorValue=0,
                IndicatorStart=0, 
                IndicatorOneText=0, 
                IndicatorTwoText=0, 
                IndicatorThreeText=0,
                IndicatorEnd=0,
                CellOneLength=0,
                CellTwoLength=0,
                CellThreeLength=0,
                CellFourLength=0;
            double IndicatorPosition = 0;
            if (values is not null)
            {
                IndicatorValue = (double)values[0];
                IndicatorStart = (double)values[1];
                IndicatorOneText = (double)values[2];
                IndicatorTwoText = (double)values[3];
                IndicatorThreeText = (double)values[4];
                IndicatorEnd = (double)values[5];
                CellOneLength = (double)values[6];
                CellTwoLength = (double)values[7];
                CellThreeLength = (double)values[8];
                CellFourLength = (double)values[9];

                if (IndicatorValue < IndicatorStart)
                {
                    IndicatorPosition = 0;
                }
                if (IndicatorValue >= IndicatorStart && IndicatorValue < IndicatorOneText)
                {
                    IndicatorPosition = (IndicatorValue - IndicatorStart) * (CellOneLength / (IndicatorOneText - IndicatorStart));
                }
                if (IndicatorValue >= IndicatorOneText && IndicatorValue < IndicatorTwoText)
                {
                    IndicatorPosition = (IndicatorValue - IndicatorOneText) * (CellTwoLength) / (IndicatorTwoText - IndicatorOneText) + CellOneLength;
                }
                if (IndicatorValue >= IndicatorTwoText && IndicatorValue < IndicatorThreeText)
                {
                    IndicatorPosition = (IndicatorValue - IndicatorTwoText) * (CellThreeLength) / (IndicatorThreeText - IndicatorTwoText) + CellOneLength + CellTwoLength;
                }
                if (IndicatorValue >= IndicatorThreeText && IndicatorValue < IndicatorEnd)
                {
                    IndicatorPosition = (IndicatorValue - IndicatorThreeText) * (CellFourLength) / (IndicatorEnd - IndicatorThreeText) + CellOneLength + CellTwoLength + CellThreeLength;
                }
                if (IndicatorValue > IndicatorEnd)
                {
                    IndicatorPosition = CellOneLength + CellTwoLength + CellThreeLength + CellFourLength;
                }

            }

          
            return IndicatorPosition;
        }

       

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

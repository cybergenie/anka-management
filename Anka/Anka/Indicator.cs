using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Anka
{
    class Indicator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static double _indicatorValue=0;   
       
        public double IndicatorValue
        {
            get { return _indicatorValue; }
            set
            {
                _indicatorValue = value;                
                OnPropertyChanged("IndicatorValue");  
            }
        }
                
        public struct IndicatorTri
        {
            public Point StartUpper;           
            public Point EndUpper;
            public Point Lower;    

            public IndicatorTri(Point _StartUpper, Point _EndUpper, Point _Lower)
            {
                StartUpper = _StartUpper;
                EndUpper = _EndUpper;
                Lower = _Lower;
            }
        };
       
        protected virtual void OnPropertyChanged(string propertyName)

        {

            if (PropertyChanged != null)

            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }

        }

        public double ToDouble(string str)
        {
            double result = 0;
            if (str != null)
            {

                string pattern = @"^(-?\d+)(\.\d+)?$";
                Match match = Regex.Match(str, pattern);
                if (match.Success)
                    result = Convert.ToDouble(str);
                else
                    result = 0;
                if (result < 0)
                    result = 0;
            }
            return result;
        }            

    }
    
}

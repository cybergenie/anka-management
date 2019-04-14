using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Anka
{
    static class DataAdapter
    {
        /// info
       
        public static string Name { set; get; }
        public static int Number { set; get; }
        public static int Age { set; get; }

        /// <summary>
        /// Male取值：
        /// true：男性
        /// false：女性
        /// </summary>
        public static Boolean Male { set; get; }

        ///
        public static string Killip { set; get; }
        public static string EF { set; get; }
        public static string LV { set; get; }
        public static string BasicOther { set; get; }
        public static int BasicRisk { set; get; } = 0b10000000000000;
        public static string RiskOther { get; set; }
        public static int PCI { get; set; }
        public static int ResidualStenosis { get; set; }
        public static Boolean CollatCirc { get; set; }


        /// <summary>
        /// DominantCoronary取值:
        /// 1:左优势型
        /// 2：均衡型
        /// 3：右优势型
        /// </summary>
        public static int DominantCoronary { get; set; }


        public static Boolean ToInt(string str, out int result)
        {
            Boolean temp = false;
            str = str.Trim(' ');
            if (str != null)
            {

                string pattern = @"^(-?\d+)(\.\d+)?$";
                Match match = Regex.Match(str, pattern);
                if (match.Success)
                {
                    result = Convert.ToInt32(str);
                    temp = true;
                }
                else
                    result = 0;               
            }

            else
                result = 0;
            
            return temp;
        }        

    }
    
}

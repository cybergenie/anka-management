using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Anka
{
    class config
    {
        private static string dataSource = @"db/anka.db";
        public static string DataSource
        {
            set
            {
                dataSource = value;
            }
            get
            {
                //return dataSource;
                return string.Format("data source={0}", dataSource);
            }
        }

        public static bool ConStatus { get; set; } = false;

        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
        public static bool IsUnsign(string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }

        public static bool isTel(string strInput)
        {
            return Regex.IsMatch(strInput, @"\d{3}-\d{8}|\d{4}-\d{7}");
        }

    }
}

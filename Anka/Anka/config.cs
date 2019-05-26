using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}

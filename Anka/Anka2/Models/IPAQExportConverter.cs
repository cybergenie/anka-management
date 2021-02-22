using Anka2.Services;
using System.Data;

namespace Anka2.Models
{
    public class IPAQExportConverter
    {
        public static void IPAQValueConvertor(ref DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataUitls.GenderConvertor(dr, "性别");
                DataUitls.DateConvertor(dr, "记录编号");
                IPAQ0ResultConvertor(dr, "是否知道");
                IPAQ5ResultConvertor(dr, "IPAQ-5(步/天)");
            }
        }

        private static void IPAQ0ResultConvertor(DataRow dr, string columnName)
        {            
            var IPAQResult = dr[columnName] switch
            {
                "True" => "是",
                "False" => "否",
                _ => string.Empty,

            };            
            dr[columnName] = IPAQResult;
        }

        private static void IPAQ5ResultConvertor(DataRow dr, string columnName)
        {
            var IPAQResult = dr[columnName] switch
            {
                "0" => "<2000步",
                "1" => "2000-3000步",
                "2" => "3000-4000步",
                "3" => "4000-5000步",
                "4" => "5000-7000步",
                "5" => "7000到1万步",
                "6" => "1万步以上",
                _ => string.Empty,

            };
            dr[columnName] = IPAQResult;
        }

    }
}

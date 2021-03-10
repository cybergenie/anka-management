using Anka2.Services;
using System.Data;

namespace Anka2.Models
{
    public class SPPBExportConverter
    {
        public static void SPPBValueConvertor(ref DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataUitls.GenderConvertor(dr, "性别");
                DataUitls.DateConvertor(dr, "记录编号"); 
            }
        }

        private static void BalanceTesting1Convertor(DataRow dr, string columnName)
        {
            string SPPBResult = dr[columnName] as string;
            int result = 0;

            string[] strResults = SPPBResult.Split('|');
            foreach (var strResult in strResults)
            {
                if (!string.IsNullOrEmpty(strResult))
                    if (System.Convert.ToInt32(strResult) > 0)
                    {
                        result += System.Convert.ToInt32(strResult);
                    }
            }
            dr[columnName] = result.ToString();
        }

    }
}

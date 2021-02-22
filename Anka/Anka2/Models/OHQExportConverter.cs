using Anka2.Services;
using System.Data;

namespace Anka2.Models
{
    public class OHQExportConverter
    {
        public static void OHQValueConvertor(ref DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataUitls.GenderConvertor(dr, "性别");
                DataUitls.DateConvertor(dr, "记录编号");
                OHQ5ResultConvertor(dr, "OHQ-5");
            }
        }

        private static void OHQ5ResultConvertor(DataRow dr, string columnName)
        {
            string PHQResult = dr[columnName] as string;
            int result = 0;

            string[] strResults = PHQResult.Split('|');
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

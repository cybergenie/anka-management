using Anka2.Services;
using System.Data;

namespace Anka2.Models
{
    public class PHQExportConverter
    {
        public static void PHQValueConvertor(ref DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataUitls.GenderConvertor(dr, "性别");
                DataUitls.DateConvertor(dr, "记录编号");
                PHQResultConvertor(dr, "PHQ评分");
            }
        }

        private static void PHQResultConvertor(DataRow dr, string columnName)
        {
            string PHQResult = dr[columnName] as string;
            int result = 0;

            string[] strResults = PHQResult.Split('|');
            foreach (var strResult in strResults)
            {
                if (!string.IsNullOrEmpty(strResult))
                    result += System.Convert.ToInt32(strResult);
            }
            dr[columnName] = result.ToString();
        }

    }
}

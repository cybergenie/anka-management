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
                BalanceTestingConvertor(dr, "平衡测试-1");
                BalanceTestingConvertor(dr, "平衡测试-2");
                BalanceTestingConvertor(dr, "平衡测试-3");
                WalkingTestingConvertor(dr, "4米行走-1");
                WalkingTestingConvertor(dr, "4米行走-2");
            }
        }

        private static void BalanceTestingConvertor(DataRow dr, string columnName)
        {
            string BalanceTestingResult = dr[columnName] as string;
            string ConvertorResult = null;
            if (!string.IsNullOrEmpty( BalanceTestingResult))
            {
                string[] strResults = BalanceTestingResult.Split('-');
                if (string.IsNullOrEmpty(strResults[1]))
                {
                    strResults[1] = "0";
                }
                if (string.IsNullOrEmpty(strResults[2]))
                {
                    strResults[2] = "0";
                }
                switch (strResults[0])
                {
                    case "A":
                        ConvertorResult = "是"; break;
                    case "B":
                        {
                            ConvertorResult = "否";
                            if (strResults[1] != "0" || strResults[2] != "0")
                            {
                                ConvertorResult += ",用时" + strResults[1] + "秒" + strResults[2] + "分秒";
                            }
                            break;
                        }
                }
                
            }
            dr[columnName] = ConvertorResult;
        }
        private static void WalkingTestingConvertor(DataRow dr, string columnName)
        {
            string WalkingTestingResult = dr[columnName] as string;
            string ConvertorResult = null;
            if (!string.IsNullOrEmpty(WalkingTestingResult))
            {
                string[] strResults = WalkingTestingResult.Split('-');
                if (string.IsNullOrEmpty(strResults[1]))
                {
                    strResults[1] = "0";
                }
                if (string.IsNullOrEmpty(strResults[2]))
                {
                    strResults[2] = "0";
                }
                switch (strResults[0])
                {
                    case "A":
                        {
                            ConvertorResult = "有辅助工具";                            
                        }
                        break;
                    case "B":
                        {
                            ConvertorResult = "无辅助工具";                           

                        }
                        break;                    
                }
                if (strResults[1] != "0" || strResults[2] != "0")
                {
                    ConvertorResult += ",用时" + strResults[1] + "秒" + strResults[2] + "分秒";
                }

            }
            dr[columnName] = ConvertorResult;
        }

    }
}

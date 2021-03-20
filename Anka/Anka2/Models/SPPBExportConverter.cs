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
                SitUpTestingConvertor(dr, "坐起立");
                TUGConverter(dr, "3米往返步行(TUG)");
                BloodPressureConverter(dr, "血压");
                HurtConvertor(dr, "左手外伤");
                HurtConvertor(dr, "右手外伤");
                HurtConvertor(dr, "左腿外伤");
                HurtConvertor(dr, "右腿外伤");
            }
            dt.Columns.Remove("BloodPressureLower");

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
        private static void SitUpTestingConvertor(DataRow dr, string columnName)
        {
            string SitUpTestingResult = dr[columnName] as string;
            string ConvertorResult = null;
            if (!string.IsNullOrEmpty(SitUpTestingResult))
            {
                string[] strResults = SitUpTestingResult.Split('-');               
                switch (strResults[0])
                {
                    case "A":
                        {
                            ConvertorResult = "5次";
                            if (string.IsNullOrEmpty(strResults[1]))
                            {
                                strResults[1] = "0";
                            }
                            if (string.IsNullOrEmpty(strResults[2]))
                            {
                                strResults[2] = "0";
                            }
                            if (strResults[1] != "0" || strResults[2] != "0")
                            { if (string.IsNullOrEmpty(strResults[1]))
                {
                    strResults[1] = "0";
                }
                if (string.IsNullOrEmpty(strResults[2]))
                {
                    strResults[2] = "0";
                }
                                ConvertorResult += ",用时" + strResults[1] + "秒" + strResults[2] + "分秒";
                            }
                        }
                        break;
                    case "B":
                        {
                            ConvertorResult = "不能完成5次";
                            if (!string.IsNullOrEmpty(strResults[2]))
                            {
                                ConvertorResult += ",可完成" + strResults[2] + "次";
                            }
                            
                        }
                        break;
                }
               

            }
            dr[columnName] = ConvertorResult;
        }      
        private static void  TUGConverter(DataRow dr, string columnName)
        {
            string SitUpTestingResult = dr[columnName] as string;
            string ConvertorResult = null;
            if (!string.IsNullOrEmpty(SitUpTestingResult))
            {
                string[] strResults = SitUpTestingResult.Split('-');
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
                           
                            if (strResults[1] != "0" || strResults[2] != "0")
                            {                               
                                ConvertorResult += ",用时" + strResults[1] + "秒" + strResults[2] + "分秒";
                            }
                        }
                        break;
                    case "B":
                        {
                            ConvertorResult = "无辅助工具";
                            if (!string.IsNullOrEmpty(strResults[2]))
                            {
                                ConvertorResult += ",用时" + strResults[1] + "秒" + strResults[2] + "分秒";
                            }

                        }
                        break;
                }
            }
            dr[columnName] = ConvertorResult;
        }
        private static void BloodPressureConverter(DataRow dr, string columnName)
        {
            string BloodPressureResult = dr[columnName] as string;
            string ConvertorResult = null;
            if (!string.IsNullOrEmpty(BloodPressureResult))
            {
                ConvertorResult += BloodPressureResult;
                ConvertorResult += "/";
                ConvertorResult += dr["BloodPressureLower"].ToString();
            }
            dr[columnName] = ConvertorResult;
        }
        public static void HurtConvertor(DataRow dr, string columnName)
        {
            var gender = dr[columnName] switch
            {
                "True" => "是",
                "False" => "否",
                _ => string.Empty,
            };
            dr[columnName] = gender;
        }

    }
}

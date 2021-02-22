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
                OHQ1ResultConvertor(dr, "OHQ-1");
                OHQ2ResultConvertor(dr, "OHQ-2");
                OHQ3ResultConvertor(dr, "OHQ-3");
                OHQ4ResultConvertor(dr, "OHQ-4");
                OHQ5ResultConvertor(dr, "OHQ-5");
                OHQ6ResultConvertor(dr, "OHQ-6");
                OHQ7ResultConvertor(dr, "OHQ-7");
                OHQ8ResultConvertor(dr, "OHQ-8");
                OHQ9ResultConvertor(dr, "OHQ-9");
            }
        }

        private static void OHQ1ResultConvertor(DataRow dr, string columnName)
        {
            var OHQResult = dr[columnName] switch
            {
                "0" => "3次以上",
                "1" => "2次",
                "2" => "1次",
                "3" => "0次（一次也不刷也有）",                
                _ => string.Empty,

            };
            dr[columnName] = OHQResult;
        }
        private static void OHQ2ResultConvertor(DataRow dr, string columnName)
        {
            var OHQResult = dr[columnName] switch
            {
                "0" => "每天",
                "1" => "有时使用",
                "2" => "不使用",               
                _ => string.Empty,

            };
            dr[columnName] = OHQResult;
        }
        private static void OHQ3ResultConvertor(DataRow dr, string columnName)
        {           
            var OHQResult = dr[columnName] switch
            {
                "0" => "1个",
                "1" => "2个",
                "2" => "3-5个",
                "3" => "6-9个",
                "4" => "10个及以上",
                "5" => "0个（没掉）",
                _ => string.Empty,

            };
            dr[columnName] = OHQResult;
        }
        private static void OHQ4ResultConvertor(DataRow dr, string columnName)
        {
            var OHQResults = dr[columnName].ToString().Split('-');
            string OHQResult;
            switch (OHQResults[0])
            {
                case "A":
                    {
                        OHQResult = "经常";
                        if (!string.IsNullOrEmpty(OHQResults[1]))
                        {
                            OHQResult += "(每周" + OHQResults[1].ToString() + "次)";
                        }
                    }break;
                case "B":
                    {
                        OHQResult = "偶尔";
                        if (!string.IsNullOrEmpty(OHQResults[1]))
                        {                            
                            OHQResult += "(每周" + OHQResults[1].ToString() + "次)";
                        }break;
                    }
                case "C":
                    {
                        OHQResult = "无(不出血)";                       
                    }break;
                default: OHQResult = null;break;

            }
            dr[columnName] = OHQResult;
        }
        private static void OHQ5ResultConvertor(DataRow dr, string columnName)
        {
            var OHQResult = dr[columnName] switch
            {
                "0" => "从来没有",
                "1" => "现在有",
                "2" => "现在没有，但有时复发",
                "3" => "以前有，现在好了",                
                _ => string.Empty,

            };
            dr[columnName] = OHQResult;
        }
        private static void OHQ6ResultConvertor(DataRow dr, string columnName)
        {
            var OHQResult = dr[columnName] switch
            {
                "0" => "否",
                "99" => "全部替换",                
                null => string.Empty,
                _ => "部分是（共" + dr[columnName].ToString() + "颗假牙)"

            };
            dr[columnName] = OHQResult;
        }
        private static void OHQ7ResultConvertor(DataRow dr, string columnName)
        {
            var OHQResult = dr[columnName] switch
            {
                "0" => "清水浸泡",
                "1" => "假牙清洁剂浸泡",
                "2" => "用牙膏刷",               
                _ => string.Empty

            };
            dr[columnName] = OHQResult;
        }
        private static void OHQ8ResultConvertor(DataRow dr, string columnName)
        {
            var OHQResult = dr[columnName] switch
            {
                "0" => "<1分钟",
                "1" => "1-2分钟",
                "2" => "2分钟以上",
                _ => string.Empty

            };
            dr[columnName] = OHQResult;
        }
        private static void OHQ9ResultConvertor(DataRow dr, string columnName)
        {
            var OHQResults = dr[columnName].ToString().Split('-');
            string OHQResult;
            switch (OHQResults[0])
            {
                case "A":
                    {
                        OHQResult = "否";                       
                    }
                    break;
                case "B":
                    {
                        OHQResult = "是（";
                        if (!string.IsNullOrEmpty(OHQResults[1]))
                        {
                            OHQResult += "常规检查" + OHQResults[1].ToString() + "次 / 年; ";
                        }
                        if (!string.IsNullOrEmpty(OHQResults[2]))
                        {
                            OHQResult += "生病" + OHQResults[2].ToString() + "次/年;";
                        }
                        OHQResult += ")";
                        break;
                    }               
                default: OHQResult = null; break;

            }
            dr[columnName] = OHQResult;
        }
    }
}

using Anka2.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anka2.Models
{
    public class BasicInfoExportConverter
    {
        public static void BasicInfoValueConvertor(ref DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataUitls.GenderConvertor(dr, "性别");
                CollatCircConvertor(dr, "侧枝循环");
                DominantCoronaryConvertor(dr, "优势冠脉");
                BasicRiskConvertor(dr, "危险因素");
            }
        }

        private static void CollatCircConvertor(DataRow dr, string columnName)
        {
            var CollatCirc = dr[columnName] switch
            {
                "True" => "有",
                "False" => "无",
                _ => string.Empty,
            };
            dr[columnName] = CollatCirc;
        }
        private static void DominantCoronaryConvertor(DataRow dr, string columnName)
        {
            var DominantCoronary = dr[columnName] switch
            {
                "1" => "左优势型",
                "0" => "均衡型",
                "-1" => "右优势型",
                _ => string.Empty,
            };
            dr[columnName] = DominantCoronary;
        }
        private static void BasicRiskConvertor(DataRow dr, string columnName)
        {
            if (!String.IsNullOrEmpty(dr[columnName].ToString()))
            {
                char[] chBasicRisk = dr[columnName].ToString().ToCharArray();
                dr[columnName] = string.Empty;
                for (int i = 0; i < chBasicRisk.Length; i++)
                {
                    if (chBasicRisk[i] == '1')
                    {
                        var BasicRisk = i switch
                        {
                            0 => "高血压, ",
                            1 => "糖尿病, ",
                            2 => "脑卒中, ",
                            3 => "吸烟, ",
                            4 => "高LDL-C, ",
                            5 => "高TG, ",
                            6 => "肥胖, ",
                            7 => "痛风, ",
                            8 => "运动不足, ",
                            9 => "周围动脉硬化闭塞, ",
                            10 => "肾功能不全CRE, ",
                            11 => "肝功能异常ALT, ",
                            _ => string.Empty,
                        };
                        dr[columnName] += BasicRisk;
                    }
                }
            }

        }
    }
}

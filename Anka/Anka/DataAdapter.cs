using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using System.Data;
using System.Windows;
using System.Diagnostics;

namespace Anka
{
    class DataAdapter
    {
        
        /// <summary>
        /// 基本信息
        /// </summary>
        public static string Name { set; get; }
        public static string Number { set; get; }
        public static int Age { set; get; }
        public static bool Male { set; get; } //true:男   false:女;

        public static bool loadNewPerson { set; get; } = false;       


        public static bool IsNumber(string str)
        {
            bool result = false;
            str = str.Trim();
            if (str != null)
            {

                string pattern = @"^(-?\d+)(\.\d+)?$";
                Match match = Regex.Match(str, pattern);
                if (match.Success)
                {
                    result = true;                   
                }                        
            }           
            
            return result;
        } 
       

        public static string ArrayToString(int[] data)
        {
            string temp = "";

            foreach (var s in data)
            {
                temp += (s.ToString() + "|");
            }

            return temp;
        }

        public static string ArrayToString(string[] data)
        {
            string temp = "";

            foreach (var s in data)
            {
                if(s!=null)
                {
                    temp += (s.ToString() + "|");
                }
                else
                {
                    temp += ("" + "|");
                }
                
            }

            return temp;
        }

        public static string ArrayToString(bool[] data)
        {
            string temp = "";

            foreach (var s in data)
            {
                int p = -1;
                if (s == true)
                { p = 1; }
                else if(s == false)
                { p = 0; }
                temp += (p.ToString() + "|");
            }

            return temp;
        }

        public static DataTable DataTableConverter(DataTable dt,int index)
        {
            DataTable dtOutput;
            switch (index)
            {
                case 0:
                    dtOutput=BasicinfoConverter(dt);
                    break;
                case 1:
                    dtOutput=ExerciseConverter(dt);
                    break;
                case 2:
                    dtOutput=PHQConverter(dt);
                    break;
                case 3:
                    dtOutput=GADConverter(dt);
                    break;
                case 4:
                    dtOutput=IPAQConverter(dt);
                    break;
                case 5:
                    dtOutput=OHQConverter(dt);
                    break;
                case 6:
                    dtOutput=SPPBConverter(dt);
                    break;
                case 7:
                    dtOutput=PhysiqueConverter(dt);
                    break;
                default:
                    dtOutput = dt;
                    break;

            }

            return dtOutput;
        }

        private static DataTable BasicinfoConverter(DataTable dt)
        {
            string[] Risks = {"高血压","糖尿病","脑卒中","吸烟","高LDL-C","高TG","肥胖","痛风","运动不足","周围动脉硬化闭塞", "肾功能不全CRE","肝功能异常ALT","其他:" };

            DataTable dtOutput = dt.Copy();         


            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns["性别"].SetOrdinal(3);
            dtOutput.Columns["Killip"].ColumnName = "Killip";
            dtOutput.Columns["EF"].ColumnName = "EF";
            dtOutput.Columns["LV"].ColumnName = "LV";
            dtOutput.Columns["BasicOther"].ColumnName = "其他";
            dtOutput.Columns["BasicRisk"].ColumnName = "危险因素";
            dtOutput.Columns["PCI"].ColumnName = "PCI支架数";
            dtOutput.Columns["ResidualStenosis"].ColumnName = "75%以上残余狭窄";
            dtOutput.Columns.Add("侧枝循环", typeof(String));
            dtOutput.Columns.Add("优势冠脉", typeof(String));

            foreach (DataRow dRow in dtOutput.Rows)
            {

                string tempRisk = null;
                string risk = dRow["危险因素"].ToString();
                for (int i = 0; i < risk.Length; i++)
                {
                    if (risk[i] == '1')
                    {
                        tempRisk += (Risks[i] + ";");
                    }

                }
                dRow["危险因素"] = tempRisk + dRow["RiskOther"];

                

                switch (dRow["Male"].ToString())
                {
                    case "True":
                        dRow["性别"] = "男";
                        break;
                    case "False":
                        dRow["性别"] = "女";
                        break;
                    default:
                        dRow["性别"] = "";
                        break;
                }
                

                switch (dRow["CollatCirc"].ToString())
                {
                    case "True":
                        dRow["侧枝循环"] = "有";
                        break;
                    case "False":
                        dRow["侧枝循环"] = "无";
                        break;
                    default:
                        dRow["侧枝循环"] = "";
                        break;
                }
                

                switch (dRow["DominantCoronary"].ToString())
                {
                    case "-1":
                        dRow["优势冠脉"] = "左优势型";
                        break;
                    case "0":
                        dRow["优势冠脉"] = "均衡型";
                        break;
                    case "1":
                        dRow["优势冠脉"] = "右优势型";
                        break;
                    default:
                        dRow["优势冠脉"] = "";
                        break;
                }   
            }
            dtOutput.Columns.Remove("DominantCoronary");
            dtOutput.Columns.Remove("Male");
            dtOutput.Columns.Remove("RiskOther");
            dtOutput.Columns.Remove("CollatCirc");
            return dtOutput;
        }
        private static DataTable ExerciseConverter(DataTable dt)
        {
            DataTable dtOutput = null;
            return dtOutput;
        }
        private static DataTable GADConverter(DataTable dt)
        {
            DataTable dtOutput = null;
            return dtOutput;
        }
        private static DataTable IPAQConverter(DataTable dt)
        {
            DataTable dtOutput = null;
            return dtOutput;
        }
        private static DataTable OHQConverter(DataTable dt)
        {
            DataTable dtOutput = null;
            return dtOutput;
        }
        private static DataTable PHQConverter(DataTable dt)
        {
            DataTable dtOutput = null;
            return dtOutput;
        }
        private static DataTable PhysiqueConverter(DataTable dt)
        {
            DataTable dtOutput = null;
            return dtOutput;
        }
        private static DataTable SPPBConverter(DataTable dt)
        {
            DataTable dtOutput = null;
            return dtOutput;
        }

    }

}

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
                    dtOutput=PAQConverter(dt);
                    break;
                case 3:
                    dtOutput= PhysiqueConverter(dt);
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
            dtOutput.TableName = "01-基本信息";


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
            DataTable dtOutput = dt.Copy();
            dtOutput.TableName = "02-运动负荷记录表";           

            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            dtOutput.Columns["ExerciseNumber"].ColumnName = "记录编号";

            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns["性别"].SetOrdinal(3);
            dtOutput.Columns.Add("床上负荷", typeof(String));            
            dtOutput.Columns.Add("室内负荷", typeof(String));
            dtOutput.Columns.Add("室外负荷", typeof(String));
            dtOutput.Columns.Add("院外负荷", typeof(String));


            foreach (DataRow dRow in dtOutput.Rows)
            {
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

                string[] strChecks = (dRow["Checks"].ToString()).Split('|');

                int p = 0;
                for (int i = 0; i < strChecks.Length; i++)
                {
                    if (strChecks[i] == "1")
                    {
                        p++;
                    }
                    if (strChecks[i] == "0")
                    {
                        p += 10; ;
                    }

                    if(i==3)
                    {
                        if (p == 0)
                        {
                            dRow["床上负荷"] = " ";
                        }
                        else if (p > 0 && p < 10)
                        {
                            dRow["床上负荷"] = "是";
                        }
                        else if (p >= 10)
                        {
                            dRow["床上负荷"] = "否";
                        }

                        p = 0;
                    }

                    if (i == 5)
                    {
                        if (p == 0)
                        {
                            dRow["室内负荷"] = " ";
                        }
                        else if (p > 0 && p < 10)
                        {
                            dRow["室内负荷"] = "是";
                        }
                        else if (p >= 10)
                        {
                            dRow["室内负荷"] = "否";
                        }

                        p = 0;
                    }

                    if (i == 8)
                    {
                        if (p == 0)
                        {
                            dRow["室外负荷"] = " ";
                        }
                        else if (p > 0 && p < 10)
                        {
                            dRow["室外负荷"] = "是";
                        }
                        else if (p >= 10)
                        {
                            dRow["室外负荷"] = "否";
                        }

                        p = 0;
                    }

                }

                if (p == 0)
                {
                    dRow["院外负荷"] = " ";
                }
                else if (p > 0 && p < 10)
                {
                    dRow["院外负荷"] = "是";
                }
                else if (p >= 10)
                {
                    dRow["院外负荷"] = "否";
                }                

            }

            string[] colDels = { "Killip", "EF", "LV", "BasicOther" , "BasicRisk", "RiskOther",
           "PCI", "ResidualStenosis","DominantCoronary","InRoomUp","Date","BloodPressureLower",
            "BloodPressureUpper","HeartRate","BloodOxygen","BorgIndex","Remarks","CollatCirc","basicinfo_Number","Checks","ECGs","Male"};
            foreach (string colDel in colDels)
            {
                dtOutput.Columns.Remove(colDel);
            }

            return dtOutput;
        }
        private static DataTable PAQConverter(DataTable dt)
        {
            DataTable dtOutput = dt.Copy();

            dtOutput.TableName = "03-身体活动水平";

            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            //dtOutput.Columns["ExerciseNumber"].ColumnName = "记录编号";

            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns["性别"].SetOrdinal(3);

            dtOutput.Columns.Add("IPAQ-0是否知道", typeof(String));
            dtOutput.Columns["IPAQ-0是否知道"].SetOrdinal(9);
            dtOutput.Columns["IPAQ1"].ColumnName = "IPAQ-1";
            dtOutput.Columns["IPAQ2"].ColumnName = "IPAQ-2";
            dtOutput.Columns["IPAQ3"].ColumnName = "IPAQ-3";
            dtOutput.Columns["IPAQ4"].ColumnName = "IPAQ-4";
            dtOutput.Columns["IPAQ5"].ColumnName = "IPAQ-5";

            dtOutput.Columns.Add("GAD-7", typeof(String));
            dtOutput.Columns["GAD-7"].SetOrdinal(7);
            dtOutput.Columns.Add("PHQ-9", typeof(String));
            dtOutput.Columns["PHQ-9"].SetOrdinal(5);

            dtOutput.Columns["BalanceTesting1"].ColumnName = "平衡能力";
            dtOutput.Columns["walkingTesting1"].ColumnName = "4米行走";
            dtOutput.Columns["SitUpTesting"].ColumnName = "坐起立";
            dtOutput.Columns["Hight"].ColumnName = "身高";
            dtOutput.Columns["Weight"].ColumnName = "体重";
            dtOutput.Columns["Waistline"].ColumnName = "腰围";
            dtOutput.Columns["Hipline"].ColumnName = "臀围";
            dtOutput.Columns["ArmlineLeft"].ColumnName = "上臂围度(左)";
            dtOutput.Columns["ArmlineRight"].ColumnName = "上臂围度(右)";
            dtOutput.Columns["LeglineLeft"].ColumnName = "大腿围度(左)";
            dtOutput.Columns["LeglineRight"].ColumnName = "大腿围度(右)";

            foreach (DataRow dRow in dtOutput.Rows)
            {
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
                switch (dRow["IPAQ0"].ToString())
                {
                    case "True":
                        dRow["IPAQ-0是否知道"] = "是";
                        break;
                    case "False":
                        dRow["IPAQ-0是否知道"] = "否";
                        break;
                    default:
                        dRow["IPAQ-0是否知道"] = " ";
                        break;
                }

                string[] strGADs = (dRow["GADResult"].ToString()).Split('|');
                int GAD_7 = 0;
                foreach(string strGAD in strGADs)
                {
                    if (strGAD.Length > 0)
                    {


                        int nGAD = Convert.ToInt32(strGAD);
                        if (nGAD > 0)
                        {
                            GAD_7 += nGAD;
                        }
                    }
                }

                dRow["GAD-7"] = GAD_7.ToString();

                string[] strPHQs = (dRow["PHQResult"].ToString()).Split('|');
                int PHQ_9 = 0;
                foreach (string strPHQ in strPHQs)
                {
                    if (strPHQ.Length > 0)
                    {


                        int nPHQ = Convert.ToInt32(strPHQ);
                        if (nPHQ > 0)
                        {
                            PHQ_9 += nPHQ;
                        }
                    }
                }

                dRow["PHQ-9"] = PHQ_9.ToString();
            }

            string[] colDels = { "Killip", "EF", "LV", "BasicOther" , "BasicRisk", "RiskOther",
           "PCI", "ResidualStenosis","DominantCoronary","CollatCirc","basicinfo_Number","Male","basicinfo_Number1","basicinfo_Number2","basicinfo_Number3",
            "GADResult","PHQResult","BalanceTesting2","BalanceTesting3","walkingTesting2","TUG","FRTLeft1","FRTLeft2","FRTRight1","FRTRight2","SFO1","SFO2","OneFootLeft1",
            "OneFootLeft2","OneFootRight1","OneFootRight2","IPAQ0","BloodPressureUpper","BloodPressureLower","HeartRate","Temperature","Breathe","LeftHandHurt","RightHandHurt",
                       "LeftLapHurt","RightLapHurt","LapStrengthLeft1","LapStrengthRight1","LapStrengthLeft2","LapStrengthRight2"};
            foreach (string colDel in colDels)
            {
                dtOutput.Columns.Remove(colDel);
            }

            return dtOutput;
        }       
        private static DataTable PhysiqueConverter(DataTable dt)
        {
            DataTable dtOutput = dt.Copy();
            return dtOutput;
        }        
    }

}

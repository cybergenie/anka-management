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
            dtOutput.Columns["D2"].ColumnName = "D-二聚体";
            dtOutput.Columns.Add("侧枝循环", typeof(String));
            dtOutput.Columns.Add("优势冠脉", typeof(String));
            if(dtOutput.Columns.Contains("Description")==true)
            {
                dtOutput.Columns["Description"].ColumnName = "诊断";
                dtOutput.Columns["诊断"].SetOrdinal(4);
            }
            try
            {
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
            }
            catch(Exception ex)
            {
                MessageBox.Show("基本信息"+ex.Message);
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
            dtOutput.Columns.Add("床上负荷", typeof(int));            
            dtOutput.Columns.Add("室内负荷", typeof(int));
            dtOutput.Columns.Add("室外负荷", typeof(int));
            dtOutput.Columns.Add("院外负荷", typeof(int));

            dtOutput.Columns.Add("GAD-7", typeof(String));
            dtOutput.Columns.Add("PHQ-9", typeof(String));
            dtOutput.Columns.Add("IPAQ-0是否知道", typeof(String));

            
            foreach (DataRow dRow in dtOutput.Rows)
            {
                try
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
                            dRow["床上负荷"] = 0;
                        }
                        else if (p > 0 && p < 10)
                        {
                            dRow["床上负荷"] = 1;
                        }
                        else if (p >= 10)
                        {
                            dRow["床上负荷"] = 0;
                        }

                        p = 0;
                    }

                    if (i == 5)
                    {
                        if (p == 0)
                        {
                            dRow["室内负荷"] = 0;
                        }
                        else if (p > 0 && p < 10)
                        {
                            dRow["室内负荷"] = 1;
                        }
                        else if (p >= 10)
                        {
                            dRow["室内负荷"] = 0;
                        }

                        p = 0;
                    }

                    if (i == 8)
                    {
                        if (p == 0)
                        {
                            dRow["室外负荷"] = 0;
                        }
                        else if (p > 0 && p < 10)
                        {
                            dRow["室外负荷"] = 1;
                        }
                        else if (p >= 10)
                        {
                            dRow["室外负荷"] = 0;
                        }

                        p = 0;
                    }

                }

                if (p == 0)
                {
                    dRow["院外负荷"] = 0;
                }
                else if (p > 0 && p < 10)
                {
                    dRow["院外负荷"] = 1;
                }
                else if (p >= 10)
                {
                    dRow["院外负荷"] = 0;
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
                foreach (string strGAD in strGADs)
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

                switch (dRow["OHQ1"].ToString())
                {
                    case "0":
                        dRow["OHQ1"] = "3次以上";
                        break;
                    case "1":
                        dRow["OHQ1"] = "2次";
                        break;
                    case "2":
                        dRow["OHQ1"] = "1次";
                        break;
                    case "3":
                        dRow["OHQ1"] = "0次";
                        break;
                    default:
                        dRow["OHQ1"] = "";
                        break;
                }
                switch (dRow["OHQ2"].ToString())
                {
                    case "0":
                        dRow["OHQ2"] = "每天";
                        break;
                    case "1":
                        dRow["OHQ2"] = "有时使用";
                        break;
                    case "2":
                        dRow["OHQ2"] = "不使用";
                        break;
                    default:
                        dRow["OHQ2"] = "";
                        break;
                }
                switch (dRow["OHQ3"].ToString())
                {
                    case "0":
                        dRow["OHQ3"] = "1个";
                        break;
                    case "1":
                        dRow["OHQ3"] = "2个";
                        break;
                    case "2":
                        dRow["OHQ3"] = "3-5个";
                        break;
                    case "3":
                        dRow["OHQ3"] = "6-9个";
                        break;
                    case "4":
                        dRow["OHQ3"] = "10个及以上";
                        break;
                    case "5":
                        dRow["OHQ3"] = "0个（没掉）";
                        break;
                    default:
                        dRow["OHQ3"] = "";
                        break;
                }
                if (dRow["OHQ4"].ToString().Length > 0)
                {
                    string[] OHQ4 = (dRow["OHQ4"].ToString()).Split('-');
                    switch (OHQ4[0])
                    {
                        case "A":
                            {
                                if (OHQ4[1].Trim().Length > 0)
                                    dRow["OHQ4"] = "经常(每周" + OHQ4[1] + "次";
                                else
                                    dRow["OHQ4"] = "经常";
                            }
                            break;
                        case "B":
                            {
                                if (OHQ4[1].Trim().Length > 0)
                                    dRow["OHQ4"] = "偶尔(每周" + OHQ4[1] + "次";
                                else
                                    dRow["OHQ4"] = "偶尔";
                            }
                            break;
                        case "C":
                            dRow["OHQ4"] = "无(不出血)";
                            break;
                        default:
                            dRow["OHQ4"] = "";
                            break;
                    }
                }
                switch (dRow["OHQ5"].ToString())
                {
                    case "0":
                        dRow["OHQ5"] = "从来没有";
                        break;
                    case "1":
                        dRow["OHQ5"] = "现在有";
                        break;
                    case "2":
                        dRow["OHQ5"] = "现在没有，但有时复发";
                        break;
                    case "3":
                        dRow["OHQ5"] = "以前有，现在好了";
                        break;
                    default:
                        dRow["OHQ5"] = "";
                        break;
                }

                if (dRow["OHQ6"].ToString().Trim().Length > 0)
                {
                    switch (dRow["OHQ6"].ToString())
                    {
                        case "0":
                            dRow["OHQ6"] = "否";
                            break;
                        case "99":
                            dRow["OHQ6"] = "全部替换";
                            break;
                        default:
                            dRow["OHQ6"] = "部分是（共" + dRow["OHQ6"].ToString() + "颗假牙";
                            break;
                    }
                }
                else
                    dRow["OHQ6"] = "";

                switch (dRow["OHQ7"].ToString())
                {
                    case "0":
                        dRow["OHQ7"] = "清水浸泡";
                        break;
                    case "1":
                        dRow["OHQ7"] = "假牙清洁剂浸泡";
                        break;
                    case "2":
                        dRow["OHQ7"] = "用牙膏刷";
                        break;
                    default:
                        dRow["OHQ7"] = "";
                        break;
                }

                switch (dRow["OHQ8"].ToString())
                {
                    case "0":
                        dRow["OHQ8"] = "<1分钟";
                        break;
                    case "1":
                        dRow["OHQ8"] = "1-2分钟";
                        break;
                    case "2":
                        dRow["OHQ8"] = "2分钟以上";
                        break;
                    default:
                        dRow["OHQ8"] = "";
                        break;
                }
                if (dRow["OHQ9"].ToString().Length > 0)
                {
                    string[] OHQ9 = (dRow["OHQ9"].ToString()).Split('-');
                    switch (OHQ9[0])
                    {
                        case "A":
                            dRow["OHQ9"] = "否";
                            break;
                        case "B":
                            dRow["OHQ9"] = "是（选择原因:";
                            if (OHQ9[1].Trim().Length > 0)
                                dRow["OHQ9"] += "常规检查" + OHQ9[1].ToString() + "次/年;";
                            if (OHQ9[2].Trim().Length > 0)
                                dRow["OHQ9"] += "生病" + OHQ9[2].ToString() + "次/年;";
                            dRow["OHQ9"] += ")";
                            break;
                        default:
                            dRow["OHQ9"] = "";
                            break;
                    }
                }
                else
                    dRow["OHQ9"] = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ExerciseConverter" + ex.Message+ex.ToString()+"\n" +dRow["OHQ6"].ToString());
                }

            }

            string[] colDels = { "Killip", "EF", "LV", "BasicOther" , "BasicRisk", "RiskOther","basicinfo_Number1","basicinfo_Number2","basicinfo_Number3","basicinfo_Number4",
           "PCI", "ResidualStenosis","DominantCoronary","InRoomUp","Date","BloodPressureLower",
            "BloodPressureUpper","HeartRate","GADResult","BloodOxygen","PHQResult","BorgIndex","Remarks","IPAQ0","CollatCirc","basicinfo_Number","Checks","ECGs","Male"};
            foreach (string colDel in colDels)
            {
                dtOutput.Columns.Remove(colDel);
            }


            dtOutput.Columns["IPAQNumber"].ColumnName = "IPAQ调查问卷编号";
            dtOutput.Columns["IPAQ调查问卷编号"].SetOrdinal(20);
            dtOutput.Columns["IPAQ-0是否知道"].SetOrdinal(21);
            dtOutput.Columns["IPAQ1"].ColumnName = "IPAQ-1";
            dtOutput.Columns["IPAQ-1"].SetOrdinal(22);
            dtOutput.Columns["IPAQ2"].ColumnName = "IPAQ-2";
            dtOutput.Columns["IPAQ-2"].SetOrdinal(23);
            dtOutput.Columns["IPAQ3"].ColumnName = "IPAQ-3";
            dtOutput.Columns["IPAQ-3"].SetOrdinal(24);
            dtOutput.Columns["IPAQ4"].ColumnName = "IPAQ-4";
            dtOutput.Columns["IPAQ-4"].SetOrdinal(25);
            dtOutput.Columns["IPAQ5"].ColumnName = "IPAQ-5";
            dtOutput.Columns["IPAQ-5"].SetOrdinal(26);
            dtOutput.Columns["GADNumber"].ColumnName = "GAD评估量表编号";
            dtOutput.Columns["GAD评估量表编号"].SetOrdinal(27);
            dtOutput.Columns["GAD-7"].SetOrdinal(28);
            dtOutput.Columns["PHQNumber"].ColumnName = "PHQ评估量表编号";
            dtOutput.Columns["PHQ评估量表编号"].SetOrdinal(29);
            dtOutput.Columns["PHQ-9"].SetOrdinal(30);

            dtOutput.Columns["OHQNumber"].ColumnName = "口腔卫生调查编号";
            dtOutput.Columns["口腔卫生调查编号"].SetOrdinal(4);
            dtOutput.Columns["OHQ1"].ColumnName = "O-1";
            dtOutput.Columns["OHQ2"].ColumnName = "O-2";
            dtOutput.Columns["OHQ3"].ColumnName = "O-3";
            dtOutput.Columns["OHQ4"].ColumnName = "O-4";
            dtOutput.Columns["OHQ5"].ColumnName = "O-5";
            dtOutput.Columns["OHQ6"].ColumnName = "O-6";
            dtOutput.Columns["OHQ7"].ColumnName = "O-7";
            dtOutput.Columns["OHQ8"].ColumnName = "O-8";
            dtOutput.Columns["OHQ9"].ColumnName = "O-9";

            return dtOutput;
        }
        private static DataTable PAQConverter(DataTable dt)
        {
            DataTable dtOutput = dt.Copy();

            dtOutput.TableName = "03-身体活动水平";
            
            //dtOutput.Columns["ExerciseNumber"].ColumnName = "记录编号";

            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns["性别"].SetOrdinal(3);  
                        
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
               

                if(dRow["BalanceTesting1"].ToString().Length>0)
                {
                    string strBTs = null;
                    string[] strBalanceTests1 = (dRow["BalanceTesting1"].ToString()).Split('-');
                    if (strBalanceTests1[0]=="A")
                    {
                        strBTs = "是";
                    }
                    else if (strBalanceTests1[0] == "B")
                    {
                        strBTs = strBalanceTests1[1]+"秒"+ strBalanceTests1[2]+"分秒";
                    }
                    dRow["BalanceTesting1"] = strBTs;
                }

                if (dRow["BalanceTesting2"].ToString().Length > 0)
                {
                    string strBTs = null;
                    string[] strBalanceTests1 = (dRow["BalanceTesting2"].ToString()).Split('-');
                    if (strBalanceTests1[0] == "A")
                    {
                        strBTs = "是";
                    }
                    else if (strBalanceTests1[0] == "B")
                    {
                        strBTs = strBalanceTests1[1] + "秒" + strBalanceTests1[2] + "分秒";
                    }
                    dRow["BalanceTesting2"] = strBTs;
                }

                if (dRow["BalanceTesting3"].ToString().Length > 0)
                {
                    string strBTs = null;
                    string[] strBalanceTests1 = (dRow["BalanceTesting3"].ToString()).Split('-');
                    if (strBalanceTests1[0] == "A")
                    {
                        strBTs = "是";
                    }
                    else if (strBalanceTests1[0] == "B")
                    {
                        strBTs = strBalanceTests1[1] + "秒" + strBalanceTests1[2] + "分秒";
                    }
                    dRow["BalanceTesting3"] = strBTs;
                }

                if (dRow["BalanceTesting3"].ToString().Length > 0)
                {
                    string strBTs = null;
                    string[] strBalanceTests1 = (dRow["BalanceTesting3"].ToString()).Split('-');
                    if (strBalanceTests1[0] == "A")
                    {
                        strBTs = "是";
                    }
                    else if (strBalanceTests1[0] == "B")
                    {
                        strBTs = strBalanceTests1[1] + "秒" + strBalanceTests1[2] + "分秒";
                    }
                    dRow["BalanceTesting3"] = strBTs;
                }

                if (dRow["walkingTesting1"].ToString().Length > 0 && dRow["walkingTesting1"].ToString().Length > 0)
                {
                    string strWTs = null;
                    string[] strWTests1 = (dRow["walkingTesting1"].ToString()).Split('-');
                    string[] strWTests2 = (dRow["walkingTesting2"].ToString()).Split('-');

                    if (strWTests1[0] == "A")
                    {
                        strWTs = "有辅助工具";
                    }
                    else if (strWTests1[0] == "B")
                    {
                        strWTs += (Convert.ToInt32(strWTests1[1]) + Convert.ToInt32(strWTests2[1])) / 2 + "秒" + (Convert.ToInt32(strWTests1[2]) + Convert.ToInt32(strWTests2[2])) / 2 + "分秒";
                    }
                    dRow["walkingTesting1"] = strWTs;
                }

                if (dRow["SitUpTesting"].ToString().Length > 0)
                {
                    string strSTs = null;
                    string[] strSitUpTests = (dRow["SitUpTesting"].ToString()).Split('-');
                    if (strSitUpTests[0] == "A")
                    {
                        strSTs = strSitUpTests[1] + "秒" + strSitUpTests[2] + "分秒"; ;
                    }
                    else if (strSitUpTests[0] == "B")
                    {
                        strSTs = "仅完成"+ strSitUpTests[0].ToString()+"次";
                    }
                    dRow["SitUpTesting"] = strSTs;
                }

                if (dRow["Hight"].ToString().Length > 0)
                {
                    if (Convert.ToDouble( dRow["Hight"])<1.0)                   
                    dRow["Hight"] = "";
                }

                if (dRow["Hipline"].ToString().Length > 0)
                {
                    if (Convert.ToDouble(dRow["Hipline"]) < 1.0)
                        dRow["Hipline"] = "";
                }
                if (dRow["GripStrengthLeft1"].ToString().Length > 0&& dRow["GripStrengthLeft2"].ToString().Length > 0 )
                {
                    if (Convert.ToDouble(dRow["GripStrengthLeft1"].ToString()) > 0.1 && Convert.ToDouble(dRow["GripStrengthLeft2"].ToString()) > 0.1)
                        dRow["GripStrengthLeft1"] = (Convert.ToDouble(dRow["GripStrengthLeft1"].ToString()) + Convert.ToDouble(dRow["GripStrengthLeft2"].ToString())) / 2.0;
                    else
                        dRow["GripStrengthLeft1"] = "";
                }

                if (dRow["GripStrengthRight1"].ToString().Length > 0 && dRow["GripStrengthRight2"].ToString().Length > 0)
                {
                    if (Convert.ToDouble(dRow["GripStrengthRight1"].ToString()) > 0.1 && Convert.ToDouble(dRow["GripStrengthRight2"].ToString()) > 0.1)
                        dRow["GripStrengthRight1"] = (Convert.ToDouble(dRow["GripStrengthRight1"].ToString()) + Convert.ToDouble(dRow["GripStrengthRight2"].ToString())) / 2.0;
                    else
                        dRow["GripStrengthRight1"] = "";
                }
                
                
            }

            

            string[] colDels = { "Killip", "EF", "LV", "BasicOther" , "BasicRisk", "RiskOther",
           "PCI", "ResidualStenosis","DominantCoronary","CollatCirc","basicinfo_Number","Male",
            "walkingTesting2","TUG","FRTLeft1","FRTLeft2","FRTRight1","FRTRight2","SFO1","SFO2","OneFootLeft1",
            "OneFootLeft2","OneFootRight1","OneFootRight2","BloodPressureUpper","BloodPressureLower","HeartRate","Temperature","Breathe","LeftHandHurt","RightHandHurt",
                       "LeftLapHurt","RightLapHurt","LapStrengthLeft1","LapStrengthRight1","LapStrengthLeft2","LapStrengthRight2","GripStrengthLeft2","GripStrengthRight2"};
            foreach (string colDel in colDels)
            {
                dtOutput.Columns.Remove(colDel);
            }

            
            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            dtOutput.Columns["SPPBNumber"].ColumnName = "平衡测试编号";
            dtOutput.Columns["平衡测试编号"].SetOrdinal(4);            
            dtOutput.Columns["BalanceTesting1"].ColumnName = "平衡测试-1";
            dtOutput.Columns["平衡测试-1"].SetOrdinal(5);
            dtOutput.Columns["BalanceTesting2"].ColumnName = "平衡测试-2";
            dtOutput.Columns["平衡测试-2"].SetOrdinal(6);
            dtOutput.Columns["BalanceTesting3"].ColumnName = "平衡测试-3";
            dtOutput.Columns["平衡测试-3"].SetOrdinal(7);
            dtOutput.Columns["walkingTesting1"].ColumnName = "4米行走";
            dtOutput.Columns["4米行走"].SetOrdinal(8);
            dtOutput.Columns["SitUpTesting"].ColumnName = "坐起立";
            dtOutput.Columns["坐起立"].SetOrdinal(9);
            dtOutput.Columns["Hight"].ColumnName = "身高";
            dtOutput.Columns["身高"].SetOrdinal(10);
            dtOutput.Columns["Weight"].ColumnName = "体重";
            dtOutput.Columns["体重"].SetOrdinal(11);
            dtOutput.Columns["Waistline"].ColumnName = "腰围";
            dtOutput.Columns["腰围"].SetOrdinal(12);
            dtOutput.Columns["Hipline"].ColumnName = "臀围";
            dtOutput.Columns["臀围"].SetOrdinal(13);
            dtOutput.Columns["ArmlineLeft"].ColumnName = "上臂围度(左)";
            dtOutput.Columns["上臂围度(左)"].SetOrdinal(14);
            dtOutput.Columns["ArmlineRight"].ColumnName = "上臂围度(右)";
            dtOutput.Columns["上臂围度(右)"].SetOrdinal(15);
            dtOutput.Columns["LeglineLeft"].ColumnName = "大腿围度(左)";
            dtOutput.Columns["大腿围度(左)"].SetOrdinal(16);
            dtOutput.Columns["LeglineRight"].ColumnName = "大腿围度(右)";
            dtOutput.Columns["大腿围度(右)"].SetOrdinal(17);
            dtOutput.Columns["GripStrengthLeft1"].ColumnName = "左手握力";
            dtOutput.Columns["左手握力"].SetOrdinal(18);
            dtOutput.Columns["GripStrengthRight1"].ColumnName = "右手握力";
            dtOutput.Columns["右手握力"].SetOrdinal(19);
          




            return dtOutput;
        }


        private static DataTable PhysiqueConverter(DataTable dt)
        {
            DataTable dtOutput = dt.Copy();
            dtOutput.TableName = "04-口腔卫生和体质";

            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns.Add("BMI", typeof(Double));
            dtOutput.Columns.Add("脂肪重量指数", typeof(Double));
            dtOutput.Columns.Add("BCW-TBW", typeof(Double));

            
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

              
                if (dRow["Weight"].ToString().Trim().Length > 0 && dRow["Hight"].ToString().Trim().Length > 0)
                    dRow["BMI"] = Math.Round((double)dRow["Weight"] / ((double)dRow["Hight"] * (double)dRow["Hight"]), 2);
                else
                    dRow["BMI"] = DBNull.Value;
                if (dRow["FM"].ToString().Trim().Length > 0 && dRow["Hight"].ToString().Trim().Length > 0)
                    dRow["脂肪重量指数"] = Math.Round((double)dRow["FM"] / ((double)dRow["Hight"] * (double)dRow["Hight"]), 2);
                else
                    dRow["脂肪重量指数"] = DBNull.Value;
                if (dRow["BCW"].ToString().Trim().Length > 0 && dRow["TBW"].ToString().Trim().Length > 0)
                    dRow["BCW-TBW"] = Math.Round((double)dRow["BCW"] / (double)dRow["TBW"], 2);
                else
                    dRow["BCW-TBW"] = DBNull.Value;
                
            }

            string[] colDels = { "Killip", "EF", "LV", "BasicOther" , "BasicRisk", "RiskOther",
           "PCI", "ResidualStenosis","DominantCoronary","CollatCirc","basicinfo_Number","Male"};
            foreach (string colDel in colDels)
            {
                dtOutput.Columns.Remove(colDel);
            }

            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            dtOutput.Columns["性别"].SetOrdinal(3);
          
            dtOutput.Columns["PhysiqueNumber"].ColumnName = "体质成分检测编号";
            dtOutput.Columns["Weight"].ColumnName = "体重";
            dtOutput.Columns["Hight"].ColumnName = "身高";
            dtOutput.Columns["BMI"].SetOrdinal(17);            
            dtOutput.Columns["FM"].ColumnName = "脂肪重量";
            dtOutput.Columns["脂肪重量指数"].SetOrdinal(19);
            dtOutput.Columns["TBW"].ColumnName = "体液总量(TBW)";
            dtOutput.Columns["BCW"].ColumnName = "细胞外液(BCW)";
            dtOutput.Columns["BCW-TBW"].SetOrdinal(22);
            dtOutput.Columns["SMMAll"].ColumnName = "骨骼肌肉质量(SMM)";
            dtOutput.Columns["SMMArmLeft"].ColumnName = "左臂";
            dtOutput.Columns["SMMArmRight"].ColumnName = "右臂";
            dtOutput.Columns["SMMBody"].ColumnName = "躯干";
            dtOutput.Columns["SMMLegLeft"].ColumnName = "左腿";
            dtOutput.Columns["SMMLegRight"].ColumnName = "右腿";
            dtOutput.Columns["VAT"].ColumnName = "内脏脂肪(VAT)";
            dtOutput.Columns["Waistline"].ColumnName = "腰围(WC)";
            dtOutput.Columns["PA"].ColumnName = "相角";
            dtOutput.Columns["PAPercent"].ColumnName = "百分位";

            return dtOutput;
        }       
    }

}

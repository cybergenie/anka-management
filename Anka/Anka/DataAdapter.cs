using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Anka
{
    static class DataAdapter
    {
        
        /// <summary>
        /// 基本信息
        /// </summary>
        public static string Name { set; get; }
        public static string Number { set; get; }
        public static int Age { set; get; }
        public static bool Male { set; get; } //true:男   false:女;

        public static bool loadNewPerson { set; get; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public struct BasicInfoData
        {
            public string Killip;
            public string EF;
            public string LV;
            public string BasicOther;
            public bool[] BasicRisk;
            public string RiskOther;
            public int PCI;
            public int ResidualStenosis;
            public bool CollatCirc;
            public int DominantCoronary;//-1:"左优势型" 0:"均衡型" 1:"右优势型"
        }
        public static BasicInfoData BasicInfoResult = new BasicInfoData();

        /// <summary>
        /// 运动负荷记录
        /// </summary>
        public static string ExerciseNumber { set; get; }
        public struct ExerciseData
        {
            public bool InRoomUp;//true:10  ;false:5;
            public  string[] Date;
            public  int[] BloodPressureLower;
            public  int[] BloodPressureUpper;
            public  int[] HeartRate;
            public  int[] BloodOxygen;
            public  int[] BorgIndex;
            public  string[] Remarks;
            public  string[] ECGs;
            public  bool[] Checks;
        }
        public static ExerciseData ExerciseResult = new ExerciseData();


        /// <summary>
        /// 心理精神状态评估表
        /// </summary>
        /// 
        public static string PHQNumber { set; get; }
        public static string GADNumber { set; get; }
        public static int[] PHQResult { set; get; }        
        public static int[] GADResult { set; get; }

        /// <summary>
        /// 国际标准化身体活动调查问卷(IPAQ)
        /// </summary>
        /// 

        public static string IPAQNumber { set; get; }
        public struct IPAQData
        {
            public bool IPAQ0;
            public int IPAQ1;
            public int IPAQ2;
            public int IPAQ3;
            public int IPAQ4;
            public int IPAQ5;            
        }        
       public static IPAQData  IPAQResult = new IPAQData();

        /// <summary>
        /// 口腔卫生调查表(OHQ)
        /// </summary>
        /// 

        public static string OHQNumber { set; get; }
        public struct OHQData
        {            
            public string OHQ1;
            public string OHQ2;
            public string OHQ3;
            public string OHQ4;//A-d：经常（每周d次）；B-dd：偶尔（每周d次）；C-0：无（不出血）
            public string OHQ5;
            public string OHQ6;//0：否；d：部分是（共d颗假牙）；99：全部替换
            public string OHQ7;
            public string OHQ8;
            public string OHQ9;//A-0-0：否）；B-d-d：常规检查 d次/年；生病d次/年
        }
        public static OHQData OHQResult = new OHQData();

        public static string SPPBNumber { set; get; }
        public struct SPPBData
        {
            public string BalanceTesting1;//A-0-0:是；B-d-d：否 d秒d分秒
            public string BalanceTesting2;//A-0-0:是；B-d-d：否 d秒d分秒
            public string BalanceTesting3;//A-0-0:是；B-d-d：否 d秒d分秒
            public string walkingTesting1;//A-d-d：有使用辅助工具d秒d分秒;B-d-d:无辅助工具d秒d分秒
            public string walkingTesting2;//A-d-d：有使用辅助工具d秒d分秒;B-d-d:无辅助工具d秒d分秒
            public string SitUpTesting;//A-d-d：d秒d分秒 B-0-d：可完成d次
        }
        public static SPPBData SPPBResult = new SPPBData();

        public struct BalanceCapability
        {
            public string TUG;
            public string FRTLeft1;
            public string FRTLeft2;
            public string FRTRight1;
            public string FRTRight2;
            public string SFO1;//座位双脚开闭1
            public string SFO2;//座位双脚开闭2
            public string OneFootLeft1;
            public string OneFootLeft2;
            public string OneFootRight1;
            public string OneFootRight2;
        }
        public static BalanceCapability BalanceCapabilityResult = new BalanceCapability();

        public struct Size
        {
            public double Hight;
            public double Weight;
            public double Waistline;
            public double Hipline;
            public double ArmlineLeft;
            public double ArmlineRight;
            public double LeglineLeft;
            public double LeglineRight;

        }
        public static Size SizeResult = new Size();

        public struct Vitals
        {
            public int BloodPressureUpper;
            public int BloodPressureLower;
            public int HeartRate;
            public double Temperature;
            public int Breathe;

        }
        public static Vitals VitalsResult = new Vitals();

        public struct GripStrength
        {
            public bool LeftHandHurt;
            public bool RightHandHurt;
            public double GripStrengthLeft1;
            public double GripStrengthRight1;
            public double GripStrengthLeft2;
            public double GripStrengthRight2;
        }
        public static GripStrength GripStrengthResult = new GripStrength();

        public struct LapStrength
        {
            public bool LeftLapHurt;
            public bool RightLapHurt;
            public double LapStrengthLeft1;
            public double LapStrengthRight1;
            public double LapStrengthLeft2;
            public double LapStrengthRight2;
        }
        public static LapStrength LapStrengthResult = new LapStrength();

        public static string PhysiqueNumber { set; get; }
        public struct Physique
        {
            public double FM;
            public double TBW;
            public double BCW;
            public double SMMAll;
            public double SMMArmLeft;
            public double SMMArmRight;
            public double SMMBody;
            public double SMMLegLeft;
            public double SMMLegRight;
            public double VAT;
            public double PA;
            public double PAPercent;
        }
        public static Physique PhysiqueResult = new Physique();

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

        public static void GetBloodPressure(string strBloodPressure, int BPLower,int BPUpper)
        {
            int[] BMPtemp = { 0, 0 };
            int i = 0;
            char[] Spliter = { '/', '\\', ' ' };
            foreach (string ctemp in strBloodPressure.Split(Spliter))
            {
                if (DataAdapter.IsNumber(ctemp) == true)
                {
                    if (Convert.ToInt32(ctemp)>0)
                    BMPtemp[i] = Convert.ToInt32(ctemp);
                }

                i++;
            }

            BPLower = BMPtemp[0];
            BPUpper = BMPtemp[1];
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
                temp += (s.ToString() + "|");
            }

            return temp;
        }

        public static string ArrayToString(bool[] data)
        {
            string temp = "";

            foreach (var s in data)
            {
                int p = s == true ? 1 : 0;
                temp += (p.ToString() + "|");
            }

            return temp;
        }


    }

}

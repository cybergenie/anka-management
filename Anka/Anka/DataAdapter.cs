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
        public static string Male { set; get; }

        ///
        public static string Killip { set; get; }
        public static string EF { set; get; }
        public static string LV { set; get; }
        public static string BasicOther { set; get; }
        public static int BasicRisk { set; get; } = 0b10000000000000;
        public static string RiskOther { get; set; }
        public static int PCI { get; set; }
        public static int ResidualStenosis { get; set; }
        public static bool CollatCirc { get; set; }
        public static string DominantCoronary { get; set; }

        /// <summary>
        /// 运动负荷记录
        /// </summary>
        public static string ExerciseNumber { set; get; }
        public static string[] Date { get; set; }     

   
        public static int[] BloodPressureLower { get; set; }

        public static int[] BloodPressureUpper { get; set; }


        public static int[] HeartRate { get; set; }       

        
        public static int[] BloodOxygen { get; set; }
       

        public static int[] BorgIndex { get; set; }        

        public static string[] Remarks { get; set; }        

        public static string[] ECGs { get; set; }       

        public static bool[] Checks { get; set; }


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

        public struct SPPBData
        {
            public string BalanceTesting1 { get; set; }//A-0-0:是；B-d-d：否 d秒d分秒
            public string BalanceTesting2 { get; set; }//A-0-0:是；B-d-d：否 d秒d分秒
            public string BalanceTesting3 { get; set; }//A-0-0:是；B-d-d：否 d秒d分秒
            public string walkingTesting1 { get; set; }//d-d：d秒d分秒
            public string walkingTesting2 { get; set; }//d-d：d秒d分秒
            public string SitUpTesting { get; set; }//A-d-d：d秒d分秒 B-0-d：可完成d次
        }
        public static SPPBData SPPBResult = new SPPBData();

        public struct BalanceCapability
        {
            public string TUG { get; set; }
            public string FRTLeft1 { get; set; }
            public string FRTLeft2 { get; set; }
            public string FRTRight1 { get; set; }
            public string FRTRight2 { get; set; }
            public string SFO1 { get; set; }
            public string SFO2 { get; set; }
            public string OneFootLeft1 { set; get; }
            public string OneFootLeft2 { set; get; }
            public string OneFootRight1 { set; get; }
            public string OneFootRight2 { set; get; }
        }
        public static BalanceCapability BalanceCapabilityResult = new BalanceCapability();

        public struct Size
        {
            public double Hight { get; set; }
            public double Weight { get; set; }
            public double Waistline { get; set; }
            public double Hipline { get; set; }
            public double ArmlineLeft { get; set; }
            public double ArmlineRight { get; set; }
            public double LeglineLeft { get; set; }
            public double LeglineRight { get; set; }

        }

        public struct Vitals
        {
            public int BloodPressureUpper { get; set; }
            public int BloodPressureLower { get; set; }
            public int HeartRate { get; set; }
            public double temperature { get; set; }
            public int breathe { get; set; }

        }

        public struct GripStrength
        {
            public bool LeftHandHurt { get; set; }
            public bool RightHandHurt { get; set; }
            public double GripStrengthLeft1 { get; set; }
            public double GripStrengthRight1 { get; set; }
            public double GripStrengthLeft2 { get; set; }
            public double GripStrengthRight2 { get; set; }
        }

        public struct LapStrength
        {
            public bool LeftLapHurt { get; set; }
            public bool RightLapHurt { get; set; }
            public double LapStrengthLeft1 { get; set; }
            public double LapStrengthRight1 { get; set; }
            public double LapStrengthLeft2 { get; set; }
            public double LapStrengthRight2 { get; set; }
        }

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
            
    }
    
}

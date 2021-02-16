using Anka2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace Anka2.Services
{
    public class DataUitls 
    {
        public static NewPersonResult AddNewPerson(ref BasicInfo newPerson)
        {
            string personId = newPerson.Number; 
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingPerson = context.DbPerson
                        .Where(BasicInfo => BasicInfo.Number == personId)
                        .Include(BasicInfo => BasicInfo.PExercise)
                        .ToList();

                    if (existingPerson.Count > 0)
                    {
                        newPerson = existingPerson[0];
                        context.SaveChanges();
                        return NewPersonResult.Success_Load;
                    }
                    else
                    {
                        context.Add(newPerson);
                        context.SaveChanges();
                        return NewPersonResult.Success_New;
                    }
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message);
                return NewPersonResult.Error;
            }            
        }


        public static bool CheckNewPerson(ref BasicInfo newPerson)
        {
            string personId = newPerson.Number;
            try
            {
                using var context = new DbAdapter();
                var existingPerson = context.DbPerson.Find(personId);

                if (existingPerson != null)
                {
                    var existingPersonList = context.DbPerson.
                        Include(BasicInfo => BasicInfo.PExercise).
                        Include(BasicInfo => BasicInfo.PGAD).
                        Where<BasicInfo>(p => p.Number == personId).
                        ToList();

                    newPerson = existingPersonList[0];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("档案号查询错误，错误信息为：" + e.Message);
                return false;
            }
        }

        public static bool SavePersonInfo(BasicInfo Person)
        {
            string personId = Person.Number;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingPerson = context.DbPerson.Find(personId);
                    if (existingPerson!=null)
                    {
                        context.Entry(existingPerson).CurrentValues.SetValues(Person);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("档案号不存在。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);                       
                        return false;
                    }                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Information);                
                return false;
            }
            return true;

        }


        public static Window GetParentWindow(DependencyObject Source)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(Source);
            while (parent != null)
            {
                if (parent is Window)
                {
                    break;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }

            return (Window)parent;
        }

        public static bool BasicRisk2Checked(string BasicRisk, int SN)
        {
            if (!String.IsNullOrEmpty(BasicRisk))
            {
                Char[] CharBasicRisks = BasicRisk.ToCharArray();
                return CharBasicRisks[SN] == '1';
            }
            else
                return false;
        }

        public static string Checked2BasicRisk(string BasicRisk, int SN, bool IsChecked)
        {
            if (String.IsNullOrEmpty(BasicRisk)) 
                BasicRisk = "000000000000";

            Char[] CharBasicRisks = BasicRisk.ToCharArray();
            CharBasicRisks[SN] = IsChecked ? '1' : '0';
            BasicRisk = new string(CharBasicRisks);
            return BasicRisk;
        }
       

        public static bool IsPersonId(string personId)
        {           
            Regex personIdReg = new Regex("^(\\d{8})$");
            return personIdReg.IsMatch(personId.ToString());          

        }

        private static void GenderConvertor(DataRow dr, string columnName)
        {
            var gender = dr[columnName] switch
            {
                "True" => "男",
                "False" => "女",
                _ => string.Empty,
            };
            dr[columnName] = gender;
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

        private static void DateConvertor(DataRow dr, string columnName)
        {
            string ExerciseNumber = dr[columnName] as string;
            string result = System.Text.RegularExpressions.Regex.Replace(ExerciseNumber, @"[^0-9]+", "/");
            dr[columnName] = result;
        }

        private static void BedUpConvertor(DataRow dr, string columnName)
        {
            string BedUp = dr[columnName] as string;
            string result = "不合格";

            string[] BedUps = BedUp.Split('|');
            for (int i = 0; i < 4; i++)
            {
                if (BedUps[i] == "1")
                    result = "合格";
            }
            dr[columnName] = result;
        }

        private static void InRoomConvertor(DataRow dr, string columnName)
        {
            string BedUp = dr[columnName] as string;
            string result = "不合格";

            string[] BedUps = BedUp.Split('|');
            for (int i = 4; i < 6; i++)
            {
                if (BedUps[i] == "1")
                    result = "合格";
            }
            dr[columnName] = result;
        }

        private static void OutRoomConvertor(DataRow dr, string columnName)
        {
            string BedUp = dr[columnName] as string;
            string result = "不合格";

            string[] BedUps = BedUp.Split('|');
            for (int i = 6; i < 8; i++)
            {
                if (BedUps[i] == "1")
                    result = "合格";
            }
            dr[columnName] = result;
        }

        private static void OutSideConvertor(DataRow dr, string columnName)
        {
            string BedUp = dr[columnName] as string;
            string result = "不合格";

            string[] BedUps = BedUp.Split('|');
            if (BedUps[9] == "1")
                result = "合格";

            dr[columnName] = result;
        }


        public static void BasicInfoValueConvertor(ref DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                GenderConvertor( dr, "性别");
                CollatCircConvertor( dr, "侧枝循环");
                DominantCoronaryConvertor( dr, "优势冠脉");
                BasicRiskConvertor( dr, "危险因素");
            }
        }
        public static void ExerciseValueConvertor(ref DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                GenderConvertor( dr, "性别");
                DateConvertor( dr, "记录编号");
                BedUpConvertor( dr, "床上负荷");
                InRoomConvertor(dr, "室内负荷");
                OutRoomConvertor(dr, "室外负荷");
                OutSideConvertor(dr, "院外负荷");
            }
        }

    }

    public enum SheetItems
    {
        BasicInfo,
        Exercise,
        PHQ,
        GAD,
        IPAQ,
        OHQ,
        SPPB,
        Physique
    };
    public enum InfoType
    {
        Success,
        Warning,
        Error
    }

    public enum NewPersonResult
    {
        Error,
        Success_New,
        Success_Load
    }
}

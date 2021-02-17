using Anka2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
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

        public static bool RepairData()
        {
            bool RepairResult = false;
            RepairResult = RepairExercise();
            RepairResult = RepairGAD();
            return RepairResult;

        }

        private static string NumberConverter(string Number,string basicinfoNumber)
        {
            string newNumber = null;
            string tempNumber = Number.Trim();
            string tempBaiscNumber = string.IsNullOrEmpty(basicinfoNumber)? null : basicinfoNumber.Trim();
            tempNumber = tempNumber.Remove(0, tempBaiscNumber == null ? 1 : tempBaiscNumber.Length + 1);

            newNumber += tempBaiscNumber;

            tempNumber = Regex.Replace(tempNumber, @"[^0-9]+", "/");

            List<string> strTempNumber = tempNumber.Split('/').ToList();
            Regex YearReg = new Regex("^([1-2][0-9][0-9][0-9])$");
            Regex MonthReg = new Regex("^(\\d{1,2})$");
            Regex DayReg = new Regex("^(\\d{1,2})$");
            if (strTempNumber[0].Length > 4)
            {
                var temp = strTempNumber[0];
                strTempNumber[0] = temp.Substring(0, 4);
                strTempNumber.Insert(1, temp.Remove(0, 4));
            }
            if (YearReg.IsMatch(strTempNumber[0]))
            {
                newNumber += "-";
                newNumber += strTempNumber[0];
            }
            if (strTempNumber.Count > 1)
            {
                if (MonthReg.IsMatch(strTempNumber[1]))
                {
                    newNumber += "/";
                    newNumber += strTempNumber[1];
                }
            }
            else
            {
                newNumber += "/";
                newNumber += "1";
            }
            if (strTempNumber.Count > 2)
            {
                if (strTempNumber[2].Length > 4)
                {
                    strTempNumber[2] = strTempNumber[2].Remove(strTempNumber[2].Length - 5, strTempNumber[2].Length - 1);
                }
                if (DayReg.IsMatch(strTempNumber[2]))
                {
                    newNumber += "/";
                    newNumber += strTempNumber[2];
                }
            }
            else
            {
                newNumber += "/";
                newNumber += "1";
            }

            return newNumber;

        }

        private static T ItemCopy<T>(T item, string Number)
        {
            PropertyDescriptorCollection properties =
              TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            DataRow row = dt.NewRow();
            foreach (PropertyDescriptor prop in properties)
            {
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            }
            dt.Rows.Add(row);
            dt.Rows[0][0] = Number;
            dt.Rows[0][dt.Columns.Count-2] = dt.Rows[0][dt.Columns.Count - 2].ToString().Trim();
            T s = Activator.CreateInstance<T>();
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                if (info != null)
                {
                    try
                    {
                        if (!Convert.IsDBNull(dt.Rows[0][i]))
                        {
                            object v = null;
                            if (info.PropertyType.ToString().Contains("System.Nullable"))
                            {
                                v = Convert.ChangeType(dt.Rows[0][i], Nullable.GetUnderlyingType(info.PropertyType));
                            }
                            else
                            {
                                v = Convert.ChangeType(dt.Rows[0][i], info.PropertyType);
                            }
                            info.SetValue(s, v, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                    }
                }
            }


                return s;
        }

        private static bool RepairExercise()
        {
            try
            {
                var context = new DbAdapter();
                var RepairList = context.DbExercise.ToList();
                string tempNumber = null;
                try
                {
                    for (int i = 0; i < RepairList.Count; i++)
                    {
                        tempNumber = RepairList[i].ExerciseNumber.Trim();
                        var tempList = RepairList[i];
                        string tempBaiscNumber = RepairList[i].basicinfoNumber;
                        context.DbExercise.Remove(tempList);

                        RepairList.RemoveAt(i);
                        var newNumber = NumberConverter(tempNumber, tempBaiscNumber);
                        RepairList.Insert(i, ItemCopy(tempList, newNumber));

                    }

                    for (int i = 0; i < RepairList.Count; i++)
                    {
                        Regex IndexReg = new Regex("^(-\\d{2})$");
                        if (!IndexReg.IsMatch(RepairList[i].ExerciseNumber.Substring(RepairList[i].ExerciseNumber.Length - 3, 3)))
                        {
                            string tempListNumber = RepairList[i].ExerciseNumber;
                            var exsitingList = RepairList.FindAll((Exercise e) => e.ExerciseNumber == tempListNumber);
                            RepairList.RemoveAll((Exercise e) => e.ExerciseNumber == tempListNumber);
                            for (int j = 0; j < exsitingList.Count; j++)
                            {
                                if (!string.IsNullOrWhiteSpace(exsitingList[j].basicinfoNumber))
                                {
                                    exsitingList[j].ExerciseNumber = exsitingList[j].ExerciseNumber + "-" + (j + 1).ToString("D2");
                                    tempNumber = exsitingList[j].ExerciseNumber;
                                    context.DbExercise.Add(exsitingList[j]);
                                    RepairList.Add(exsitingList[j]);
                                }
                            }
                        }
                    }



                }
                catch (Exception e)
                {
                    MessageBox.Show("数据表Exercise转换错误,错误编号为" + tempNumber + "\n" + e.Message);
                    return false;
                }
                var s = RepairList;
                context.SaveChanges();
                return true;


            }
            catch (Exception e)
            {
                MessageBox.Show("Exercise数据连接错误，错误信息为：" + e.Message);
                return false;
            }

        }

        private static bool RepairGAD()
        {
            try
            {
                var context = new DbAdapter();
                var RepairList = context.DbGAD.ToList();
                string tempNumber = null;
                try
                {
                    for (int i = 0; i < RepairList.Count; i++)
                    {
                        tempNumber = RepairList[i].GADNumber.Trim();
                        var tempList = RepairList[i];
                        string tempBaiscNumber = RepairList[i].basicinfoNumber;
                        context.DbGAD.Remove(tempList);

                        RepairList.RemoveAt(i);
                        var newNumber = NumberConverter(tempNumber, tempBaiscNumber);
                        RepairList.Insert(i, ItemCopy(tempList, newNumber));

                    }

                    for (int i = 0; i < RepairList.Count; i++)
                    {
                        Regex IndexReg = new Regex("^(-\\d{2})$");
                        if (!IndexReg.IsMatch(RepairList[i].GADNumber.Substring(RepairList[i].GADNumber.Length - 3, 3)))
                        {
                            string tempListNumber = RepairList[i].GADNumber;
                            var exsitingList = RepairList.FindAll((GAD e) => e.GADNumber == tempListNumber);
                            RepairList.RemoveAll((GAD e) => e.GADNumber == tempListNumber);
                            for (int j = 0; j < exsitingList.Count; j++)
                            {
                                if (!string.IsNullOrWhiteSpace(exsitingList[j].basicinfoNumber))
                                {
                                    exsitingList[j].GADNumber = exsitingList[j].GADNumber + "-" + (j + 1).ToString("D2");
                                    tempNumber = exsitingList[j].GADNumber;
                                    context.DbGAD.Add(exsitingList[j]);
                                    RepairList.Add(exsitingList[j]);                                    
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("数据表GAD转换错误,错误编号为" + tempNumber + "\n" + e.Message);
                    return false;
                }               
                context.SaveChanges();
                return true;


            }
            catch (Exception e)
            {
                MessageBox.Show("GAD数据连接错误，错误信息为：" + e.Message);
                return false;
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

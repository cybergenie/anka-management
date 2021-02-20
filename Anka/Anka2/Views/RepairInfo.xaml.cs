﻿using Anka2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Anka2.Views
{
    /// <summary>
    /// RepairInfo.xaml 的交互逻辑
    /// </summary>
    public partial class RepairInfo : Window
    {
        public RepairInfo()
        {
            InitializeComponent();
        }

        BackgroundWorker bgMeet;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            bgMeet = new BackgroundWorker();
            //能否报告进度更新
            bgMeet.WorkerReportsProgress = true;
            //要执行的后台任务
            bgMeet.DoWork += new DoWorkEventHandler(bgMeet_DoWork);
            //进度报告方法
            bgMeet.ProgressChanged += new ProgressChangedEventHandler(bgMeet_ProgressChanged);
            //后台任务执行完成时调用的方法
            bgMeet.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgMeet_RunWorkerCompleted);
            bgMeet.RunWorkerAsync(); //任务启动
        }

        void bgMeet_DoWork(object sender, DoWorkEventArgs e)
        {
            //开始播放等待动画
            this.Dispatcher.Invoke(new Action(() =>
            {
                loading.Visibility = System.Windows.Visibility.Visible;
            }));
            //开始后台任务
            GetData();
        }

        //报告任务进度
        void bgMeet_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.lab_pro.Content = e.ProgressPercentage + "%";
            }));
        }

        //任务执行完成后更新状态
        void bgMeet_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loading.Visibility = System.Windows.Visibility.Collapsed;
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.lab_pro.Content = "完成";
            }));
        }

        public void GetData()
        {
            RepairExercise();
            RepairGAD();
            RepairPHQ();
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
                    MessageBox.Show("数据表Exercise转换错误,错误编号为" + tempNumber + "\n" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                var s = RepairList;
                context.SaveChanges();
                return true;


            }
            catch (Exception e)
            {
                MessageBox.Show("Exercise数据连接错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("数据表GAD转换错误,错误编号为" + tempNumber + "\n" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                context.SaveChanges();
                return true;


            }
            catch (Exception e)
            {
                MessageBox.Show("GAD数据连接错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        private static bool RepairPHQ()
        {
            try
            {
                var context = new DbAdapter();
                var RepairList = context.DbPHQ.ToList();
                string tempNumber = null;
                try
                {
                    for (int i = 0; i < RepairList.Count; i++)
                    {
                        tempNumber = RepairList[i].PHQNumber.Trim();
                        var tempList = RepairList[i];
                        string tempBaiscNumber = RepairList[i].basicinfoNumber;
                        context.DbPHQ.Remove(tempList);

                        RepairList.RemoveAt(i);
                        var newNumber = NumberConverter(tempNumber, tempBaiscNumber);
                        RepairList.Insert(i, ItemCopy(tempList, newNumber));

                    }

                    for (int i = 0; i < RepairList.Count; i++)
                    {
                        Regex IndexReg = new Regex("^(-\\d{2})$");
                        if (!IndexReg.IsMatch(RepairList[i].PHQNumber.Substring(RepairList[i].PHQNumber.Length - 3, 3)))
                        {
                            string tempListNumber = RepairList[i].PHQNumber;
                            var exsitingList = RepairList.FindAll((PHQ e) => e.PHQNumber == tempListNumber);
                            RepairList.RemoveAll((PHQ e) => e.PHQNumber == tempListNumber);
                            for (int j = 0; j < exsitingList.Count; j++)
                            {
                                if (!string.IsNullOrWhiteSpace(exsitingList[j].basicinfoNumber))
                                {
                                    exsitingList[j].PHQNumber = exsitingList[j].PHQNumber + "-" + (j + 1).ToString("D2");
                                    tempNumber = exsitingList[j].PHQNumber;
                                    context.DbPHQ.Add(exsitingList[j]);
                                    RepairList.Add(exsitingList[j]);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("数据表PHQ转换错误,错误编号为" + tempNumber + "\n" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                context.SaveChanges();
                return true;


            }
            catch (Exception e)
            {
                MessageBox.Show("PHQ数据连接错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        private static string NumberConverter(string Number, string basicinfoNumber)
        {
            string newNumber = null;
            string tempNumber = Number.Trim();
            string tempBaiscNumber = string.IsNullOrEmpty(basicinfoNumber) ? null : basicinfoNumber.Trim();
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
            else
            {
                newNumber += "-";
                newNumber += "1900";
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
            dt.Rows[0][dt.Columns.Count - 2] = dt.Rows[0][dt.Columns.Count - 2].ToString().Trim();
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



    }
}
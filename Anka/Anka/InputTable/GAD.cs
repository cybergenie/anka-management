﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;

namespace Anka
{
    public partial class MainWindow
    {       
       
        private void BtGADSave_Click(object sender, RoutedEventArgs e)
        {
            if(this.txGADLoop.Text.Trim().Length>0)
            {
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        var dicData = new Dictionary<string, object>();

                        dicData["GADResult"] = DataAdapter.ArrayToString(GADData());

                        string GADNumber = DataAdapter.Number + "-" + this.txGADLoop.Text.Trim();

                        string sql = string.Format("SELECT * FROM gad where GADNumber='{0}';", GADNumber);
                        DataTable dt = sh.Select(sql);
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                var dicCondition = new Dictionary<string, object>();
                                dicCondition["basicinfo_Number"] = DataAdapter.Number;
                                dicCondition["GADNumber"] = GADNumber;
                                sh.Update("gad", dicData, dicCondition);
                            }
                            else
                            {
                                dicData["GADNumber"] = GADNumber;
                                dicData["basicinfo_Number"] = DataAdapter.Number;
                                sh.Insert("gad", dicData);
                                txGADLoop.Items.Add(this.txGADLoop.Text.Trim());
                            }
                        }
                        catch (SQLiteException ex)
                        {
                            MessageBox.Show(string.Format("数据更新错误。错误代码为:{0}", ex.ErrorCode), "数据更新错误");
                        }
                        finally
                        {
                            conn.Close();
                        }

                    }
                }
                
                ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                MessageBox.Show("请输入PHQ评估量表编号。");
            }
           


        }

        private void BtGADLoad_Click(object sender, RoutedEventArgs e)
        {
            if (this.txGADLoop.Text.Trim().Length > 0)
            {
                string GADNumber = DataAdapter.Number + "-" + this.txGADLoop.Text.Trim();
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        try
                        {
                            DataTable dt = sh.Select(string.Format("select * from gad where GADNumber='{0}';", GADNumber));
                            if (dt.Rows.Count > 0)
                            {
                                GADDataLoad(dt);
                            }
                            else
                            {
                                MessageBox.Show("该编号数据不存在。");
                            }

                        }
                        catch (SQLiteException ex)
                        {
                            MessageBox.Show(string.Format("数据更新错误。错误代码为:{0}", ex.ErrorCode), "数据更新错误");
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请输入项目编号。");
            }

        }

        private int[] GADData()
        {
            int[] Data = new int[7];
            RadioButton[,] rbGADs = new RadioButton[7, 4]
               {{this.rbGAD11,this.rbGAD12,this.rbGAD13,this.rbGAD14},
                {this.rbGAD21,this.rbGAD22,this.rbGAD23,this.rbGAD24 },
                {this.rbGAD31,this.rbGAD32,this.rbGAD33,this.rbGAD34 },
                {this.rbGAD41,this.rbGAD42,this.rbGAD43,this.rbGAD44 },
                {this.rbGAD51,this.rbGAD52,this.rbGAD53,this.rbGAD54 },
                {this.rbGAD61,this.rbGAD62,this.rbGAD63,this.rbGAD64 },
                {this.rbGAD71,this.rbGAD72,this.rbGAD73,this.rbGAD74 }
               };
            for (int i = 0; i < 7; i++)
            {
                int temp = -1;
                for (int j = 0; j < 4; j++)
                {
                    if (rbGADs[i, j].IsChecked == true)
                        temp = j;
                }
                Data[i] = temp;
            }

            return Data;
        }

        private void GADDataLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];

            RadioButton[,] rbGADs = new RadioButton[7, 4]
              {{this.rbGAD11,this.rbGAD12,this.rbGAD13,this.rbGAD14},
                {this.rbGAD21,this.rbGAD22,this.rbGAD23,this.rbGAD24 },
                {this.rbGAD31,this.rbGAD32,this.rbGAD33,this.rbGAD34 },
                {this.rbGAD41,this.rbGAD42,this.rbGAD43,this.rbGAD44 },
                {this.rbGAD51,this.rbGAD52,this.rbGAD53,this.rbGAD54 },
                {this.rbGAD61,this.rbGAD62,this.rbGAD63,this.rbGAD64 },
                {this.rbGAD71,this.rbGAD72,this.rbGAD73,this.rbGAD74 }
              };

            string sGADResult = dr["GADResult"] == System.DBNull.Value ? "" : dr["GADResult"].ToString();
            string[] GADResultArray = sGADResult.Split('|');
            int result = 0;
            for (int i = 0; i < 7; i++)
            {
                result += (Convert.ToInt32(GADResultArray[i]) > 0 ? Convert.ToInt32(GADResultArray[i]) : 0);
                switch (GADResultArray[i])
                {
                    case "0":
                        rbGADs[i, 0].IsChecked = true;
                        rbGADs[i, 1].IsChecked = false;
                        rbGADs[i, 2].IsChecked = false;
                        rbGADs[i, 3].IsChecked = false;
                        break;
                    case "1":
                        rbGADs[i, 0].IsChecked = false;
                        rbGADs[i, 1].IsChecked = true;
                        rbGADs[i, 2].IsChecked = false;
                        rbGADs[i, 3].IsChecked = false;
                        break;
                    case "2":
                        rbGADs[i, 0].IsChecked = false;
                        rbGADs[i, 1].IsChecked = false;
                        rbGADs[i, 2].IsChecked = true;
                        rbGADs[i, 3].IsChecked = false;
                        break;
                    case "3":
                        rbGADs[i, 0].IsChecked = false;
                        rbGADs[i, 1].IsChecked = false;
                        rbGADs[i, 2].IsChecked = false;
                        rbGADs[i, 3].IsChecked = true;
                        break;
                    default:
                        rbGADs[i, 0].IsChecked = false;
                        rbGADs[i, 1].IsChecked = false;
                        rbGADs[i, 2].IsChecked = false;
                        rbGADs[i, 3].IsChecked = false;
                        break;

                }
            }           

            this.lbGADResult.Content = result.ToString();
        }

        private void RbGAD_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            int[] Data = GADData();
            foreach (int temp in Data)
            {
                if (temp >= 0)
                    result += temp;
            }

            this.lbGADResult.Content = result.ToString();
        }

    }
}

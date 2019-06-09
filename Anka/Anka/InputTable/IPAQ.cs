using System;
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
        private void BtIPAQSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.txIPAQLoop.Text.Trim().Length > 0)
            {

                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        var dicData = new Dictionary<string, object>();

                        IPAQDataSave(dicData);

                        string IPAQNumber = DataAdapter.Number + "-" + this.txIPAQLoop.Text.Trim();

                        string sql = string.Format("SELECT * FROM ipaq where IPAQNumber='{0}';", IPAQNumber);
                        DataTable dt = sh.Select(sql);
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                var dicCondition = new Dictionary<string, object>();
                                dicCondition["basicinfo_Number"] = DataAdapter.Number;
                                dicCondition["IPAQNumber"] = IPAQNumber;
                                sh.Update("ipaq", dicData, dicCondition);
                            }
                            else
                            {
                                dicData["IPAQNumber"] = IPAQNumber;
                                dicData["basicinfo_Number"] = DataAdapter.Number;
                                sh.Insert("ipaq", dicData);
                                txIPAQLoop.Items.Add(this.txIPAQLoop.Text.Trim());
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
                MessageBox.Show("请输入国际标准化身体活动调查问卷编号。");
            }
           

        }
        private void BtIPAQLoad_Click(object sender, RoutedEventArgs e)
        {
            if (this.txIPAQLoop.Text.Trim().Length > 0)
            {
                string IPAQNumber = DataAdapter.Number + "-" + this.txIPAQLoop.Text.Trim();
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        try
                        {
                            DataTable dt = sh.Select(string.Format("select * from ipaq where IPAQNumber='{0}';", IPAQNumber));
                            if (dt.Rows.Count > 0)
                            {
                                IPAQDataLoad(dt);
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

        private void TxIPAQ4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.txIPAQ4.Text.Contains('h') || this.txIPAQ4.Text.Contains('H'))
                if(DataAdapter.IsNumber(this.txIPAQ4.Text.Trim('h'))==true)
                {
                    this.txIPAQ4.Text = (Convert.ToInt32(this.txIPAQ4.Text.Trim('h')) * 60).ToString();
                }
            else
                {
                    this.txIPAQ4.Text = "";
                }
                
        }

        private void IPAQDataSave(Dictionary<string, object> dic)
        {
            if (this.rbIPAQYes.IsChecked == true)
                dic["IPAQ0"] = true;
            if (this.rbIPAQNo.IsChecked == false)
                dic["IPAQ0"] = false;
            dic["IPAQ1"] = DataAdapter.IsNumber(this.txIPAQ1.Text.Trim()) ? Convert.ToInt32(this.txIPAQ1.Text.Trim()) : 0;
            dic["IPAQ2"] = DataAdapter.IsNumber(this.txIPAQ2.Text.Trim()) ? Convert.ToInt32(this.txIPAQ2.Text.Trim()) : 0;
            dic["IPAQ3"] = DataAdapter.IsNumber(this.txIPAQ3.Text.Trim()) ? Convert.ToInt32(this.txIPAQ3.Text.Trim()) : 0;
            dic["IPAQ4"] = DataAdapter.IsNumber(this.txIPAQ4.Text.Trim()) ? Convert.ToInt32(this.txIPAQ4.Text.Trim()) : 0;

            RadioButton[] rbIPAQ5 = new RadioButton[7] { this.rbIPAQ51, this.rbIPAQ52, this.rbIPAQ53, this.rbIPAQ54, this.rbIPAQ55, this.rbIPAQ56, this.rbIPAQ57 };
            for (int i = 0; i < 7; i++)
            {
                if (rbIPAQ5[i].IsChecked == true)
                    dic["IPAQ5"] = i;
            }


        }

        private void IPAQDataLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            rbIPAQYes.IsChecked = false; rbIPAQNo.IsChecked = false;
            if (dr["IPAQ0"] != System.DBNull.Value)
            {

                if (Convert.ToBoolean(dr["IPAQ0"]) == true)
                {
                    rbIPAQYes.IsChecked = true;                    
                }
                else if (Convert.ToBoolean(dr["IPAQ0"]) == false)
                {                   
                    rbIPAQNo.IsChecked = true;
                }
            }

            txIPAQ1.Text = dr["IPAQ1"] == System.DBNull.Value ? "" : dr["IPAQ1"].ToString();
            txIPAQ2.Text = dr["IPAQ2"] == System.DBNull.Value ? "" : dr["IPAQ2"].ToString();
            txIPAQ3.Text = dr["IPAQ3"] == System.DBNull.Value ? "" : dr["IPAQ3"].ToString();
            txIPAQ4.Text = dr["IPAQ4"] == System.DBNull.Value ? "" : dr["IPAQ4"].ToString();
            
            RadioButton[] rbIPAQ5 = new RadioButton[7] { this.rbIPAQ51, this.rbIPAQ52, this.rbIPAQ53, this.rbIPAQ54, this.rbIPAQ55, this.rbIPAQ56, this.rbIPAQ57 };
            foreach (RadioButton rbIPAQ in rbIPAQ5)
            {
                rbIPAQ.IsChecked = false;
            }
            if (dr["IPAQ5"] != System.DBNull.Value)
            {
                int i = Convert.ToInt32(dr["IPAQ5"]);
                rbIPAQ5[i].IsChecked = true;
            }



        }


    }
}

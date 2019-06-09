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
        private void BtOHQSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.txOHQLoop.Text.Trim().Length > 0)
            {
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        var dicData = new Dictionary<string, object>();

                        OHQDataSave(dicData);

                        string OHQNumber = DataAdapter.Number + "-" + this.txOHQLoop.Text.Trim();

                        string sql = string.Format("SELECT * FROM ohq where OHQNumber='{0}';", OHQNumber);
                        DataTable dt = sh.Select(sql);
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                var dicCondition = new Dictionary<string, object>();
                                dicCondition["basicinfo_Number"] = DataAdapter.Number;
                                dicCondition["OHQNumber"] = OHQNumber;
                                sh.Update("ohq", dicData, dicCondition);
                            }
                            else
                            {
                                dicData["OHQNumber"] = OHQNumber;
                                dicData["basicinfo_Number"] = DataAdapter.Number;
                                sh.Insert("ohq", dicData);
                                txOHQLoop.Items.Add(this.txOHQLoop.Text.Trim());
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
                MessageBox.Show("请输入口腔卫生调查表编号。");
            }
           
        }
        private void BtOHQLoad_Click(object sender, RoutedEventArgs e)
        {
            if (this.txOHQLoop.Text.Trim().Length > 0)
            {
                string OHQNumber = DataAdapter.Number + "-" + this.txOHQLoop.Text.Trim();
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        try
                        {
                            DataTable dt = sh.Select(string.Format("select * from ohq where OHQNumber='{0}';", OHQNumber));
                            if (dt.Rows.Count > 0)
                            {
                                OHQDataLoad(dt);
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

        private void OHQDataSave(Dictionary<string, object> dic)
        {
            RadioButton[] rbOHQ1 = new RadioButton[4] { this.rbOHQ11, this.rbOHQ12, this.rbOHQ13, this.rbOHQ14 };
            RadioButton[] rbOHQ2 = new RadioButton[3] { this.rbOHQ21, this.rbOHQ22, this.rbOHQ23 };
            RadioButton[] rbOHQ3 = new RadioButton[6] { this.rbOHQ31, this.rbOHQ32, this.rbOHQ33, this.rbOHQ34, this.rbOHQ35, this.rbOHQ36 };
            RadioButton[] rbOHQ4 = new RadioButton[3] { this.rbOHQ41, this.rbOHQ42, this.rbOHQ43 };
            RadioButton[] rbOHQ5 = new RadioButton[4] { this.rbOHQ51, this.rbOHQ52, this.rbOHQ53, this.rbOHQ54 };
            RadioButton[] rbOHQ6 = new RadioButton[3] { this.rbOHQ61, this.rbOHQ62, this.rbOHQ63 };
            RadioButton[] rbOHQ7 = new RadioButton[3] { this.rbOHQ71, this.rbOHQ72, this.rbOHQ73 };
            RadioButton[] rbOHQ8 = new RadioButton[3] { this.rbOHQ81, this.rbOHQ82, this.rbOHQ83 };
            RadioButton[] rbOHQ9 = new RadioButton[2] { this.rbOHQ91, this.rbOHQ92 };

            for (int i = 0; i < rbOHQ1.Length; i++)
            {
                if (rbOHQ1[i].IsChecked == true)
                    dic["OHQ1"] = i.ToString();
            }

            for (int i = 0; i < rbOHQ2.Length; i++)
            {
                if (rbOHQ2[i].IsChecked == true)
                    dic["OHQ2"] = i.ToString();
            }

            for (int i = 0; i < rbOHQ3.Length; i++)
            {
                if (rbOHQ3[i].IsChecked == true)
                    dic["OHQ3"] = i.ToString();
            }


            for (int i = 0; i < rbOHQ4.Length; i++)
            {
                if (rbOHQ4[i].IsChecked == true)
                {
                    switch (i)
                    {
                        case 0:
                            dic["OHQ4"] = "A-" + this.txOHQ41.Text.Trim();
                            break;
                        case 1:
                            dic["OHQ4"] = "B-" + this.txOHQ42.Text.Trim();
                            break;
                        case 2:
                            dic["OHQ4"] = "C-0";
                            break;
                    }
                }
            }

            for (int i = 0; i < rbOHQ5.Length; i++)
            {
                if (rbOHQ5[i].IsChecked == true)
                    dic["OHQ5"] = i.ToString();
            }

            for (int i = 0; i < rbOHQ6.Length; i++)
            {
                if (rbOHQ6[i].IsChecked == true)
                {
                    switch (i)
                    {
                        case 0:
                            dic["OHQ6"] = "0";
                            break;
                        case 1:
                            dic["OHQ6"] = this.txOHQ62.Text.Trim();
                            break;
                        case 2:
                            dic["OHQ6"] = "99";
                            break;
                    }
                }
            }

            for (int i = 0; i < rbOHQ7.Length; i++)
            {
                if (rbOHQ7[i].IsChecked == true)
                    dic["OHQ7"] = i.ToString();
            }

            for (int i = 0; i < rbOHQ8.Length; i++)
            {
                if (rbOHQ8[i].IsChecked == true)
                    dic["OHQ8"] = i.ToString();
            }

            for (int i = 0; i < rbOHQ9.Length; i++)
            {
                if (rbOHQ9[i].IsChecked == true)
                {
                    switch (i)
                    {
                        case 0:
                            dic["OHQ9"] = "A-0-0";
                            break;
                        case 1:
                            dic["OHQ9"] = "B-" + this.txOHQ91.Text.Trim() + "-" + this.txOHQ92.Text.Trim();
                            break;
                    }
                }
            }

        }

        private void OHQDataLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];

            RadioButton[] rbOHQ1 = new RadioButton[4] { this.rbOHQ11, this.rbOHQ12, this.rbOHQ13, this.rbOHQ14 };
            RadioButton[] rbOHQ2 = new RadioButton[3] { this.rbOHQ21, this.rbOHQ22, this.rbOHQ23 };
            RadioButton[] rbOHQ3 = new RadioButton[6] { this.rbOHQ31, this.rbOHQ32, this.rbOHQ33, this.rbOHQ34, this.rbOHQ35, this.rbOHQ36 };
            RadioButton[] rbOHQ4 = new RadioButton[3] { this.rbOHQ41, this.rbOHQ42, this.rbOHQ43 };
            RadioButton[] rbOHQ5 = new RadioButton[4] { this.rbOHQ51, this.rbOHQ52, this.rbOHQ53, this.rbOHQ54 };
            RadioButton[] rbOHQ6 = new RadioButton[3] { this.rbOHQ61, this.rbOHQ62, this.rbOHQ63 };
            RadioButton[] rbOHQ7 = new RadioButton[3] { this.rbOHQ71, this.rbOHQ72, this.rbOHQ73 };
            RadioButton[] rbOHQ8 = new RadioButton[3] { this.rbOHQ81, this.rbOHQ82, this.rbOHQ83 };
            RadioButton[] rbOHQ9 = new RadioButton[2] { this.rbOHQ91, this.rbOHQ92 };

            foreach(RadioButton rbOHQ in rbOHQ1)
            {
                rbOHQ.IsChecked = false;
            }
            if(dr["OHQ1"] != System.DBNull.Value)
            {
                int i = Convert.ToInt32(dr["OHQ1"]);
                rbOHQ1[i].IsChecked = true;
            }

            foreach (RadioButton rbOHQ in rbOHQ2)
            {
                rbOHQ.IsChecked = false;
            }
            if (dr["OHQ2"] != System.DBNull.Value)
            {
                int i = Convert.ToInt32(dr["OHQ2"]);
                rbOHQ2[i].IsChecked = true;
            }

            foreach (RadioButton rbOHQ in rbOHQ3)
            {
                rbOHQ.IsChecked = false;
            }
            if (dr["OHQ3"] != System.DBNull.Value)
            {
                int i = Convert.ToInt32(dr["OHQ3"]);
                rbOHQ3[i].IsChecked = true;
            }
            foreach (RadioButton rbOHQ in rbOHQ4)
            {
                rbOHQ.IsChecked = false;
            }
            if (dr["OHQ4"] != System.DBNull.Value)
            {
                txOHQ41.Text = ""; txOHQ42.Text = "";
                string sOHQ4 = dr["OHQ4"] == System.DBNull.Value ? "" : dr["OHQ4"].ToString();
                string[] OHQ4Array = sOHQ4.Split('-');
                switch (OHQ4Array[0])
                {
                    case "A":
                        rbOHQ4[0].IsChecked = true;
                        if (OHQ4Array.Length > 1)
                            txOHQ41.Text = OHQ4Array[1];
                        else
                            txOHQ41.Text = "";
                        break;
                    case "B":
                        rbOHQ4[1].IsChecked = true;
                        if (OHQ4Array.Length > 1)
                            txOHQ42.Text = OHQ4Array[1];
                        else
                            txOHQ42.Text = "";
                        break;
                    case "C":
                        rbOHQ4[2].IsChecked = true;                       
                        break;
                }
            }
            foreach (RadioButton rbOHQ in rbOHQ5)
            {
                rbOHQ.IsChecked = false;
            }
            if (dr["OHQ5"] != System.DBNull.Value)
            {
                int i = Convert.ToInt32(dr["OHQ5"]);
                rbOHQ5[i].IsChecked = true;
            }
            foreach (RadioButton rbOHQ in rbOHQ6)
            {
                rbOHQ.IsChecked = false;
            }
            if (dr["OHQ6"] != System.DBNull.Value)
            {
                txOHQ62.Text = "";
                string sOHQ6 = dr["OHQ6"] == System.DBNull.Value ? "" : dr["OHQ6"].ToString();
                switch (sOHQ6)
                {
                    case "0":
                        rbOHQ6[0].IsChecked = true;
                        break;
                    case "99":
                        rbOHQ6[2].IsChecked = true;
                        break;
                    default:
                        if (sOHQ6 != "")
                        {
                            rbOHQ6[1].IsChecked = true;
                            txOHQ62.Text = sOHQ6;
                        }
                        break;
                }
            }

            foreach (RadioButton rbOHQ in rbOHQ7)
            {
                rbOHQ.IsChecked = false;
            }
            if (dr["OHQ7"] != System.DBNull.Value)
            {
                int i = Convert.ToInt32(dr["OHQ7"]);
                rbOHQ7[i].IsChecked = true;
            }

            foreach (RadioButton rbOHQ in rbOHQ8)
            {
                rbOHQ.IsChecked = false;
            }
            if (dr["OHQ8"] != System.DBNull.Value)
            {
                int i = Convert.ToInt32(dr["OHQ8"]);
                rbOHQ8[i].IsChecked = true;
            }

            foreach (RadioButton rbOHQ in rbOHQ9)
            {
                rbOHQ.IsChecked = false;
            }
            if (dr["OHQ9"] != System.DBNull.Value)
            {
                txOHQ91.Text = ""; txOHQ92.Text = "";
                string sOHQ9 = dr["OHQ9"] == System.DBNull.Value ? "" : dr["OHQ9"].ToString();
                string[] OHQ9Array = sOHQ9.Split('-');
                switch (OHQ9Array[0])
                {
                    case "A":
                        rbOHQ9[0].IsChecked = true;                        
                        break;
                    case "B":
                        rbOHQ9[1].IsChecked = true;
                        if (OHQ9Array.Length > 1)
                            txOHQ91.Text = OHQ9Array[1];
                        else
                            txOHQ91.Text = "";
                        if (OHQ9Array.Length > 2)
                            txOHQ92.Text = OHQ9Array[2];
                        else
                            txOHQ92.Text = "";
                        break;
                    
                }
            }
        }

        private void RbOHQ62_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbOHQ62.IsChecked == true)
            {
                this.txOHQ62.IsEnabled = true;
            }
            else
            {
                this.txOHQ62.Text = "";
                this.txOHQ62.IsEnabled = false;
            }
        }

        private void RbOHQ4_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbOHQ41.IsChecked == true)
                this.txOHQ41.IsEnabled = true;
            else
            {
                this.txOHQ41.Text = "";
                this.txOHQ41.IsEnabled = false;
            }

            if (this.rbOHQ42.IsChecked == true)
                this.txOHQ42.IsEnabled = true;
            else
            {
                this.txOHQ42.Text = "";
                this.txOHQ42.IsEnabled = false;
            }
        }
        private void RbOHQ9_Click(object sender, RoutedEventArgs e)
        {
            if(rbOHQ91.IsChecked==true)
            {
                this.txOHQ91.Text = "";
                this.txOHQ91.IsEnabled = false;
                this.txOHQ92.Text = "";
                this.txOHQ92.IsEnabled = false;
            }
            else if(rbOHQ92.IsChecked == true)
            {
                this.txOHQ91.IsEnabled = true;
                this.txOHQ92.IsEnabled = true;
            }

        }

    }
}

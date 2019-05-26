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

    }
}

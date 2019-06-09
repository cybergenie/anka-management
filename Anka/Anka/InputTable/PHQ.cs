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
        private void BtPHQSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.txPHQLoop.Text.Trim().Length > 0)
            {
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        var dicData = new Dictionary<string, object>();

                        dicData["PHQResult"] = DataAdapter.ArrayToString(PHQData());

                        string PHQNumber = DataAdapter.Number + "-" + this.txPHQLoop.Text.Trim();

                        string sql = string.Format("SELECT * FROM phq where PHQNumber='{0}';", PHQNumber);
                        DataTable dt = sh.Select(sql);
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                var dicCondition = new Dictionary<string, object>();
                                dicCondition["basicinfo_Number"] = DataAdapter.Number;
                                dicCondition["PHQNumber"] = PHQNumber;
                                sh.Update("phq", dicData, dicCondition);
                            }
                            else
                            {
                                dicData["PHQNumber"] = PHQNumber;
                                dicData["basicinfo_Number"] = DataAdapter.Number;
                                sh.Insert("phq", dicData);
                                txPHQLoop.Items.Add(this.txPHQLoop.Text.Trim());
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
        private void BtPHQLoad_Click(object sender, RoutedEventArgs e)
        {

            if (this.txPHQLoop.Text.Trim().Length > 0)
            {
                string PHQNumber = DataAdapter.Number + "-" + this.txPHQLoop.Text.Trim();
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        try
                        {
                            DataTable dt = sh.Select(string.Format("select * from phq where PHQNumber='{0}';", PHQNumber));
                            if (dt.Rows.Count > 0)
                            {
                                PHQDataLoad(dt);
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

        private int[] PHQData()
        {
            int[] Data = new int[9];
            RadioButton[,] rbPHQs = new RadioButton[9, 4]
               {{this.rbPHQ11,this.rbPHQ12,this.rbPHQ13,this.rbPHQ14},
                {this.rbPHQ21,this.rbPHQ22,this.rbPHQ23,this.rbPHQ24 },
                {this.rbPHQ31,this.rbPHQ32,this.rbPHQ33,this.rbPHQ34 },
                {this.rbPHQ41,this.rbPHQ42,this.rbPHQ43,this.rbPHQ44 },
                {this.rbPHQ51,this.rbPHQ52,this.rbPHQ53,this.rbPHQ54 },
                {this.rbPHQ61,this.rbPHQ62,this.rbPHQ63,this.rbPHQ64 },
                {this.rbPHQ71,this.rbPHQ72,this.rbPHQ73,this.rbPHQ74 },
                {this.rbPHQ81,this.rbPHQ82,this.rbPHQ83,this.rbPHQ84 },
                {this.rbPHQ91,this.rbPHQ92,this.rbPHQ93,this.rbPHQ94 }
               };
            for (int i = 0; i < 9; i++)
            {
                int temp = -1;
                for (int j = 0; j < 4; j++)
                {
                    if (rbPHQs[i, j].IsChecked == true)
                        temp = j;
                }
                Data[i] = temp;
            }

            return Data;
        }

        private void PHQDataLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];

            RadioButton[,] rbPHQs = new RadioButton[9, 4]
               {{this.rbPHQ11,this.rbPHQ12,this.rbPHQ13,this.rbPHQ14},
                {this.rbPHQ21,this.rbPHQ22,this.rbPHQ23,this.rbPHQ24 },
                {this.rbPHQ31,this.rbPHQ32,this.rbPHQ33,this.rbPHQ34 },
                {this.rbPHQ41,this.rbPHQ42,this.rbPHQ43,this.rbPHQ44 },
                {this.rbPHQ51,this.rbPHQ52,this.rbPHQ53,this.rbPHQ54 },
                {this.rbPHQ61,this.rbPHQ62,this.rbPHQ63,this.rbPHQ64 },
                {this.rbPHQ71,this.rbPHQ72,this.rbPHQ73,this.rbPHQ74 },
                {this.rbPHQ81,this.rbPHQ82,this.rbPHQ83,this.rbPHQ84 },
                {this.rbPHQ91,this.rbPHQ92,this.rbPHQ93,this.rbPHQ94 }
               };

            string sPHQResult = dr["PHQResult"] == System.DBNull.Value ? "" : dr["PHQResult"].ToString();
            string[] PHQResultArray = sPHQResult.Split('|');
            int result = 0;
            for (int i = 0; i < 9; i++)
            {
                result += (Convert.ToInt32(PHQResultArray[i]) > 0 ? Convert.ToInt32(PHQResultArray[i]) : 0);
                switch (PHQResultArray[i])
                {                    
                    case "0":
                        rbPHQs[i, 0].IsChecked = true;
                        rbPHQs[i, 1].IsChecked = false;
                        rbPHQs[i, 2].IsChecked = false;
                        rbPHQs[i, 3].IsChecked = false;
                        break;
                    case "1":
                        rbPHQs[i, 0].IsChecked = false;
                        rbPHQs[i, 1].IsChecked = true;
                        rbPHQs[i, 2].IsChecked = false;
                        rbPHQs[i, 3].IsChecked = false;
                        break;
                    case "2":
                        rbPHQs[i, 0].IsChecked = false;
                        rbPHQs[i, 1].IsChecked = false;
                        rbPHQs[i, 2].IsChecked = true;
                        rbPHQs[i, 3].IsChecked = false;
                        break;
                    case "3":
                        rbPHQs[i, 0].IsChecked = false;
                        rbPHQs[i, 1].IsChecked = false;
                        rbPHQs[i, 2].IsChecked = false;
                        rbPHQs[i, 3].IsChecked = true;
                        break;
                    default:
                        rbPHQs[i, 0].IsChecked = false;
                        rbPHQs[i, 1].IsChecked = false;
                        rbPHQs[i, 2].IsChecked = false;
                        rbPHQs[i, 3].IsChecked = false;
                        break;

                }
            }

            if (result >= 0 && result <= 4)
                this.rbPHQResult1.IsChecked = true;
            if (result >= 5 && result <= 9)
                this.rbPHQResult2.IsChecked = true;
            if (result >= 10 && result <= 13)
                this.rbPHQResult3.IsChecked = true;
            if (result >= 14 && result <= 18)
                this.rbPHQResult4.IsChecked = true;
            if (result >= 19)
                this.rbPHQResult5.IsChecked = true;

            this.lbPHQResult.Content = result.ToString();
        }



        private void RbPHQ_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            int[] Data = PHQData();
            foreach (int temp in Data)
            {
                if (temp >= 0)
                    result += temp;
            }

            if (result >= 0 && result <= 4)
                this.rbPHQResult1.IsChecked = true;
            if (result >= 5 && result <= 9)
                this.rbPHQResult2.IsChecked = true;
            if (result >= 10 && result <= 13)
                this.rbPHQResult3.IsChecked = true;
            if (result >= 14 && result <= 18)
                this.rbPHQResult4.IsChecked = true;
            if (result >= 19)
                this.rbPHQResult5.IsChecked = true;

            this.lbPHQResult.Content = result.ToString();
        }
    }
}

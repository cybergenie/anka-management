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

        private void TxIPAQ4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.txIPAQ4.Text.Contains('h') || this.txIPAQ4.Text.Contains('H'))
                this.txIPAQ4.Text = (Convert.ToInt32(this.txIPAQ4.Text.Trim('h')) * 60).ToString();
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


    }
}

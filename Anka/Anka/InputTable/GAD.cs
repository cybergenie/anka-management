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

    }
}
